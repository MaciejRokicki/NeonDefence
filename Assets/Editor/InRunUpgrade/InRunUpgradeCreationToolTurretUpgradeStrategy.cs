using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

namespace Assets.Scripts.InRunUpgrade
{
    public class InRunUpgradeCreationToolTurretUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        private readonly InRunUpgradeCreationTool inRunUpgradeCreationTool;

        //TODO: add effects
        private int selectedTurretId;
        private float damage;
        private float range;
        private float rotationSpeed;
        private float missilesPerSecond;
        private float missileSpeed;

        private float laserHitsPerSecond;
        private float laserActivationTime;
        private float laserDeactivationTime;

        private float slowdownEffectiveness;
        private float slowdownEffectDuration;

        private float poisonDamage;
        private float poisonHitRate;
        private float poisonDuration;

        private float explosionDamage;
        private float explosionRange;

        private float auraDamage;
        private float auraRange;

        private float auraSlowdownEffectiveness;

        public InRunUpgradeCreationToolTurretUpgradeStrategy(InRunUpgradeCreationTool inRunUpgradeCreationTool)
        {
            this.inRunUpgradeCreationTool = inRunUpgradeCreationTool;
        }

        public override void OnGui()
        {
            TurretScriptableObject turret = inRunUpgradeCreationTool.turrets[selectedTurretId];
            selectedTurretId = EditorGUILayout.Popup("Turret", selectedTurretId, inRunUpgradeCreationTool.turretNames);

            if(turret)
            {
                if (turret.aura)
                {            
                    auraDamage = EditorGUILayout.FloatField("Aura damage", auraDamage);
                    auraRange = EditorGUILayout.FloatField("Aura range", auraRange);
                    EditorGUILayout.Separator();

                    if (turret.auraSlowdown)
                    {
                        auraSlowdownEffectiveness = EditorGUILayout.FloatField("Aura slowdown effectiveness", auraSlowdownEffectiveness);
                    }
                }
                else
                {
                    damage = EditorGUILayout.FloatField("Damage", damage);
                    range = EditorGUILayout.FloatField("Range", range);
                    rotationSpeed = EditorGUILayout.FloatField("Rotation speed", rotationSpeed);

                    if (turret.missile)
                    {
                        missilesPerSecond = EditorGUILayout.FloatField("Missiles per second", missilesPerSecond);
                        missileSpeed = EditorGUILayout.FloatField("Missile speed", missileSpeed);
                    }

                    EditorGUILayout.Separator();

                    if (turret.laser)
                    {                   
                        laserHitsPerSecond = EditorGUILayout.FloatField("Laser hits per second", laserHitsPerSecond);
                        laserActivationTime = EditorGUILayout.FloatField("Laser activation time", laserActivationTime);
                        laserDeactivationTime = EditorGUILayout.FloatField("Laser deactivation time", laserDeactivationTime);
                        EditorGUILayout.Separator();
                    }

                    if (turret.slowdownMissile)
                    {
                        slowdownEffectiveness = EditorGUILayout.FloatField("Slowdown effectiveness", slowdownEffectiveness);
                        slowdownEffectDuration = EditorGUILayout.FloatField("Slowdown effect duration", slowdownEffectDuration);
                        EditorGUILayout.Separator();
                    }

                    if (turret.poisonMissile)
                    {
                        poisonDamage = EditorGUILayout.FloatField("Poison damage", poisonDamage);
                        poisonHitRate = EditorGUILayout.FloatField("Poison hit rate", poisonHitRate);
                        poisonDuration = EditorGUILayout.FloatField("Poison duration", poisonDuration);
                        EditorGUILayout.Separator();
                    }

                    if (turret.explosiveMissile)
                    {
                        explosionDamage = EditorGUILayout.FloatField("Explosion damage", explosionDamage);
                        explosionRange = EditorGUILayout.FloatField("Explosion range", explosionRange);
                        EditorGUILayout.Separator();
                    }
                }
            }
            else
            {
                damage = EditorGUILayout.FloatField("Damage", damage);
                range = EditorGUILayout.FloatField("Range", range);
                rotationSpeed = EditorGUILayout.FloatField("Rotation speed", rotationSpeed);
                missilesPerSecond = EditorGUILayout.FloatField("Missiles per second", missilesPerSecond);
                missileSpeed = EditorGUILayout.FloatField("Missile speed", missileSpeed);
                EditorGUILayout.Separator();
                laserHitsPerSecond = EditorGUILayout.FloatField("Laser hits per second", laserHitsPerSecond);
                laserActivationTime = EditorGUILayout.FloatField("Laser activation time", laserActivationTime);
                laserDeactivationTime = EditorGUILayout.FloatField("Laser deactivation time", laserDeactivationTime);
                EditorGUILayout.Separator();
                slowdownEffectiveness = EditorGUILayout.FloatField("Slowdown effectiveness", slowdownEffectiveness);
                slowdownEffectDuration = EditorGUILayout.FloatField("Slowdown effect duration", slowdownEffectDuration);
                EditorGUILayout.Separator();
                poisonDamage = EditorGUILayout.FloatField("Poison damage", poisonDamage);
                poisonHitRate = EditorGUILayout.FloatField("Poison hit rate", poisonHitRate);
                poisonDuration = EditorGUILayout.FloatField("Poison duration", poisonDuration);
                EditorGUILayout.Separator();
                explosionDamage = EditorGUILayout.FloatField("Explosion damage", explosionDamage);
                explosionRange = EditorGUILayout.FloatField("Explosion range", explosionRange);
                EditorGUILayout.Separator();
                auraDamage = EditorGUILayout.FloatField("Aura damage", auraDamage);
                auraRange = EditorGUILayout.FloatField("Aura range", auraRange);
                EditorGUILayout.Separator();
                auraSlowdownEffectiveness = EditorGUILayout.FloatField("Aura slowdown effectiveness", auraSlowdownEffectiveness);
            }
        }

