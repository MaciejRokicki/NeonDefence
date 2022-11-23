using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InGameUpgrades
{
    public abstract class InRunUpgrade : ScriptableObject
    {
        public TierScriptableObject tier;

        public abstract void Apply();
    }
}