using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

namespace Assets.Editor.Game.InRunUpgrade.CreationTool.Strategies
{
    public class InRunUpgradeCreationToolTurretUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        private readonly InRunUpgradeCreationTool inRunUpgradeCreationTool;
        private int selectedTurretId;

        private bool poisonMissile;
        private bool explosiveMissile;
        private bool slowdownMissile;
        private bool penetrationMissile;
        private bool trackingMissile;

        private bool auraSlowdown;

        private float damage;
        private bool damageIsPercentage;
        private float range;
        private bool rangeIsPercentage;
        private float rotationSpeed;
        private bool rotationSpeedIsPercentage;
        private float missilesPerSecond;
        private bool missilesPerSecondIsPercentage;
        private float missileSpeed;
        private bool missileSpeedIsPercentage;

        private float laserHitsPerSecond;
        private bool laserHitsPerSecondIsPercentage;
        private float laserActivationTime;
        private bool laserActivationTimeIsPercentage;
        private float laserDeactivationTime;
        private bool laserDeactivationTimeIsPercentage;

        private float slowdownEffectiveness;
        private bool slowdownEffectivenessIsPercentage;
        private float slowdownEffectDuration;
        private bool slowdownEffectDurationIsPercentage;

        private float poisonDamage;
        private bool poisonDamageIsPercentage;
        private float poisonHitRate;
        private bool poisonHitRateIsPercentage;
        private float poisonDuration;
        private bool poisonDurationIsPercentage;

        private GameObject explosionPrefab;
        private Sprite explosionSprite;
        private Material explosionMaterial;
        private float explosionDamage;
        private bool explosionDamageIsPercentage;
        private float explosionRange;
        private bool explosionRangeIsPercentage;
        private bool explosionCopyMissileEffects;

        private float auraDamage;
        private bool auraDamageIsPercentage;
        private float auraRange;
        private bool auraRangeIsPercentage;

        private float auraSlowdownEffectiveness;
        private bool auraSlowdownEffectivenessIsPercentage;

        public InRunUpgradeCreationToolTurretUpgradeStrategy(InRunUpgradeCreationTool inRunUpgradeCreationTool, UpgradeManager upgradeManager) : base(upgradeManager)
        {
            this.inRunUpgradeCreationTool = inRunUpgradeCreationTool;
        }

        public override void OnGui()
        {
            TurretScriptableObject turret = inRunUpgradeCreationTool.turrets[selectedTurretId];
            selectedTurretId = EditorGUILayout.Popup("Turret", selectedTurretId, inRunUpgradeCreationTool.turretNames);

            if (turret)
            {
                if (turret.Aura)
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretAuraSection(
                        ref auraSlowdown,
                        ref auraDamage,
                        ref auraDamageIsPercentage,
                        ref auraRange,
                        ref auraRangeIsPercentage);

                    if (turret.AuraSlowdown || auraSlowdown)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretAuraSlowdownSection(
                            ref auraSlowdownEffectiveness,
                            ref auraSlowdownEffectivenessIsPercentage);
                    }
                }
                else
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretSection(
                        ref poisonMissile,
                        ref slowdownMissile,
                        ref explosiveMissile,
                        ref penetrationMissile,
                        ref trackingMissile,
                        ref damage,
                        ref damageIsPercentage,
                        ref range,
                        ref rangeIsPercentage,
                        ref rotationSpeed,
                        ref rotationSpeedIsPercentage);

