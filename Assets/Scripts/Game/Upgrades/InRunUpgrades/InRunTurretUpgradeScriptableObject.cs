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
        public bool trackingMissile;
        public bool penetrationMissile;

        public float damage;
        public float range;
        public float rotationSpeed;
        public float missilesPerSecond;
        public float missileSpeed;

        public float laserHitsPerSecond;
        public float laserActivationTime;
        public float laserDeactivationTime;

        public float slowdownEffectiveness;
        public float slowdownEffectDuration;

        public float poisonDamage;
        public float poisonHitRate;
        public float poisonDuration;

        public GameObject explosionPrefab;
        public Sprite explosionSprite;
        public Material explosionMaterial;
        public float explosionDamage;
        public float explosionRange;
        public bool explosionCopyMissileEffects;

        public float auraDamage;
        public float auraRange;

        public float auraSlowdownEffectiveness;

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
                turret.SetProperites();
            }
        }

        private void UpdateTurretProperties(TurretScriptableObject turret)
        {
            if (turret.aura)
            {
                turret.auraDamage += auraDamage;
                turret.auraRange += auraRange;

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
                    if(poisonMissile)
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
                    if(turret.explosionPrefab == null)
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
    }
}
