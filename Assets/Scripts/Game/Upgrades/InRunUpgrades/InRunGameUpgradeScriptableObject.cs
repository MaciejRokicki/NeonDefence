﻿using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InRunGameUpgrade", order = 5)]
    public class InRunGameUpgradeScriptableObject : InRunUpgrade
    {
        private GameManager gameManager;

        public float health;
        public float maxHealth;
        public bool increaseHealthToo;
        public int neonBlocks;

        public override void Apply()
        {
            gameManager = GameManager.instance;

            gameManager.IncreaseHealth(health);
            gameManager.IncreaseMaxHealth(maxHealth, increaseHealthToo);
            gameManager.AddNeonBlocks(neonBlocks);
        }
    }
}
