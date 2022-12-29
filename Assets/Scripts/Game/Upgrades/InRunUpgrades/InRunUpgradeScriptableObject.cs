using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    public abstract class InRunUpgradeScriptableObject : ScriptableObject
    {
        public TierScriptableObject Tier;
        public bool Unique;
        public string Description;

        public abstract void Apply();

        protected float CalculatePropertyPercentage(float currentValue, float property, bool isPercentage)
        {
            if (isPercentage)
            {
                property = property * currentValue;
            }

            return property;
        }
    }
}