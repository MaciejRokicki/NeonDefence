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
                turret.AuraDamage += CalculatePropertyPercentage(turret.AuraDamage, AuraDamage, AuraDamageIsPercentage); ;
                turret.AuraRange += CalculatePropertyPercentage(turret.AuraRange, AuraRange, AuraRangeIsPercentage); ;

                turret.AuraSlowdown = turret.AuraSlowdown || AuraSlowdown;

                if (turret.AuraSlowdown)
                {
                    turret.AuraSlowdownEffectiveness += CalculatePropertyPercentage(turret.AuraSlowdownEffectiveness, AuraSlowdownEffectiveness, AuraSlowdownEffectivenessIsPercentage);
                }
            }
            else
            {
                turret.PoisonMissile = turret.PoisonMissile || PoisonMissile;
                turret.xplosiveMissile = turret.xplosiveMissile || ExplosiveMissile;
                turret.SlowdownMissile = turret.SlowdownMissile || SlowdownMissile;
                turret.TrackingMissile = turret.TrackingMissile || TrackingMissile;
                turret.PenetrationMissile = turret.PenetrationMissile || PenetrationMissile;

                turret.Damage += CalculatePropertyPercentage(turret.Damage, Damage, DamageIsPercentage); ;
                turret.Range += CalculatePropertyPercentage(turret.Range, Range, RangeIsPercentage); ;
                turret.RotationSpeed += CalculatePropertyPercentage(turret.RotationSpeed, RotationSpeed, RotationSpeedIsPercentage);

                if (turret.Missile)
                {
                    turret.MissilesPerSecond += CalculatePropertyPercentage(turret.MissilesPerSecond, MissilesPerSecond, MissilesPerSecondIsPercentage);
                    turret.MissileSpeed += CalculatePropertyPercentage(turret.MissileSpeed, MissileSpeed, MissileSpeedIsPercentage);
                }

                if (turret.Laser)
                {
                    turret.LaserHitsPerSecond += CalculatePropertyPercentage(turret.LaserHitsPerSecond, LaserHitsPerSecond, LaserHitsPerSecondIsPercentage);
                    turret.LaserActivationTime -= CalculatePropertyPercentage(turret.LaserActivationTime, LaserActivationTime, LaserActivationTimeIsPercentage);
                    turret.LaserDeactivationTime -= CalculatePropertyPercentage(turret.LaserDeactivationTime, LaserDeactivationTime, LaserDeactivationTimeIsPercentage);
                }

                if (turret.PoisonMissile)
                {
                    turret.PoisonDamage += CalculatePropertyPercentage(turret.PoisonDamage, PoisonDamage, PoisonDamageIsPercentage);
                    if (PoisonMissile)
                    {
                        turret.PoisonHitRate = CalculatePropertyPercentage(turret.PoisonHitRate, PoisonHitRate, PoisonHitRateIsPercentage);
                    }
                    else
                    {
                        turret.PoisonHitRate -= CalculatePropertyPercentage(turret.PoisonHitRate, PoisonHitRate, PoisonHitRateIsPercentage);
                    }
                    turret.PoisonDuration += CalculatePropertyPercentage(turret.PoisonDuration, PoisonDuration, PoisonDurationIsPercentage);
                }

                if (turret.SlowdownMissile)
                {
                    turret.SlowdownEffectiveness += CalculatePropertyPercentage(turret.SlowdownEffectiveness, SlowdownEffectiveness, SlowdownEffectivenessIsPercentage);
                    turret.SlowdownEffectDuration += CalculatePropertyPercentage(turret.SlowdownEffectDuration, SlowdownEffectDuration, SlowdownEffectDurationIsPercentage);
                }

                if (turret.xplosiveMissile)
                {
                    if (turret.ExplosionPrefab == null)
                    {
                        turret.ExplosionPrefab = ExplosionPrefab;
                        turret.ExplosionSprite = ExplosionSprite;
                        turret.ExplosionMaterial = ExplosionMaterial;
                    }

                    turret.ExplosionDamage += CalculatePropertyPercentage(turret.ExplosionDamage, ExplosionDamage, ExplosionDamageIsPercentage);
                    turret.ExplosionRange += CalculatePropertyPercentage(turret.ExplosionRange, ExplosionRange, ExplosionRangeIsPercentage);
                    turret.CopyMissileEffects = turret.CopyMissileEffects || ExplosionCopyMissileEffects;
                }
            }
        }

        public override void Apply()
        {
            if (Turret)
            {
                SetTurretProperties(Turret);
            }
            else
            {
                foreach (TurretScriptableObject turret in TurretManager.instance.turretVariants)
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
