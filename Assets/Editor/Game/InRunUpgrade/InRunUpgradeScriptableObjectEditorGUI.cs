using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Game.InRunUpgrade
{
    public static class InRunUpgradeScriptableObjectEditorGUI
    {
        public static void InRunUpgradeSection(
            ref TierScriptableObject tier,
            ref bool unique,
            ref string description)
        {
            tier = EditorGUILayout.ObjectField("Tier", tier, typeof(TierScriptableObject), false) as TierScriptableObject;
            unique = EditorGUILayout.Toggle("Is unique", unique);
            description = EditorGUILayout.TextArea(description, GUILayout.Height(80.0f));
        }

        public static void GameUpgradeSection(
            ref float health,
            ref bool healthIsPercentage,
            ref float maxHealth,
            ref bool maxHealthIsPercentage,
            ref bool increaseHealthToo,
            ref int neonBlocks)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Game upgrade", EditorStyles.boldLabel);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
            FloatOrSliderField("Health", ref health, ref healthIsPercentage);
            FloatOrSliderField("Max health", ref maxHealth, ref maxHealthIsPercentage);
            EditorGUILayout.Toggle("Increase health too", increaseHealthToo);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Neon blocks", EditorStyles.boldLabel);
            neonBlocks = EditorGUILayout.IntField("Increase neon blocks", neonBlocks);
        }

        public static void TurretAuraSection(
            ref bool auraSlowdown, 
            ref float auraDamage,
            ref bool auraDamageIsPercentage,
            ref float auraRange, 
            ref bool auraRangeIsPercentage)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Aura effects", EditorStyles.boldLabel);

            auraSlowdown = EditorGUILayout.Toggle("Slowdown effect", auraSlowdown);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Aura statistics", EditorStyles.boldLabel);
            FloatOrSliderField("Aura damage", ref auraDamage, ref auraDamageIsPercentage);
            FloatOrSliderField("Aura range", ref auraRange, ref auraRangeIsPercentage);
        }

        public static void TurretAuraSlowdownSection(
            ref float auraSlowdownEffectiveness, 
            ref bool auraSlowdownEffectivenessIsPercentage)
        {
            FloatOrSliderField("Effectiveness", ref auraSlowdownEffectiveness, ref auraSlowdownEffectivenessIsPercentage);
        }

        public static void TurretSection(
            ref bool poisonMissile, 
            ref bool slowdownMissile, 
            ref bool explosiveMissile, 
            ref bool penetrationMissile, 
            ref bool trackingMissile,
            ref float damage,
            ref bool damageIsPercentage,
            ref float range,
            ref bool rangeIsPercentage,
            ref float rotationSpeed,
            ref bool rotationSpeedIsPercentage)
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

        public static void TurretMissileSection(
            ref float missilesPerSecond,
            ref bool missilesPerSecondIsPercentage,
            ref float missileSpeed,
            ref bool missileSpeedIsPercentage)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Missile statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Missiles per second", ref missilesPerSecond, ref missilesPerSecondIsPercentage);
            FloatOrSliderField("Speed", ref missileSpeed, ref missileSpeedIsPercentage);
        }

        public static void TurretLaserSection(
            ref float laserHitsPerSecond,
            ref bool laserHitsPerSecondIsPercentage,
            ref float laserActivationTime,
            ref bool laserActivationTimeIsPercentage,
            ref float laserDeactivationTime,
            ref bool laserDeactivationTimeIsPercentage)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Laser statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Hits per second", ref laserHitsPerSecond, ref laserHitsPerSecondIsPercentage);
            FloatOrSliderField("Activation time", ref laserActivationTime, ref laserActivationTimeIsPercentage);
            FloatOrSliderField("Deactivation time", ref laserDeactivationTime, ref laserDeactivationTimeIsPercentage);
        }

        public static void TurretPoisionSection(
            ref float poisonDamage,
            ref bool poisonDamageIsPercentage,
            ref float poisonHitRate,
            ref bool poisonHitRateIsPercentage,
            ref float poisonDuration,
            ref bool poisonDurationIsPercentage)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Poision effect statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Damage", ref poisonDamage, ref poisonDamageIsPercentage);
            FloatOrSliderField("Hit rate", ref poisonHitRate, ref poisonHitRateIsPercentage);
            FloatOrSliderField("Duration", ref poisonDuration, ref poisonDurationIsPercentage);
        }

        public static void TurretSlowdownSection(
            ref float slowdownEffectiveness,
            ref bool slowdownEffectivenessIsPercentage,
            ref float slowdownEffectDuration,
            ref bool slowdownEffectDurationIsPercentage)
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Slowdown effect statistics", EditorStyles.boldLabel);

            FloatOrSliderField("Effectiveness", ref slowdownEffectiveness, ref slowdownEffectivenessIsPercentage);
            FloatOrSliderField("Duration", ref slowdownEffectDuration, ref slowdownEffectDurationIsPercentage);
        }

        public static void TurretExplosionSection(
            ref GameObject explosionPrefab,
            ref Sprite explosionSprite,
            ref Material explosionMaterial,
            ref float explosionDamage,
            ref bool explosionDamageIsPercentage,
            ref float explosionRange,
            ref bool explosionRangeIsPercentage,
            ref bool explosionCopyMissileEffects)
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

        private static void FloatOrSliderField(string label, ref float property, ref bool isPercentage)
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
