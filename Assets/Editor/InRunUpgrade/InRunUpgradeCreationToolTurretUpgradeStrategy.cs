using System.Text;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;
using Assets.Extensions;

namespace Assets.Scripts.InRunUpgrade
{
    public class InRunUpgradeCreationToolTurretUpgradeStrategy : InRunUpgradeCreationToolStrategy
    {
        private readonly InRunUpgradeCreationTool inRunUpgradeCreationTool;

        private bool poisonMissile;
        private bool explosiveMissile;
        private bool slowdownMissile;
        private bool trackingMissile;
        private bool penetrationMissile;

        private int selectedTurretId;
        [Min(0.0f)]
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

        private GameObject explosionPrefab;
        private Sprite explosionSprite;
        private Material explosionMaterial;
        private float explosionDamage;
        private float explosionRange;
        private bool explosionCopyMissileEffects;

        private float auraDamage;
        private float auraRange;

        private float auraSlowdownEffectiveness;

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
                    auraDamage = EditorGUILayoutExtension.FloatField("Aura damage", auraDamage, 0.0f);
                    auraRange = EditorGUILayoutExtension.FloatField("Aura range", auraRange, 0.0f);
                    EditorGUILayout.Separator();

                    if (turret.auraSlowdown)
                    {
                        auraSlowdownEffectiveness = EditorGUILayoutExtension.FloatField("Aura slowdown effectiveness", auraSlowdownEffectiveness, 0.0f, 0.8f);
                    }
                }
                else
                {
                    damage = EditorGUILayoutExtension.FloatField("Damage", damage, 0.0f);
                    range = EditorGUILayoutExtension.FloatField("Range", range, 0.0f);
                    rotationSpeed = EditorGUILayoutExtension.FloatField("Rotation speed", rotationSpeed, 0.0f);

                    if (turret.missile)
                    {
                        missilesPerSecond = EditorGUILayoutExtension.FloatField("Missiles per second", missilesPerSecond, 0.0f);
                        missileSpeed = EditorGUILayoutExtension.FloatField("Missile speed", missileSpeed, 0.0f);
                    }

                    EditorGUILayout.Separator();

                    if (turret.laser)
                    {                   
                        laserHitsPerSecond = EditorGUILayoutExtension.FloatField("Laser hits per second", laserHitsPerSecond, 0.0f);
                        laserActivationTime = EditorGUILayoutExtension.FloatField("Laser activation time", laserActivationTime, 0.0f);
                        laserDeactivationTime = EditorGUILayoutExtension.FloatField("Laser deactivation time", laserDeactivationTime, 0.0f);
                        EditorGUILayout.Separator();
                    }

                    poisonMissile = EditorGUILayout.Toggle("Missile poison effect", poisonMissile);

                    if (turret.poisonMissile || poisonMissile)
                    {
                        poisonDamage = EditorGUILayoutExtension.FloatField("Poison damage", poisonDamage, 0.0f);
                        poisonHitRate = EditorGUILayoutExtension.FloatField("Poison hit rate", poisonHitRate, 0.0f);
                        poisonDuration = EditorGUILayoutExtension.FloatField("Poison duration", poisonDuration, 0.0f);
                        EditorGUILayout.Separator();
                    }

                    slowdownMissile = EditorGUILayout.Toggle("Missile slowdown effect", slowdownMissile);

                    if (turret.slowdownMissile || slowdownMissile)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Slowdown effectiveness");
                        slowdownEffectiveness = EditorGUILayout.Slider(slowdownEffectiveness, 0.0f, 1.0f);
                        EditorGUILayout.EndHorizontal();
                        slowdownEffectDuration = EditorGUILayoutExtension.FloatField("Slowdown effect duration", slowdownEffectDuration, 0.0f);
                        EditorGUILayout.Separator();
                    }

                    explosiveMissile = EditorGUILayout.Toggle("Explosive missile effect", explosiveMissile);

