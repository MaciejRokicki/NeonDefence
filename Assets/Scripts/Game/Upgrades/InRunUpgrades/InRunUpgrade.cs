using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InGameUpgrades
{
    public enum InRunUpgradeTier
    {
        Basic = 0,
        Fine = 1,
        Superior = 2,
        Epic = 3,
        Legendary = 4
    }

    public abstract class InRunUpgrade : ScriptableObject
    {
        public InRunUpgradeTier tier;

        public abstract void Apply();
    }
}