using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InGameUpgrades;

namespace Assets.Scripts.InRunUpgradre
{
    public class InRunUpgradeCreationToolTurretUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        public override void OnGui()
        {
        }

        public override void Create(string upgradeName, TierScriptableObject tier)
        {
            StringBuilder pathStringBuilder = new StringBuilder("Assets/ScriptableObjects/Upgrades/InRunUpgrades/");
            pathStringBuilder.Append(upgradeName);
            pathStringBuilder.Append(".asset");

            InRunGameUpgradeScriptableObject gameUpgradeScriptablejObject = ScriptableObject.CreateInstance<InRunGameUpgradeScriptableObject>();


            AssetDatabase.CreateAsset(gameUpgradeScriptablejObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