                    if (turret.Missile)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretMissileSection(
                            ref missilesPerSecond,
                            ref missilesPerSecondIsPercentage,
                            ref missileSpeed,
                            ref missileSpeedIsPercentage);
                    }

                    if (turret.Laser)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretLaserSection(
                            ref laserHitsPerSecond,
                            ref laserHitsPerSecondIsPercentage,
                            ref laserActivationTime,
                            ref laserActivationTimeIsPercentage,
                            ref laserDeactivationTime,
                            ref laserDeactivationTimeIsPercentage);
                    }

                    if (turret.PoisonMissile || poisonMissile)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretPoisionSection(
                            ref poisonDamage,
                            ref poisonDamageIsPercentage,
                            ref poisonHitRate,
                            ref poisonHitRateIsPercentage,
                            ref poisonDuration,
                            ref poisonDurationIsPercentage);
                    }

                    if (turret.SlowdownMissile || slowdownMissile)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretSlowdownSection(
                            ref slowdownEffectiveness,
                            ref slowdownEffectivenessIsPercentage,
                            ref slowdownEffectDuration,
                            ref slowdownEffectDurationIsPercentage);
                    }

                    if (turret.xplosiveMissile || explosiveMissile)
                    {
                        InRunUpgradeScriptableObjectEditorGUI.TurretExplosionSection(
                            ref explosionPrefab,
                            ref explosionSprite,
                            ref explosionMaterial,
                            ref explosionDamage,
                            ref explosionDamageIsPercentage,
                            ref explosionRange,
                            ref explosionRangeIsPercentage,
                            ref explosionCopyMissileEffects);
                    }
                }
            }
            else
            {
                InRunUpgradeScriptableObjectEditorGUI.TurretAuraSection(
                    ref auraSlowdown,
                    ref auraDamage,
                    ref auraDamageIsPercentage,
                    ref auraRange,
                    ref auraRangeIsPercentage);

                if (auraSlowdown)
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretAuraSlowdownSection(
                        ref auraSlowdownEffectiveness,
                        ref auraSlowdownEffectivenessIsPercentage);
                }

                InRunUpgradeScriptableObjectEditorGUI.TurretSection(
                    ref poisonMissile,
                    ref slowdownMissile,
                    ref explosiveMissile,
                    ref penetrationMissile,
                    ref trackingMissile,
                    ref damage,
                    ref damageIsPercentage,
                    ref range,
                    ref rangeIsPercentage,
                    ref rotationSpeed,
                    ref rotationSpeedIsPercentage);


                InRunUpgradeScriptableObjectEditorGUI.TurretMissileSection(
                    ref missilesPerSecond,
                    ref missilesPerSecondIsPercentage,
                    ref missileSpeed,
                    ref missileSpeedIsPercentage);

                InRunUpgradeScriptableObjectEditorGUI.TurretLaserSection(
                    ref laserHitsPerSecond,
                    ref laserHitsPerSecondIsPercentage,
                    ref laserActivationTime,
                    ref laserActivationTimeIsPercentage,
                    ref laserDeactivationTime,
                    ref laserDeactivationTimeIsPercentage);

                if (poisonMissile)
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretPoisionSection(
                        ref poisonDamage,
                        ref poisonDamageIsPercentage,
                        ref poisonHitRate,
                        ref poisonHitRateIsPercentage,
                        ref poisonDuration,
                        ref poisonDurationIsPercentage);
                }

                if (slowdownMissile)
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretSlowdownSection(
                        ref slowdownEffectiveness,
                        ref slowdownEffectivenessIsPercentage,
                        ref slowdownEffectDuration,
                        ref slowdownEffectDurationIsPercentage);
                }

                if (explosiveMissile)
                {
                    InRunUpgradeScriptableObjectEditorGUI.TurretExplosionSection(
                        ref explosionPrefab,
                        ref explosionSprite,
                        ref explosionMaterial,
                        ref explosionDamage,
                        ref explosionDamageIsPercentage,
                        ref explosionRange,
                        ref explosionRangeIsPercentage,
                        ref explosionCopyMissileEffects);
                }
            }
        }

        public override void Create(string upgradeName, bool unique, TierScriptableObject tier, string description)
        {
            StringBuilder pathStringBuilder = new StringBuilder("Assets/ScriptableObjects/Upgrades/InRunUpgrades/");
            pathStringBuilder.Append(upgradeName);
            pathStringBuilder.Append(".asset");

            InRunTurretUpgradeScriptableObject turretUpgradeScriptableObject = ScriptableObject.CreateInstance<InRunTurretUpgradeScriptableObject>();

            turretUpgradeScriptableObject.Tier = tier;
            turretUpgradeScriptableObject.Unique = unique;

            turretUpgradeScriptableObject.Turret = inRunUpgradeCreationTool.turrets[selectedTurretId];

            turretUpgradeScriptableObject.Damage = damage;
            turretUpgradeScriptableObject.DamageIsPercentage = damageIsPercentage;
            turretUpgradeScriptableObject.Range = range;
            turretUpgradeScriptableObject.RangeIsPercentage = rangeIsPercentage;
            turretUpgradeScriptableObject.RotationSpeed = rotationSpeed;
            turretUpgradeScriptableObject.RotationSpeedIsPercentage = rotationSpeedIsPercentage;
            turretUpgradeScriptableObject.MissilesPerSecond = missilesPerSecond;
            turretUpgradeScriptableObject.MissilesPerSecondIsPercentage = missilesPerSecondIsPercentage;
            turretUpgradeScriptableObject.MissileSpeed = missileSpeed;
            turretUpgradeScriptableObject.MissileSpeedIsPercentage = missileSpeedIsPercentage;

            turretUpgradeScriptableObject.LaserHitsPerSecond = laserHitsPerSecond;
            turretUpgradeScriptableObject.LaserHitsPerSecondIsPercentage = laserHitsPerSecondIsPercentage;
            turretUpgradeScriptableObject.LaserActivationTime = laserActivationTime;
            turretUpgradeScriptableObject.LaserActivationTimeIsPercentage = laserActivationTimeIsPercentage;
            turretUpgradeScriptableObject.LaserDeactivationTime = laserDeactivationTime;
            turretUpgradeScriptableObject.LaserDeactivationTimeIsPercentage = laserDeactivationTimeIsPercentage;

            turretUpgradeScriptableObject.PoisonMissile = poisonMissile;
            turretUpgradeScriptableObject.PoisonDamage = poisonDamage;
            turretUpgradeScriptableObject.PoisonDamageIsPercentage = poisonDamageIsPercentage;
            turretUpgradeScriptableObject.PoisonHitRate = poisonHitRate;
            turretUpgradeScriptableObject.PoisonHitRateIsPercentage = poisonHitRateIsPercentage;
            turretUpgradeScriptableObject.PoisonDuration = poisonDuration;
            turretUpgradeScriptableObject.PoisonDurationIsPercentage = poisonDurationIsPercentage;

            turretUpgradeScriptableObject.SlowdownMissile = slowdownMissile;
            turretUpgradeScriptableObject.SlowdownEffectiveness = slowdownEffectiveness;
            turretUpgradeScriptableObject.SlowdownEffectivenessIsPercentage = slowdownEffectivenessIsPercentage;
            turretUpgradeScriptableObject.SlowdownEffectDurationIsPercentage = slowdownEffectDurationIsPercentage;

            turretUpgradeScriptableObject.ExplosiveMissile = explosiveMissile;
            turretUpgradeScriptableObject.ExplosionPrefab = explosionPrefab;
            turretUpgradeScriptableObject.ExplosionSprite = explosionSprite;
            turretUpgradeScriptableObject.ExplosionMaterial = explosionMaterial;
            turretUpgradeScriptableObject.ExplosionDamage = explosionDamage;
            turretUpgradeScriptableObject.ExplosionDamageIsPercentage = explosionDamageIsPercentage;
            turretUpgradeScriptableObject.ExplosionRange = explosionRange;
            turretUpgradeScriptableObject.ExplosionRangeIsPercentage = explosionRangeIsPercentage;
            turretUpgradeScriptableObject.ExplosionCopyMissileEffects = explosionCopyMissileEffects;

            turretUpgradeScriptableObject.TrackingMissile = trackingMissile;
            turretUpgradeScriptableObject.PenetrationMissile = penetrationMissile;

            turretUpgradeScriptableObject.AuraDamage = auraDamage;
            turretUpgradeScriptableObject.AuraDamageIsPercentage = auraDamageIsPercentage;
            turretUpgradeScriptableObject.AuraRange = auraRange;
            turretUpgradeScriptableObject.AuraRangeIsPercentage = auraRangeIsPercentage;

            turretUpgradeScriptableObject.AuraSlowdownEffectiveness = auraSlowdownEffectiveness;
            turretUpgradeScriptableObject.AuraSlowdownEffectivenessIsPercentage = auraSlowdownEffectivenessIsPercentage;

            turretUpgradeScriptableObject.Description = description;

            AssetDatabase.CreateAsset(turretUpgradeScriptableObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            upgradeManager.AddUpgrade(turretUpgradeScriptableObject);
        }
    }
}
