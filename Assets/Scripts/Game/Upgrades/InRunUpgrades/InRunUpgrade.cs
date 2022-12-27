using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    public abstract class InRunUpgrade : ScriptableObject
    {
        public TierScriptableObject Tier;
        public bool Unique;
        [TextArea]
        public string Description;

        public abstract void Apply();
    }
}