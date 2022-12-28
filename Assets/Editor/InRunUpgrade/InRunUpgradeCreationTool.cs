using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.InRunUpgrade
{
    public class InRunUpgradeCreationTool : EditorWindow
    {
        private UpgradeManager upgradeManager;

        private InRunUpgradeCreationToolStrategy selectedStrategy;
        private InRunUpgradeCreationToolStrategy gameUpgradeStrategy;
        private InRunUpgradeCreationToolStrategy turretUpgradeStrategy;

        private Vector2 windowScroll;

        private StringBuilder upgradeNameStringBuilder;
        private string upgradeName;
        private int selectedTierId = 0;
        private bool unique;
        private string[] tiersNames;
        private TierScriptableObject[] tiers;
        private bool isGameUpgrade = true;
        private string description;

        internal TurretScriptableObject[] turrets;
        internal string[] turretNames;

        private void Awake()
        {
            upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

            gameUpgradeStrategy = new InRunUpgradeCreationToolGameUpgradeStrategy(upgradeManager);
            turretUpgradeStrategy = new InRunUpgradeCreationToolTurretUpgradeStrategy(this, upgradeManager);
        }

        private void OnEnable()
        {
            InitTiers();
            InitTurrets();

            upgradeNameStringBuilder = new();
        }

        [MenuItem("Tools/InRunUpgrade Creation tool")]
        public static void ShowWindow()
        {
            InRunUpgradeCreationTool window = GetWindow<InRunUpgradeCreationTool>("InRunUpgrade Creation tool");

            window.minSize = new Vector2(400.0f, 100.0f);
        }

        private void OnGUI()
        {
            upgradeNameStringBuilder.Append(tiersNames[selectedTierId].Split(" ")[0]);
            upgradeNameStringBuilder.Append(upgradeName);
            upgradeNameStringBuilder.Append("InRunUpgrade");

            windowScroll = EditorGUILayout.BeginScrollView(windowScroll);

            GUILayout.Label("New upgrade", EditorStyles.boldLabel);
            upgradeName = EditorGUILayout.TextField("Name", upgradeName);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Name preview");
            EditorGUILayout.LabelField(upgradeNameStringBuilder.ToString());
            EditorGUILayout.EndHorizontal();
            selectedTierId = EditorGUILayout.Popup("Tier", selectedTierId, tiersNames);
            unique = EditorGUILayout.Toggle("Is unique", unique);
            isGameUpgrade = EditorGUILayout.Toggle("Is game upgrade", isGameUpgrade);

            selectedStrategy = isGameUpgrade ? gameUpgradeStrategy : turretUpgradeStrategy;

            selectedStrategy.OnGui();

            EditorGUILayout.Separator();
            EditorGUILayout.PrefixLabel("Description");
            description = EditorGUILayout.TextArea(description, GUILayout.Height(80.0f));

            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(upgradeName));

            if (GUILayout.Button("Create"))
            {
                selectedStrategy.Create(upgradeName, unique, tiers[selectedTierId], description);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

            upgradeNameStringBuilder.Clear();
        }

        private void InitTiers()
        {
            string[] files = Directory.GetFiles("Assets/ScriptableObjects/Upgrades/Tiers", "*.asset");
            tiersNames = new string[files.Length];
            tiers = new TierScriptableObject[files.Length];

            StringBuilder tierNameBuilder = new StringBuilder();

            for (int i = 0; i < files.Length; i++)
            {
                TierScriptableObject tier = AssetDatabase.LoadAssetAtPath(files[i], typeof(TierScriptableObject)) as TierScriptableObject;

                tiers[i] = tier;
            }


            tiers = tiers.OrderBy(x => x.MinChance).ToArray();

            for (int i = 0; i < tiers.Length; i++)
            {
                TierScriptableObject tier = tiers[i];

                tierNameBuilder.Append(tier.Name);
                tierNameBuilder.Append(" ");
                tierNameBuilder.Append(tier.MinChance);
                tierNameBuilder.Append("%-");
                tierNameBuilder.Append(tier.MaxChance);
                tierNameBuilder.Append("%");

                tiersNames[i] = tierNameBuilder.ToString();

                tierNameBuilder.Clear();
            }
        }

        private void InitTurrets()
        {
            string[] files = Directory.GetFiles("Assets/ScriptableObjects/Turrets", "*.asset");
            turretNames = new string[files.Length + 1];
            turrets = new TurretScriptableObject[files.Length + 1];

            for (int i = 0; i < files.Length; i++)
            {
                TurretScriptableObject turret = AssetDatabase.LoadAssetAtPath(files[i], typeof(TurretScriptableObject)) as TurretScriptableObject;

                turrets[i] = turret;
                turretNames[i] = turret.name;
            }

            turretNames[files.Length] = "All";
        }
    }

}