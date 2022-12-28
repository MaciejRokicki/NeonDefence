using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InRunTurretUpgrade", order = 6)]
    public class InRunTurretUpgradeScriptableObject : InRunUpgrade
    {
        #nullable enable
        public TurretScriptableObject? turret;
        #nullable disable

        public bool poisonMissile;
        public bool explosiveMissile;
        public bool slowdownMissile;
        public bool penetrationMissile;
        public bool trackingMissile;

        public bool auraSlowdown;

        public float damage;
        public bool damageIsPercentage;
        public float range;
        public bool rangeIsPercentage;
        public float rotationSpeed;
        public bool rotationSpeedIsPercentage;
        public float missilesPerSecond;
        public bool missilesPerSecondIsPercentage;
        public float missileSpeed;
        public bool missileSpeedIsPercentage;

        public float laserHitsPerSecond;
        public bool laserHitsPerSecondIsPercentage;
        public float laserActivationTime;
        public bool laserActivationTimeIsPercentage;
        public float laserDeactivationTime;
        public bool laserDeactivationTimeIsPercentage;

        public float slowdownEffectiveness;
        public bool slowdownEffectivenessIsPercentage;
        public float slowdownEffectDuration;
        public bool slowdownEffectDurationIsPercentage;

        public float poisonDamage;
        public bool poisonDamageIsPercentage;
        public float poisonHitRate;
        public bool poisonHitRateIsPercentage;
        public float poisonDuration;
        public bool poisonDurationIsPercentage;

        public GameObject explosionPrefab;
        public Sprite explosionSprite;
        public Material explosionMaterial;
        public float explosionDamage;
        public bool explosionDamageIsPercentage;
        public float explosionRange;
        public bool explosionRangeIsPercentage;
        public bool explosionCopyMissileEffects;

        public float auraDamage;
        public bool auraDamageIsPercentage;
        public float auraRange;
        public bool auraRangeIsPercentage;

        public float auraSlowdownEffectiveness;
        public bool auraSlowdownEffectivenessIsPercentage;

        public override void Apply()
        {
            if(turret)
            {
                UpdateTurretProperties(turret);
            }
            else
            {
                foreach (TurretScriptableObject turret in TurretManager.instance.turretVariants)
                {
                    UpdateTurretProperties(turret);
                }
            }

            foreach (Turret turret in GameObject.Find("Turrets").GetComponentsInChildren<Turret>())
            {
                turret.UpdateProperties();
            }
        }

        private void CalculateTurretProperties(TurretScriptableObject turret)
        {
            void CalculateProperty(TurretScriptableObject turret, ref float property, bool isPercentage)
            {
                if(isPercentage)
                {
                    float turretValue = (float)turret
                        .GetType()
                        .GetProperty(property.GetType().Name)
                        .GetValue(turret);

                    property = property * turretValue;
                }
            }

            CalculateProperty(turret, ref damage, damageIsPercentage);
            CalculateProperty(turret, ref range, rangeIsPercentage);
            CalculateProperty(turret, ref rotationSpeed, rotationSpeedIsPercentage);

            CalculateProperty(turret, ref missilesPerSecond, missilesPerSecondIsPercentage);
            CalculateProperty(turret, ref missileSpeed, missileSpeedIsPercentage);

            CalculateProperty(turret, ref laserHitsPerSecond, laserHitsPerSecondIsPercentage);
            CalculateProperty(turret, ref laserActivationTime, laserActivationTimeIsPercentage);
            CalculateProperty(turret, ref laserDeactivationTime, laserDeactivationTimeIsPercentage);

            CalculateProperty(turret, ref slowdownEffectiveness, slowdownEffectivenessIsPercentage);
            CalculateProperty(turret, ref slowdownEffectDuration, slowdownEffectDurationIsPercentage);

            CalculateProperty(turret, ref poisonDamage, poisonDamageIsPercentage);
            CalculateProperty(turret, ref poisonHitRate, poisonHitRateIsPercentage);
            CalculateProperty(turret, ref poisonDuration, poisonDurationIsPercentage);

            CalculateProperty(turret, ref explosionDamage, explosionDamageIsPercentage);
            CalculateProperty(turret, ref explosionRange, explosionRangeIsPercentage);

            CalculateProperty(turret, ref auraDamage, auraDamageIsPercentage);
            CalculateProperty(turret, ref auraRange, auraRangeIsPercentage);

            CalculateProperty(turret, ref auraSlowdownEffectiveness, auraSlowdownEffectivenessIsPercentage);
        }

        private void SetTurretProperties(TurretScriptableObject turret)
        {
            if (turret.aura)
            {
                turret.auraDamage += auraDamage;
                turret.auraRange += auraRange;

                turret.auraSlowdown = turret.auraSlowdown || auraSlowdown;

                if (turret.auraSlowdown)
                {
                    turret.auraSlowdownEffectiveness += auraSlowdownEffectiveness;
                }
            }
            else
            {
                turret.poisonMissile = turret.poisonMissile || poisonMissile;
                turret.explosiveMissile = turret.explosiveMissile || explosiveMissile;
                turret.slowdownMissile = turret.slowdownMissile || slowdownMissile;
                turret.trackingMissile = turret.trackingMissile || trackingMissile;
                turret.penetrationMissile = turret.penetrationMissile || penetrationMissile;

                turret.damage += damage;
                turret.range += range;
                turret.rotationSpeed += rotationSpeed;

                if (turret.missile)
                {
                    turret.missilesPerSecond += missilesPerSecond;
                    turret.missileSpeed += missileSpeed;
                }

                if (turret.laser)
                {
                    turret.laserHitsPerSecond += laserHitsPerSecond;
                    turret.laserActivationTime -= laserActivationTime;
                    turret.laserDeactivationTime -= laserDeactivationTime;
                }

                if (turret.poisonMissile)
                {
                    turret.poisonDamage += poisonDamage;
                    if (poisonMissile)
                    {
                        turret.poisonHitRate = poisonHitRate;
                    }
                    else
                    {
                        turret.poisonHitRate -= poisonHitRate;
                    }
                    turret.poisonDuration += poisonDuration;
                }

                if (turret.slowdownMissile)
                {
                    turret.slowdownEffectiveness += slowdownEffectiveness;
                    turret.slowdownEffectDuration += slowdownEffectDuration;
                }

                if (turret.explosiveMissile)
                {
                    if (turret.explosionPrefab == null)
                    {
                        turret.explosionPrefab = explosionPrefab;
                        turret.explosionSprite = explosionSprite;
                        turret.explosionMaterial = explosionMaterial;
                    }

                    turret.explosionDamage += explosionDamage;
                    turret.explosionRange += explosionRange;
                    turret.copyMissileEffects = turret.copyMissileEffects || explosionCopyMissileEffects;
                }
            }
        }

        private void UpdateTurretProperties(TurretScriptableObject turret)
        {
            CalculateTurretProperties(turret);
            SetTurretProperties(turret);
        }
    }
}
