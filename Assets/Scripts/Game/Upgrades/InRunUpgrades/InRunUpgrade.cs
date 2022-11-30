using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    public abstract class InRunUpgrade : ScriptableObject
    {
        public TierScriptableObject Tier;
        public bool Unique;

        public abstract void Apply();
    }
}