                    if (turret.explosiveMissile || explosiveMissile)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Explosion prefab");
                        explosionPrefab = EditorGUILayout.ObjectField(explosionPrefab, typeof(GameObject), false) as GameObject;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Explosion sprite");
                        explosionSprite = EditorGUILayout.ObjectField(explosionSprite, typeof(Sprite), false) as Sprite;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Explosion material");
                        explosionMaterial = EditorGUILayout.ObjectField(explosionMaterial, typeof(Material), false) as Material;
                        EditorGUILayout.EndHorizontal();
                        explosionDamage = EditorGUILayoutExtension.FloatField("Explosion damage", explosionDamage, 0.0f);
                        explosionRange = EditorGUILayoutExtension.FloatField("Explosion range", explosionRange, 0.0f);
                        explosionCopyMissileEffects = EditorGUILayout.Toggle("Copy missile effects on explosion", explosionCopyMissileEffects);
                        EditorGUILayout.Separator();

                    }

                    trackingMissile = EditorGUILayout.Toggle("Tracking missile", trackingMissile);
                    penetrationMissile = EditorGUILayout.Toggle("Penetration missile", penetrationMissile);
                }
            }
            else
            {
                damage = EditorGUILayoutExtension.FloatField("Damage", damage, 0.0f);
                range = EditorGUILayoutExtension.FloatField("Range", range, 0.0f);
                rotationSpeed = EditorGUILayoutExtension.FloatField("Rotation speed", rotationSpeed, 0.0f);
                missilesPerSecond = EditorGUILayoutExtension.FloatField("Missiles per second", missilesPerSecond, 0.0f);
                missileSpeed = EditorGUILayoutExtension.FloatField("Missile speed", missileSpeed, 0.0f);
                EditorGUILayout.Separator();
                laserHitsPerSecond = EditorGUILayoutExtension.FloatField("Laser hits per second", laserHitsPerSecond, 0.0f);
                laserActivationTime = EditorGUILayoutExtension.FloatField("Laser activation time", laserActivationTime, 0.0f);
                laserDeactivationTime = EditorGUILayoutExtension.FloatField("Laser deactivation time", laserDeactivationTime, 0.0f);
                EditorGUILayout.Separator();
                poisonMissile = EditorGUILayout.Toggle("Missile poison effect", poisonMissile);
                poisonDamage = EditorGUILayoutExtension.FloatField("Poison damage", poisonDamage, 0.0f);
                poisonHitRate = EditorGUILayoutExtension.FloatField("Poison hit rate", poisonHitRate, 0.0f);
                poisonDuration = EditorGUILayoutExtension.FloatField("Poison duration", poisonDuration, 0.0f);
                EditorGUILayout.Separator();
                slowdownMissile = EditorGUILayout.Toggle("Missile slowdown effect", slowdownMissile);
                slowdownEffectiveness = EditorGUILayoutExtension.FloatField("Slowdown effectiveness", slowdownEffectiveness, 0.0f, 0.8f);
                slowdownEffectDuration = EditorGUILayoutExtension.FloatField("Slowdown effect duration", slowdownEffectDuration, 0.0f);
                EditorGUILayout.Separator();
                explosiveMissile = EditorGUILayout.Toggle("Explosive missile effect", explosiveMissile);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Explosion prefab");
                explosionPrefab = EditorGUILayout.ObjectField(explosionPrefab, typeof(GameObject), false) as GameObject;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Explosion sprite");
                explosionSprite = EditorGUILayout.ObjectField(explosionSprite, typeof(Sprite), false) as Sprite;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Explosion material");
                explosionMaterial = EditorGUILayout.ObjectField(explosionMaterial, typeof(Material), false) as Material;
                EditorGUILayout.EndHorizontal();
                explosionDamage = EditorGUILayoutExtension.FloatField("Explosion damage", explosionDamage, 0.0f);
                explosionRange = EditorGUILayoutExtension.FloatField("Explosion range", explosionRange, 0.0f);
                explosionCopyMissileEffects = EditorGUILayout.Toggle("Copy missile effects on explosion", explosionCopyMissileEffects);
                EditorGUILayout.Separator();
                trackingMissile = EditorGUILayout.Toggle("Tracking missile", trackingMissile);
                penetrationMissile = EditorGUILayout.Toggle("Penetration missile", penetrationMissile);
                EditorGUILayout.Separator();
                auraDamage = EditorGUILayoutExtension.FloatField("Aura damage", auraDamage, 0.0f);
                auraRange = EditorGUILayoutExtension.FloatField("Aura range", auraRange, 0.0f);
                EditorGUILayout.Separator();
                auraSlowdownEffectiveness = EditorGUILayoutExtension.FloatField("Aura slowdown effectiveness", auraSlowdownEffectiveness, 0.0f, 0.8f);
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
            turretUpgradeScriptableObject.range = range;
            turretUpgradeScriptableObject.rotationSpeed = rotationSpeed;
            turretUpgradeScriptableObject.missilesPerSecond = missilesPerSecond;
            turretUpgradeScriptableObject.missileSpeed = missileSpeed;

            turretUpgradeScriptableObject.laserHitsPerSecond = laserHitsPerSecond;
            turretUpgradeScriptableObject.laserActivationTime = laserActivationTime;
            turretUpgradeScriptableObject.laserDeactivationTime = laserDeactivationTime;

            turretUpgradeScriptableObject.poisonMissile = poisonMissile;
            turretUpgradeScriptableObject.poisonDamage = poisonDamage;
            turretUpgradeScriptableObject.poisonHitRate = poisonHitRate;
            turretUpgradeScriptableObject.poisonDuration = poisonDuration;

            turretUpgradeScriptableObject.slowdownMissile = slowdownMissile;
            turretUpgradeScriptableObject.slowdownEffectiveness = slowdownEffectiveness;
            turretUpgradeScriptableObject.slowdownEffectDuration = slowdownEffectDuration;

            turretUpgradeScriptableObject.explosiveMissile = explosiveMissile;
            turretUpgradeScriptableObject.explosionPrefab = explosionPrefab;
            turretUpgradeScriptableObject.explosionSprite = explosionSprite;
            turretUpgradeScriptableObject.explosionMaterial = explosionMaterial;
            turretUpgradeScriptableObject.explosionDamage = explosionDamage;
            turretUpgradeScriptableObject.explosionRange = explosionRange;
            turretUpgradeScriptableObject.explosionCopyMissileEffects = explosionCopyMissileEffects;

            turretUpgradeScriptableObject.trackingMissile = trackingMissile;
            turretUpgradeScriptableObject.penetrationMissile = penetrationMissile;

            turretUpgradeScriptableObject.auraDamage = auraDamage;
            turretUpgradeScriptableObject.auraRange = auraRange;

            turretUpgradeScriptableObject.auraSlowdownEffectiveness = auraSlowdownEffectiveness;

            turretUpgradeScriptableObject.Description = description;

            AssetDatabase.CreateAsset(turretUpgradeScriptableObject, pathStringBuilder.ToString());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            upgradeManager.AddUpgrade(turretUpgradeScriptableObject);
        }
    }
}
