using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

namespace Assets.Scripts.InRunUpgrade
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

            if(turret)
            {
                if (turret.aura)
                {
                    TurretAuraSection();

                    if (turret.auraSlowdown || auraSlowdown)
                    {
                        TurretAuraSlowdownSection();
                    }
                }
                else
                {
                    TurretSection();

                    if (turret.missile)
                    {
                        TurretMissileSection();
                    }

                    if (turret.laser)
                    {
                        TurretLaserSection();
                    }

                    if (turret.poisonMissile || poisonMissile)
                    {
                        TurretPoisionSection();
                    }             

                    if (turret.slowdownMissile || slowdownMissile)
                    {
                        TurretSlowdownSection();
                    }                    

                    if (turret.explosiveMissile || explosiveMissile)
                    {
                        TurretExplosionSection();
                    }
                }
            }
            else
            {
                TurretAuraSection();

                if (auraSlowdown)
                {
                    TurretAuraSlowdownSection();
                }

                TurretSection();
                TurretMissileSection();
                TurretLaserSection();

                if (poisonMissile)
                {
                    TurretPoisionSection();
                }

                if (slowdownMissile)
                {
                    TurretSlowdownSection();
                }

                if (explosiveMissile)
                {
                    TurretExplosionSection();
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

            turretUpgradeScriptableObject.turret = inRunUpgradeCreationTool.turrets[selectedTurretId];

            turretUpgradeScriptableObject.damage = damage;
            turretUpgradeScriptableObject.damageIsPercentage = damageIsPercentage;
            turretUpgradeScriptableObject.range = range;
            turretUpgradeScriptableObject.rangeIsPercentage = rangeIsPercentage;
            turretUpgradeScriptableObject.rotationSpeed = rotationSpeed;
            turretUpgradeScriptableObject.rotationSpeedIsPercentage = rotationSpeedIsPercentage;
            turretUpgradeScriptableObject.missilesPerSecond = missilesPerSecond;
            turretUpgradeScriptableObject.missilesPerSecondIsPercentage = missilesPerSecondIsPercentage;
            turretUpgradeScriptableObject.missileSpeed = missileSpeed;
            turretUpgradeScriptableObject.missileSpeedIsPercentage = missileSpeedIsPercentage;

            turretUpgradeScriptableObject.laserHitsPerSecond = laserHitsPerSecond;
            turretUpgradeScriptableObject.laserHitsPerSecondIsPercentage = laserHitsPerSecondIsPercentage;
            turretUpgradeScriptableObject.laserActivationTime = laserActivationTime;
            turretUpgradeScriptableObject.laserActivationTimeIsPercentage = laserActivationTimeIsPercentage;
            turretUpgradeScriptableObject.laserDeactivationTime = laserDeactivationTime;
            turretUpgradeScriptableObject.laserDeactivationTimeIsPercentage = laserDeactivationTimeIsPercentage;

            turretUpgradeScriptableObject.poisonMissile = poisonMissile;
            turretUpgradeScriptableObject.poisonDamage = poisonDamage;
            turretUpgradeScriptableObject.poisonDamageIsPercentage = poisonDamageIsPercentage;
            turretUpgradeScriptableObject.poisonHitRate = poisonHitRate;
            turretUpgradeScriptableObject.poisonHitRateIsPercentage = poisonHitRateIsPercentage;
            turretUpgradeScriptableObject.poisonDuration = poisonDuration;
            turretUpgradeScriptableObject.poisonDurationIsPercentage = poisonDurationIsPercentage;

            turretUpgradeScriptableObject.slowdownMissile = slowdownMissile;
            turretUpgradeScriptableObject.slowdownEffectiveness = slowdownEffectiveness;
            turretUpgradeScriptableObject.slowdownEffectivenessIsPercentage = slowdownEffectivenessIsPercentage;
            turretUpgradeScriptableObject.slowdownEffectDurationIsPercentage = slowdownEffectDurationIsPercentage;

            turretUpgradeScriptableObject.explosiveMissile = explosiveMissile;
            turretUpgradeScriptableObject.explosionPrefab = explosionPrefab;
            turretUpgradeScriptableObject.explosionSprite = explosionSprite;
            turretUpgradeScriptableObject.explosionMaterial = explosionMaterial;
            turretUpgradeScriptableObject.explosionDamage = explosionDamage;
            turretUpgradeScriptableObject.explosionDamageIsPercentage = explosionDamageIsPercentage;
            turretUpgradeScriptableObject.explosionRange = explosionRange;
            turretUpgradeScriptableObject.explosionRangeIsPercentage = explosionRangeIsPercentage;
            turretUpgradeScriptableObject.explosionCopyMissileEffects = explosionCopyMissileEffects;

            turretUpgradeScriptableObject.trackingMissile = trackingMissile;
            turretUpgradeScriptableObject.penetrationMissile = penetrationMissile;

            turretUpgradeScriptableObject.auraDamage = auraDamage;
            turretUpgradeScriptableObject.auraDamageIsPercentage = auraDamageIsPercentage;
            turretUpgradeScriptableObject.auraRange = auraRange;
            turretUpgradeScriptableObject.auraRangeIsPercentage = auraRangeIsPercentage;

            turretUpgradeScriptableObject.auraSlowdownEffectiveness = auraSlowdownEffectiveness;
            turretUpgradeScriptableObject.auraSlowdownEffectivenessIsPercentage = auraSlowdownEffectivenessIsPercentage;

            turretUpgradeScriptableObject.Description = description;

            AssetDatabase.CreateAsset(turretUpgradeScriptableObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            upgradeManager.AddUpgrade(turretUpgradeScriptableObject);
        }

        private void TurretAuraSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Aura effects", EditorStyles.boldLabel);

            auraSlowdown = EditorGUILayout.Toggle("Slowdown effect", auraSlowdown);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Aura statistics", EditorStyles.boldLabel);
            FloatOrSliderField("Aura damage", ref auraDamage, ref auraDamageIsPercentage);
            FloatOrSliderField("Aura range", ref auraRange, ref auraRangeIsPercentage);
        }

        private void TurretAuraSlowdownSection()
        {
            FloatOrSliderField("Effectiveness", ref auraSlowdownEffectiveness, ref auraSlowdownEffectivenessIsPercentage);
        }

        private void TurretSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

            poisonMissile = EditorGUILayout.Toggle("Missile poison effect", poisonMissile);
            slowdownMissile = EditorGUILayout.Toggle("Missile slowdown effect", slowdownMissile);
            explosiveMissile = EditorGUILayout.Toggle("Explosive missile effect", explosiveMissile);
            penetrationMissile = EditorGUILayout.Toggle("Penetration missile", penetrationMissile);
            trackingMissile = EditorGUILayout.Toggle("Tracking missile", trackingMissile);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Turret statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Damage", ref damage, ref damageIsPercentage);
            FloatOrSliderField("Range", ref range, ref rangeIsPercentage); 
            FloatOrSliderField("Rotation speed", ref rotationSpeed, ref rotationSpeedIsPercentage);
        }

        private void TurretMissileSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Missile statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Missiles per second", ref missilesPerSecond, ref missilesPerSecondIsPercentage);
            FloatOrSliderField("Speed", ref missileSpeed, ref missileSpeedIsPercentage);
        }

        private void TurretLaserSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Laser statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Hits per second", ref laserHitsPerSecond, ref laserHitsPerSecondIsPercentage);
            FloatOrSliderField("Activation time", ref laserActivationTime, ref laserActivationTimeIsPercentage);
            FloatOrSliderField("Deactivation time", ref laserDeactivationTime, ref laserDeactivationTimeIsPercentage);
        }

        private void TurretPoisionSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Poision effect statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Damage", ref poisonDamage, ref poisonDamageIsPercentage);
            FloatOrSliderField("Hit rate", ref poisonHitRate, ref poisonHitRateIsPercentage);
            FloatOrSliderField("Duration", ref poisonDuration, ref poisonDurationIsPercentage);
        }

        private void TurretSlowdownSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Slowdown effect statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Effectiveness", ref slowdownEffectiveness, ref slowdownEffectivenessIsPercentage);
            FloatOrSliderField("Duration", ref slowdownEffectDuration, ref slowdownEffectDurationIsPercentage);
        }

        private void TurretExplosionSection()
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Explosion effect", EditorStyles.boldLabel);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Appearance", EditorStyles.boldLabel);

            explosionPrefab = EditorGUILayout.ObjectField("Prefab", explosionPrefab, typeof(GameObject), false) as GameObject;
            explosionSprite = EditorGUILayout.ObjectField("Sprite", explosionSprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Sprite;
            explosionMaterial = EditorGUILayout.ObjectField("Material", explosionMaterial, typeof(Material), false) as Material;

            EditorGUILayout.LabelField("Statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Damage", ref explosionDamage, ref explosionDamageIsPercentage);
            FloatOrSliderField("Range", ref explosionRange, ref explosionRangeIsPercentage);

            explosionCopyMissileEffects = EditorGUILayout.Toggle("Copy effects on explosion", explosionCopyMissileEffects);
        }

        private void FloatOrSliderField(string label, ref float property, ref bool isPercentage)
        {
            EditorGUILayout.BeginHorizontal();
            if (isPercentage)
            {
                property = EditorGUILayout.Slider(label, property, -1.0f, 1.0f);
            }
            else
            {
                property = EditorGUILayout.FloatField(label, property);
            }

            EditorGUILayout.LabelField("%", GUILayout.Width(12.5f));
            isPercentage = EditorGUILayout.Toggle(isPercentage, GUILayout.Width(15.0f));
            EditorGUILayout.EndHorizontal();
        }
    }
}
