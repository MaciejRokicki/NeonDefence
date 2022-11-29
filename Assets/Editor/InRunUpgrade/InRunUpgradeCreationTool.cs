using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.InRunUpgradre
{
    public class InRunUpgradeCreationTool : EditorWindow
    {
        private InRunUpgradeCreationToolStrategy selectedStrategy;
        private InRunUpgradeCreationToolStrategy gameUpgradeStrategy;
        private InRunUpgradeCreationToolStrategy turretUpgradeStrategy;

        private string upgradeName;
        private int selectedTierId = 0;
        private bool unique;
        private string[] tiersNames;
        private TierScriptableObject[] tiers;
        private bool isGameUpgrade = true;

        private void Awake()
        {
            gameUpgradeStrategy = new InRunUpgradeCreationToolGameUpgradeStrategy();
            turretUpgradeStrategy = new InRunUpgradeCreationToolTurretUpgradeStrategy();
        }

        private void OnEnable()
        {
            string[] files = Directory.GetFiles("Assets/ScriptableObjects/Upgrades/Tiers", "*.asset");
            tiersNames = new string[files.Length];
            tiers = new TierScriptableObject[files.Length];

            StringBuilder tierNameBuilder = new StringBuilder();

            for(int i = 0; i < files.Length; i++)
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

        [MenuItem("Tools/InRunUpgrade Creation tool")]
        public static void ShowWindow()
        {
            GetWindow<InRunUpgradeCreationTool>();
        }

        private void OnGUI()
        {
            GUILayout.Label("New upgrade", EditorStyles.boldLabel);
            upgradeName = EditorGUILayout.TextField("Name", upgradeName);
            selectedTierId = EditorGUILayout.Popup("Tier", selectedTierId, tiersNames);
            unique = EditorGUILayout.Toggle("Is unique", unique);
            isGameUpgrade = EditorGUILayout.Toggle("Is game upgrade", isGameUpgrade);

            selectedStrategy = isGameUpgrade ? gameUpgradeStrategy : turretUpgradeStrategy;

            selectedStrategy.OnGui();

            if (GUILayout.Button("Create"))
            {
                selectedStrategy.Create(upgradeName, unique, tiers[selectedTierId]);
            }
        }
    }

}