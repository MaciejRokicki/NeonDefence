using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InGameUpgrades;

namespace Assets.Scripts.InRunUpgradre
{
    public class InRunUpgradeCreationToolGameUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        private float health;
        private float maxHealth;
        private bool increaseHealthToo;
        private int neonBlocks;

        public override void OnGui()
        {
            health = EditorGUILayout.FloatField("Health", health);
            maxHealth = EditorGUILayout.FloatField("Max health", maxHealth);
            increaseHealthToo = EditorGUILayout.Toggle("Increase health too", increaseHealthToo);
            neonBlocks = EditorGUILayout.IntField("Neon blocks", neonBlocks);
        }

        public override void Create(string upgradeName, TierScriptableObject tier)
        {
            StringBuilder pathStringBuilder = new StringBuilder("Assets/ScriptableObjects/Upgrades/InRunUpgrades/");
            pathStringBuilder.Append(tier.Name);
            pathStringBuilder.Append(upgradeName);
            pathStringBuilder.Append("InRunUpgrade");
            pathStringBuilder.Append(".asset");

            InRunGameUpgradeScriptableObject gameUpgradeScriptablejObject = ScriptableObject.CreateInstance<InRunGameUpgradeScriptableObject>();

            gameUpgradeScriptablejObject.tier = tier;

            gameUpgradeScriptablejObject.health = health;
            gameUpgradeScriptablejObject.maxHealth = maxHealth;
            gameUpgradeScriptablejObject.increaseHealthToo = increaseHealthToo;
            gameUpgradeScriptablejObject.neonBlocks = neonBlocks;

            AssetDatabase.CreateAsset(gameUpgradeScriptablejObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
