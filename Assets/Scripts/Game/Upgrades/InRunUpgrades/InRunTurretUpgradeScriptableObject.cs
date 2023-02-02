using UnityEngine;

namespace Assets.Scripts.Game.Upgrades.InRunUpgrades
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InRunTurretUpgrade", order = 6)]
    public class InRunTurretUpgradeScriptableObject : InRunUpgradeScriptableObject
    {
        #nullable enable
        public TurretScriptableObject? Turret;
        #nullable disable

        public bool PoisonMissile;
        public bool ExplosiveMissile;
        public bool SlowdownMissile;
        public bool PenetrationMissile;
        public bool TrackingMissile;

        public bool AuraSlowdown;

        public float Damage;
        public bool DamageIsPercentage;
        public float Range;
        public bool RangeIsPercentage;
        public float RotationSpeed;
        public bool RotationSpeedIsPercentage;
        public float MissilesPerSecond;
        public bool MissilesPerSecondIsPercentage;
        public float MissileSpeed;
        public bool MissileSpeedIsPercentage;

        public float LaserHitsPerSecond;
        public bool LaserHitsPerSecondIsPercentage;
        public float LaserActivationTime;
        public bool LaserActivationTimeIsPercentage;
        public float LaserDeactivationTime;
        public bool LaserDeactivationTimeIsPercentage;

        public float SlowdownEffectiveness;
        public bool SlowdownEffectivenessIsPercentage;
        public float SlowdownEffectDuration;
        public bool SlowdownEffectDurationIsPercentage;

        public float PoisonDamage;
        public bool PoisonDamageIsPercentage;
        public float PoisonHitRate;
        public bool PoisonHitRateIsPercentage;
        public float PoisonDuration;
        public bool PoisonDurationIsPercentage;

        public GameObject ExplosionPrefab;
        public Sprite ExplosionSprite;
        public Material ExplosionMaterial;
        public float ExplosionDamage;
        public bool ExplosionDamageIsPercentage;
        public float ExplosionRange;
        public bool ExplosionRangeIsPercentage;
        public bool ExplosionCopyMissileEffects;

        public float AuraDamage;
        public bool AuraDamageIsPercentage;
        public float AuraRange;
        public bool AuraRangeIsPercentage;

        public float AuraSlowdownEffectiveness;
        public bool AuraSlowdownEffectivenessIsPercentage;

        private void SetTurretProperties(TurretScriptableObject turret)
        {
            if (turret.Aura)
            {
                turret.AuraDamage += CalculatePropertyPercentage(turret.GetBaseAuraDamage(), AuraDamage, AuraDamageIsPercentage);
                turret.AuraRange += CalculatePropertyPercentage(turret.GetBaseAuraRange(), AuraRange, AuraRangeIsPercentage);

                turret.AuraSlowdown = turret.AuraSlowdown || AuraSlowdown;

                if (turret.AuraSlowdown)
                {
                    turret.AuraSlowdownEffectiveness += CalculatePropertyPercentage(turret.GetBaseAuraSlowdownEffectiveness(), AuraSlowdownEffectiveness, AuraSlowdownEffectivenessIsPercentage);
                }
            }
            else
            {
                turret.PoisonMissile = turret.PoisonMissile || PoisonMissile;
                turret.explosiveMissile = turret.explosiveMissile || ExplosiveMissile;
                turret.SlowdownMissile = turret.SlowdownMissile || SlowdownMissile;
                turret.TrackingMissile = turret.TrackingMissile || TrackingMissile;
                turret.PenetrationMissile = turret.PenetrationMissile || PenetrationMissile;

                turret.Damage += CalculatePropertyPercentage(turret.GetBaseDamage(), Damage, DamageIsPercentage);
                turret.Range += CalculatePropertyPercentage(turret.GetBaseRange(), Range, RangeIsPercentage);
                turret.RotationSpeed += CalculatePropertyPercentage(turret.GetBaseRotationSpeed(), RotationSpeed, RotationSpeedIsPercentage);

                if (turret.Missile)
                {
                    turret.MissilesPerSecond += CalculatePropertyPercentage(turret.GetBaseMissilesPerSecond(), MissilesPerSecond, MissilesPerSecondIsPercentage);
                    turret.MissileSpeed += CalculatePropertyPercentage(turret.GetBaseMissileSpeed(), MissileSpeed, MissileSpeedIsPercentage);
                }

                if (turret.Laser)
                {
                    turret.LaserHitsPerSecond += CalculatePropertyPercentage(turret.GetBaseLaserHitsPerSecond(), LaserHitsPerSecond, LaserHitsPerSecondIsPercentage);
                    turret.LaserActivationTime -= CalculatePropertyPercentage(turret.GetBaseLaserActivationTime(), LaserActivationTime, LaserActivationTimeIsPercentage);
                    turret.LaserDeactivationTime -= CalculatePropertyPercentage(turret.GetBaseLaserDeactivationTime(), LaserDeactivationTime, LaserDeactivationTimeIsPercentage);
                }

                if (turret.PoisonMissile)
                {
                    turret.PoisonDamage += CalculatePropertyPercentage(turret.GetBasePoisonDamage(), PoisonDamage, PoisonDamageIsPercentage);
                    if (PoisonMissile)
                    {
                        turret.PoisonHitRate = CalculatePropertyPercentage(turret.GetBasePoisonHitRate(), PoisonHitRate, PoisonHitRateIsPercentage);
                    }
                    else
                    {
                        turret.PoisonHitRate -= CalculatePropertyPercentage(turret.GetBasePoisonHitRate(), PoisonHitRate, PoisonHitRateIsPercentage);
                    }
                    turret.PoisonDuration += CalculatePropertyPercentage(turret.GetBasePoisonDuration(), PoisonDuration, PoisonDurationIsPercentage);
                }

                if (turret.SlowdownMissile)
                {
                    turret.SlowdownEffectiveness += CalculatePropertyPercentage(turret.GetBaseSlowdownEffectiveness(), SlowdownEffectiveness, SlowdownEffectivenessIsPercentage);
                    turret.SlowdownEffectDuration += CalculatePropertyPercentage(turret.GetBaseSlowdownEffectDuration(), SlowdownEffectDuration, SlowdownEffectDurationIsPercentage);
                }

                if (turret.explosiveMissile)
                {
                    if (turret.ExplosionPrefab == null)
                    {
                        turret.ExplosionPrefab = ExplosionPrefab;
                        turret.ExplosionSprite = ExplosionSprite;
                        turret.ExplosionMaterial = ExplosionMaterial;
                    }

                    turret.ExplosionDamage += CalculatePropertyPercentage(turret.GetBaseExplosionDamage(), ExplosionDamage, ExplosionDamageIsPercentage);
                    turret.ExplosionRange += CalculatePropertyPercentage(turret.GetBaseExplosionRange(), ExplosionRange, ExplosionRangeIsPercentage);
                    turret.CopyMissileEffects = turret.CopyMissileEffects || ExplosionCopyMissileEffects;
                }
            }
        }

        public override void Apply()
        {
            StatisticsManager.instance.AddPickedUpgradesCount();

            if (Turret)
            {
                SetTurretProperties(Turret);
            }
            else
            {
                foreach (TurretScriptableObject turret in TurretManager.Instance.TurretVariants)
                {
                    SetTurretProperties(turret);
                }
            }

            foreach (Turret turret in GameObject.Find("Turrets").GetComponentsInChildren<Turret>())
            {
                turret.UpdateProperties();
            }
        }
    }
}
