using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Game.InRunUpgrade.ScriptableObjectEditor.Strategies
{
    public class InRunTurretUpgradesScriptableObjectEditorStrategy : InRunUpgradesScriptableObjectEditorStrategy
    {
        private TurretScriptableObject turret;

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

        public InRunTurretUpgradesScriptableObjectEditorStrategy(SerializedObject serializedObject) : base(serializedObject) { }

        public override void OnEnable()
        {
            turret = serializedObject.FindProperty("Turret").objectReferenceValue as TurretScriptableObject;

            poisonMissile = serializedObject.FindProperty("PoisonMissile").boolValue;
            explosiveMissile = serializedObject.FindProperty("ExplosiveMissile").boolValue;
            explosionCopyMissileEffects = serializedObject.FindProperty("ExplosionCopyMissileEffects").boolValue;
            slowdownMissile = serializedObject.FindProperty("SlowdownMissile").boolValue;
            penetrationMissile = serializedObject.FindProperty("PenetrationMissile").boolValue;
            trackingMissile = serializedObject.FindProperty("TrackingMissile").boolValue;

            auraSlowdown = serializedObject.FindProperty("AuraSlowdown").boolValue;

            damage = serializedObject.FindProperty("Damage").floatValue;
            damageIsPercentage = serializedObject.FindProperty("DamageIsPercentage").boolValue;
            range = serializedObject.FindProperty("Range").floatValue;
            rangeIsPercentage = serializedObject.FindProperty("RangeIsPercentage").boolValue;
            rotationSpeed = serializedObject.FindProperty("RotationSpeed").floatValue;
            rotationSpeedIsPercentage = serializedObject.FindProperty("RotationSpeedIsPercentage").boolValue;
            missilesPerSecond = serializedObject.FindProperty("MissilesPerSecond").floatValue;
            missilesPerSecondIsPercentage = serializedObject.FindProperty("MissilesPerSecondIsPercentage").boolValue;
            missileSpeed = serializedObject.FindProperty("MissileSpeed").floatValue;
            missileSpeedIsPercentage = serializedObject.FindProperty("MissileSpeedIsPercentage").boolValue;

            laserHitsPerSecond = serializedObject.FindProperty("LaserHitsPerSecond").floatValue;
            laserHitsPerSecondIsPercentage = serializedObject.FindProperty("LaserHitsPerSecondIsPercentage").boolValue;
            laserActivationTime = serializedObject.FindProperty("LaserActivationTime").floatValue;
            laserActivationTimeIsPercentage = serializedObject.FindProperty("LaserActivationTimeIsPercentage").boolValue;
            laserDeactivationTime = serializedObject.FindProperty("LaserDeactivationTime").floatValue;
            laserDeactivationTimeIsPercentage = serializedObject.FindProperty("LaserDeactivationTimeIsPercentage").boolValue;

            slowdownEffectiveness = serializedObject.FindProperty("SlowdownEffectiveness").floatValue;
            slowdownEffectivenessIsPercentage = serializedObject.FindProperty("SlowdownEffectivenessIsPercentage").boolValue;
            slowdownEffectDuration = serializedObject.FindProperty("SlowdownEffectDuration").floatValue;
            slowdownEffectDurationIsPercentage = serializedObject.FindProperty("SlowdownEffectDurationIsPercentage").boolValue;

            poisonDamage = serializedObject.FindProperty("PoisonDamage").floatValue;
            poisonDamageIsPercentage = serializedObject.FindProperty("PoisonDamageIsPercentage").boolValue;
            poisonHitRate = serializedObject.FindProperty("PoisonHitRate").floatValue;
            poisonHitRateIsPercentage = serializedObject.FindProperty("PoisonHitRateIsPercentage").boolValue;
            poisonDuration = serializedObject.FindProperty("PoisonDuration").floatValue;
            poisonDurationIsPercentage = serializedObject.FindProperty("PoisonDurationIsPercentage").boolValue;

            explosionDamage = serializedObject.FindProperty("ExplosionDamage").floatValue;
            explosionDamageIsPercentage = serializedObject.FindProperty("ExplosionDamageIsPercentage").boolValue;
            explosionRange = serializedObject.FindProperty("ExplosionRange").floatValue;
            explosionRangeIsPercentage = serializedObject.FindProperty("ExplosionRangeIsPercentage").boolValue;

            auraDamage = serializedObject.FindProperty("AuraDamage").floatValue;
            auraDamageIsPercentage = serializedObject.FindProperty("AuraDamageIsPercentage").boolValue;
            auraRange = serializedObject.FindProperty("AuraRange").floatValue;
            auraRangeIsPercentage = serializedObject.FindProperty("AuraRangeIsPercentage").boolValue;

            auraSlowdownEffectiveness = serializedObject.FindProperty("AuraSlowdownEffectiveness").floatValue;
            auraSlowdownEffectivenessIsPercentage = serializedObject.FindProperty("AuraSlowdownEffectivenessIsPercentage").boolValue;

            explosionPrefab = serializedObject.FindProperty("ExplosionPrefab").objectReferenceValue as GameObject;
            explosionSprite = serializedObject.FindProperty("ExplosionSprite").objectReferenceValue as Sprite;
            explosionMaterial = serializedObject.FindProperty("ExplosionMaterial").objectReferenceValue as Material;
        }

        public override void OnInspectorGUI()
        {
            turret = EditorGUILayout.ObjectField("Turret", turret, typeof(TurretScriptableObject), false) as TurretScriptableObject;

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

        public override void SaveProperties()
        {
            serializedObject.FindProperty("Turret").objectReferenceValue = turret;

            serializedObject.FindProperty("PoisonMissile").boolValue = poisonMissile;
            serializedObject.FindProperty("ExplosiveMissile").boolValue = explosiveMissile;
            serializedObject.FindProperty("ExplosionCopyMissileEffects").boolValue = explosionCopyMissileEffects;
            serializedObject.FindProperty("SlowdownMissile").boolValue = slowdownMissile;
            serializedObject.FindProperty("PenetrationMissile").boolValue = penetrationMissile;
            serializedObject.FindProperty("TrackingMissile").boolValue = trackingMissile;

            serializedObject.FindProperty("AuraSlowdown").boolValue = auraSlowdown;

            serializedObject.FindProperty("Damage").floatValue = damage;
            serializedObject.FindProperty("DamageIsPercentage").boolValue = damageIsPercentage;
            serializedObject.FindProperty("Range").floatValue = range;
            serializedObject.FindProperty("RangeIsPercentage").boolValue = rangeIsPercentage;
            serializedObject.FindProperty("RotationSpeed").floatValue = rotationSpeed;
            serializedObject.FindProperty("RotationSpeedIsPercentage").boolValue = rotationSpeedIsPercentage;
            serializedObject.FindProperty("MissilesPerSecond").floatValue = missilesPerSecond;
            serializedObject.FindProperty("MissilesPerSecondIsPercentage").boolValue = missilesPerSecondIsPercentage;
            serializedObject.FindProperty("MissileSpeed").floatValue = missileSpeed;
            serializedObject.FindProperty("MissileSpeedIsPercentage").boolValue = missileSpeedIsPercentage;

            serializedObject.FindProperty("LaserHitsPerSecond").floatValue = laserHitsPerSecond;
            serializedObject.FindProperty("LaserHitsPerSecondIsPercentage").boolValue = laserHitsPerSecondIsPercentage;
            serializedObject.FindProperty("LaserActivationTime").floatValue = laserActivationTime;
            serializedObject.FindProperty("LaserActivationTimeIsPercentage").boolValue = laserActivationTimeIsPercentage;
            serializedObject.FindProperty("LaserDeactivationTime").floatValue = laserDeactivationTime;
            serializedObject.FindProperty("LaserDeactivationTimeIsPercentage").boolValue = laserDeactivationTimeIsPercentage;

            serializedObject.FindProperty("SlowdownEffectiveness").floatValue = slowdownEffectiveness;
            serializedObject.FindProperty("SlowdownEffectivenessIsPercentage").boolValue = slowdownEffectivenessIsPercentage;
            serializedObject.FindProperty("SlowdownEffectDuration").floatValue = slowdownEffectDuration;
            serializedObject.FindProperty("SlowdownEffectDurationIsPercentage").boolValue = slowdownEffectDurationIsPercentage;

            serializedObject.FindProperty("PoisonDamage").floatValue = poisonDamage;
            serializedObject.FindProperty("PoisonDamageIsPercentage").boolValue = poisonDamageIsPercentage;
            serializedObject.FindProperty("PoisonHitRate").floatValue = poisonHitRate;
            serializedObject.FindProperty("PoisonHitRateIsPercentage").boolValue = poisonHitRateIsPercentage;
            serializedObject.FindProperty("PoisonDuration").floatValue = poisonDuration;
            serializedObject.FindProperty("PoisonDurationIsPercentage").boolValue = poisonDurationIsPercentage;

            serializedObject.FindProperty("ExplosionDamage").floatValue = explosionDamage;
            serializedObject.FindProperty("ExplosionDamageIsPercentage").boolValue = explosionDamageIsPercentage;
            serializedObject.FindProperty("ExplosionRange").floatValue = explosionRange;
            serializedObject.FindProperty("ExplosionRangeIsPercentage").boolValue = explosionRangeIsPercentage;

            serializedObject.FindProperty("AuraDamage").floatValue = auraDamage;
            serializedObject.FindProperty("AuraDamageIsPercentage").boolValue = auraDamageIsPercentage;
            serializedObject.FindProperty("AuraRange").floatValue = auraRange;
            serializedObject.FindProperty("AuraRangeIsPercentage").boolValue = auraRangeIsPercentage;

            serializedObject.FindProperty("AuraSlowdownEffectiveness").floatValue = auraSlowdownEffectiveness;
            serializedObject.FindProperty("AuraSlowdownEffectivenessIsPercentage").boolValue = auraSlowdownEffectivenessIsPercentage;

            serializedObject.FindProperty("ExplosionPrefab").objectReferenceValue = explosionPrefab;
            serializedObject.FindProperty("ExplosionSprite").objectReferenceValue = explosionSprite;
            serializedObject.FindProperty("ExplosionMaterial").objectReferenceValue = explosionMaterial;
        }
    }
}
