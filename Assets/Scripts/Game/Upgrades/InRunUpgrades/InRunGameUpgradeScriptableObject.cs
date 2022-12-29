using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InRunGameUpgrade", order = 5)]
    public class InRunGameUpgradeScriptableObject : InRunUpgradeScriptableObject
    {
        private GameManager gameManager;

        public float Health;
        public bool HealthIsPercentage;
        public float MaxHealth;
        public bool MaxHealthIsPercentage;
        public bool IncreaseHealthToo;
        public int NeonBlocks;

        public override void Apply()
        {
            gameManager = GameManager.instance;

            gameManager.IncreaseHealth(CalculatePropertyPercentage(gameManager.Health, Health, HealthIsPercentage));
            gameManager.IncreaseMaxHealth(CalculatePropertyPercentage(gameManager.MaxHealth, MaxHealth, MaxHealthIsPercentage), IncreaseHealthToo);
            gameManager.AddNeonBlocks(NeonBlocks);
        }
    }
}