        public override void Create(string upgradeName, bool unique, TierScriptableObject tier)
        {
            StringBuilder pathStringBuilder = new StringBuilder("Assets/ScriptableObjects/Upgrades/InRunUpgrades/");
            pathStringBuilder.Append(tier.Name);
            pathStringBuilder.Append(upgradeName);
            pathStringBuilder.Append("InRunUpgrade");
            pathStringBuilder.Append(".asset");

            InRunTurretUpgradeScriptableObject turretUpgradeScriptableObject = ScriptableObject.CreateInstance<InRunTurretUpgradeScriptableObject>();

            turretUpgradeScriptableObject.Tier = tier;
            turretUpgradeScriptableObject.Unique = unique;

            turretUpgradeScriptableObject.turret = inRunUpgradeCreationTool.turrets[selectedTurretId];
            turretUpgradeScriptableObject.damage = damage;
            turretUpgradeScriptableObject.range = range;
            turretUpgradeScriptableObject.rotationSpeed = rotationSpeed;
            turretUpgradeScriptableObject.missilesPerSecond = missilesPerSecond;
            turretUpgradeScriptableObject.missileSpeed = missileSpeed;

            turretUpgradeScriptableObject.laserHitsPerSecond = laserHitsPerSecond;
            turretUpgradeScriptableObject.laserActivationTime = laserActivationTime;
            turretUpgradeScriptableObject.laserDeactivationTime = laserDeactivationTime;

            turretUpgradeScriptableObject.slowdownEffectiveness = slowdownEffectiveness;
            turretUpgradeScriptableObject.slowdownEffectDuration = slowdownEffectDuration;

            turretUpgradeScriptableObject.poisonDamage = poisonDamage;
            turretUpgradeScriptableObject.poisonHitRate = poisonHitRate;
            turretUpgradeScriptableObject.poisonDuration = poisonDuration;

            turretUpgradeScriptableObject.explosionDamage = explosionDamage;
            turretUpgradeScriptableObject.explosionRange = explosionRange;

            turretUpgradeScriptableObject.auraDamage = auraDamage;
            turretUpgradeScriptableObject.auraRange = auraRange;

            turretUpgradeScriptableObject.auraSlowdownEffectiveness = auraSlowdownEffectiveness;

            AssetDatabase.CreateAsset(turretUpgradeScriptableObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
