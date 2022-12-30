using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

namespace Assets.Editor.Game.InRunUpgrade.CreationTool.Strategies
{
    public class InRunUpgradeCreationToolGameUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        private float health;
        private bool healthIsPercentage;
        private float maxHealth;
        private bool maxHealthIsPercentage;
        private bool increaseHealthToo;
        private int neonBlocks;

        public InRunUpgradeCreationToolGameUpgradeStrategy(UpgradeManager upgradeManager) : base(upgradeManager) { }

        public override void OnGui()
        {
            InRunUpgradeScriptableObjectEditorGUI.GameUpgradeSection(
                ref health,
                ref healthIsPercentage,
                ref maxHealth,
                ref maxHealthIsPercentage,
                ref increaseHealthToo,
                ref neonBlocks);
        }

        public override void Create(string upgradeName, bool unique, TierScriptableObject tier, string description)
        {
            StringBuilder pathStringBuilder = new StringBuilder("Assets/ScriptableObjects/Upgrades/InRunUpgrades/");
            pathStringBuilder.Append(upgradeName);
            pathStringBuilder.Append(".asset");

            InRunGameUpgradeScriptableObject gameUpgradeScriptablejObject = ScriptableObject.CreateInstance<InRunGameUpgradeScriptableObject>();

            gameUpgradeScriptablejObject.Tier = tier;
            gameUpgradeScriptablejObject.Unique = unique;

            gameUpgradeScriptablejObject.Health = health;
            gameUpgradeScriptablejObject.HealthIsPercentage = healthIsPercentage;
            gameUpgradeScriptablejObject.MaxHealth = maxHealth;
            gameUpgradeScriptablejObject.MaxHealthIsPercentage = maxHealthIsPercentage;
            gameUpgradeScriptablejObject.IncreaseHealthToo = increaseHealthToo;
            gameUpgradeScriptablejObject.NeonBlocks = neonBlocks;

            gameUpgradeScriptablejObject.Description = description;

            AssetDatabase.CreateAsset(gameUpgradeScriptablejObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            upgradeManager.AddUpgrade(gameUpgradeScriptablejObject);
        }
    }
}
