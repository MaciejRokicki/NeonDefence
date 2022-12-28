using UnityEditor;

[CustomEditor(typeof(TurretScriptableObject))]
public class TurreteScriptableObjectEditor : Editor
{
    SerializedProperty _cost;

    SerializedProperty _needTarget;
    SerializedProperty _aura;

    SerializedProperty _missile;
    SerializedProperty _laser;

    SerializedProperty _poisonMissile;
    SerializedProperty _explosiveMissile;
    SerializedProperty _slowdownMissile;

    SerializedProperty _penetrationMissile;
    SerializedProperty _trackingMissile;
    SerializedProperty _copyMissileEffects;

    SerializedProperty _auraSlowdown;


    SerializedProperty _damage;
    SerializedProperty _damageLimit;
    SerializedProperty _range;
    SerializedProperty _rangeLimit;
    SerializedProperty _rotationSpeed;
    SerializedProperty _rotationSpeedLimit;
    SerializedProperty _missilesPerSecond;
    SerializedProperty _missilesPerSecondLimit;
    SerializedProperty _missileSpeed;
    SerializedProperty _missileSpeedLimit;

    SerializedProperty _laserHitsPerSecond;
    SerializedProperty _laserHitsPerSecondLimit;
    SerializedProperty _laserActivationTime;
    SerializedProperty _laserActivationTimeLimit;
    SerializedProperty _laserDeactivationTime;
    SerializedProperty _laserDeactivationTimeLimit;

    SerializedProperty _slowdownEffectiveness;
    SerializedProperty _slowdownEffectivenessLimit;
    SerializedProperty _slowdownEffectDuration;
    SerializedProperty _slowdownEffectDurationLimit;

    SerializedProperty _poisonDamage;
    SerializedProperty _poisonDamageLimit;
    SerializedProperty _poisonHitRate;
    SerializedProperty _poisonHitRateLimit;
    SerializedProperty _poisonDuration;
    SerializedProperty _poisonDurationLimit;

    SerializedProperty _explosionDamage;
    SerializedProperty _explosionDamageLimit;
    SerializedProperty _explosionRange;
    SerializedProperty _explosionRangeLimit;

    SerializedProperty _auraDamage;
    SerializedProperty _auraDamageLimit;
    SerializedProperty _auraRange;
    SerializedProperty _auraRangeLimit;

    SerializedProperty _auraSlowdownEffectiveness;
    SerializedProperty _auraSlowdownEffectivenessLimit;


    SerializedProperty _turretIcon;
    SerializedProperty _turretIconMaterial;

    SerializedProperty _turretSprite;
    SerializedProperty _turretMaterial;
    SerializedProperty _cannonPrefab;
    SerializedProperty _cannonSprite;
    SerializedProperty _cannonMaterial;
    SerializedProperty _missilePrefab;
    SerializedProperty _missileColliderOffset;
    SerializedProperty _missileColliderSize;
    SerializedProperty _missileSpriteSize;
    SerializedProperty _missileSprite;
    SerializedProperty _missileMaterial;
    SerializedProperty _explosionPrefab;
    SerializedProperty _explosionSprite;
    SerializedProperty _explosionMaterial;
    SerializedProperty _auraPrefab;
    SerializedProperty _auraSprite;
    SerializedProperty _auraMaterial;

    private void OnEnable()
    {
        _cost = serializedObject.FindProperty("_cost");

        _needTarget = serializedObject.FindProperty("needTarget");
        _aura = serializedObject.FindProperty("aura");

        _missile = serializedObject.FindProperty("missile");
        _laser = serializedObject.FindProperty("laser");

        _poisonMissile = serializedObject.FindProperty("_poisonMissile");
        _explosiveMissile = serializedObject.FindProperty("_explosiveMissile");
        _copyMissileEffects = serializedObject.FindProperty("_copyMissileEffects");
        _slowdownMissile = serializedObject.FindProperty("_slowdownMissile");
        _penetrationMissile = serializedObject.FindProperty("_penetrationMissile");
        _trackingMissile = serializedObject.FindProperty("_trackingMissile");

        _auraSlowdown = serializedObject.FindProperty("auraSlowdown");


        _damage = serializedObject.FindProperty("_damage");
        _damageLimit = serializedObject.FindProperty("damageLimit");
        _range = serializedObject.FindProperty("_range");
        _rangeLimit = serializedObject.FindProperty("rangeLimit");
        _rotationSpeed = serializedObject.FindProperty("_rotationSpeed");
        _rotationSpeedLimit = serializedObject.FindProperty("rotationSpeedLimit");
        _missilesPerSecond = serializedObject.FindProperty("_missilesPerSecond");
        _missilesPerSecondLimit = serializedObject.FindProperty("missilesPerSecondLimit");
        _missileSpeed = serializedObject.FindProperty("_missileSpeed");
        _missileSpeedLimit = serializedObject.FindProperty("missileSpeedLimit");

        _laserHitsPerSecond = serializedObject.FindProperty("_laserHitsPerSecond");
        _laserHitsPerSecondLimit = serializedObject.FindProperty("laserHitsPerSecondLimit");
        _laserActivationTime = serializedObject.FindProperty("_laserActivationTime");
        _laserActivationTimeLimit = serializedObject.FindProperty("laserActivationTimeLimit");
        _laserDeactivationTime = serializedObject.FindProperty("_laserDeactivationTime");
        _laserDeactivationTimeLimit = serializedObject.FindProperty("laserDeactivationTimeLimit");

        _slowdownEffectiveness = serializedObject.FindProperty("_slowdownEffectiveness");
        _slowdownEffectivenessLimit = serializedObject.FindProperty("slowdownEffectivenessLimit");
        _slowdownEffectDuration = serializedObject.FindProperty("_slowdownEffectDuration");
        _slowdownEffectDurationLimit = serializedObject.FindProperty("slowdownEffectDurationLimit");

        _poisonDamage = serializedObject.FindProperty("_poisonDamage");
        _poisonDamageLimit = serializedObject.FindProperty("poisonDamageLimit");
        _poisonHitRate = serializedObject.FindProperty("_poisonHitRate");
        _poisonHitRateLimit = serializedObject.FindProperty("poisonHitRateLimit");
        _poisonDuration = serializedObject.FindProperty("_poisonDuration");
        _poisonDurationLimit = serializedObject.FindProperty("poisonDurationLimit");

        _explosionDamage = serializedObject.FindProperty("_explosionDamage");
        _explosionDamageLimit = serializedObject.FindProperty("explosionDamageLimit");
        _explosionRange = serializedObject.FindProperty("_explosionRange");
        _explosionRangeLimit = serializedObject.FindProperty("explosionRangeLimit");

        _auraDamage = serializedObject.FindProperty("_auraDamage");
        _auraDamageLimit = serializedObject.FindProperty("auraDamageLimit");
        _auraRange = serializedObject.FindProperty("_auraRange");
        _auraRangeLimit = serializedObject.FindProperty("auraRangeLimit");

        _auraSlowdownEffectiveness = serializedObject.FindProperty("_auraSlowdownEffectiveness");
        _auraSlowdownEffectivenessLimit = serializedObject.FindProperty("auraSlowdownEffectivenessLimit");


        _turretIcon = serializedObject.FindProperty("turretIcon");
        _turretIconMaterial = serializedObject.FindProperty("turretIconMaterial");

        _turretSprite = serializedObject.FindProperty("turretSprite");
        _turretMaterial = serializedObject.FindProperty("turretMaterial");
        _cannonPrefab = serializedObject.FindProperty("cannonPrefab");
        _cannonSprite = serializedObject.FindProperty("cannonSprite");
        _cannonMaterial = serializedObject.FindProperty("cannonMaterial");
        _missilePrefab = serializedObject.FindProperty("missilePrefab");
        _missileColliderOffset = serializedObject.FindProperty("missileColliderOffset");
        _missileColliderSize = serializedObject.FindProperty("missileColliderSize");
        _missileSpriteSize = serializedObject.FindProperty("missileSpriteSize");
        _missileSprite = serializedObject.FindProperty("missileSprite");
        _missileMaterial = serializedObject.FindProperty("missileMaterial");

        _explosionPrefab = serializedObject.FindProperty("_explosionPrefab");
        _explosionSprite = serializedObject.FindProperty("_explosionSprite");
        _explosionMaterial = serializedObject.FindProperty("_explosionMaterial");

        _auraPrefab = serializedObject.FindProperty("auraPrefab");
        _auraSprite = serializedObject.FindProperty("auraSprite");
        _auraMaterial = serializedObject.FindProperty("auraMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_cost);

        EditorGUILayout.LabelField("Turret Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_needTarget);
        EditorGUILayout.PropertyField(_aura);

        if(_needTarget.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Missile Type", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_missile);
            EditorGUILayout.PropertyField(_laser);

            if (_missile.boolValue || _laser.boolValue)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Missile Options", EditorStyles.boldLabel);
            }

            if (_missile.boolValue)
            {
                EditorGUILayout.PropertyField(_poisonMissile);
                EditorGUILayout.PropertyField(_explosiveMissile);
                if (_explosiveMissile.boolValue)
                {
                    EditorGUILayout.PropertyField(_copyMissileEffects);
                }
                EditorGUILayout.PropertyField(_trackingMissile);
            }

            if(_missile.boolValue || _laser.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownMissile);
                EditorGUILayout.PropertyField(_penetrationMissile);
            }
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Aura Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_auraSlowdown);
        }

        if (_needTarget.boolValue && (_missile.boolValue || _laser.boolValue))
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Missile Statistics", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_damage);
            EditorGUILayout.PropertyField(_damageLimit);
            EditorGUILayout.PropertyField(_range);
            EditorGUILayout.PropertyField(_rangeLimit);
            EditorGUILayout.PropertyField(_rotationSpeed);
            EditorGUILayout.PropertyField(_rotationSpeedLimit);

            if (_missile.boolValue)
            {
                EditorGUILayout.PropertyField(_missilesPerSecond);
                EditorGUILayout.PropertyField(_missilesPerSecondLimit);
                EditorGUILayout.PropertyField(_missileSpeed);
                EditorGUILayout.PropertyField(_missileSpeedLimit);
            }

            if (_laser.boolValue)
            {
                EditorGUILayout.PropertyField(_laserHitsPerSecond);
                EditorGUILayout.PropertyField(_laserHitsPerSecondLimit);
                EditorGUILayout.PropertyField(_laserActivationTime);
                EditorGUILayout.PropertyField(_laserActivationTimeLimit);
                EditorGUILayout.PropertyField(_laserDeactivationTime);
                EditorGUILayout.PropertyField(_laserDeactivationTimeLimit);
            }

            if (_slowdownMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownEffectiveness);
                EditorGUILayout.PropertyField(_slowdownEffectivenessLimit);
                EditorGUILayout.PropertyField(_slowdownEffectDuration);
                EditorGUILayout.PropertyField(_slowdownEffectDurationLimit);
            }

            if (_poisonMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_poisonDamage);
                EditorGUILayout.PropertyField(_poisonDamageLimit);
                EditorGUILayout.PropertyField(_poisonHitRate);
                EditorGUILayout.PropertyField(_poisonHitRateLimit);
                EditorGUILayout.PropertyField(_poisonDuration);
                EditorGUILayout.PropertyField(_poisonDurationLimit);
            }

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionDamage);
                EditorGUILayout.PropertyField(_explosionDamageLimit);
                EditorGUILayout.PropertyField(_explosionRange);
                EditorGUILayout.PropertyField(_explosionRangeLimit);
            }
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Aura Statistics", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_auraDamage);
            EditorGUILayout.PropertyField(_auraDamageLimit);
            EditorGUILayout.PropertyField(_auraRange);
            EditorGUILayout.PropertyField(_auraRangeLimit);
        }

        if (_auraSlowdown.boolValue)
        {
            EditorGUILayout.PropertyField(_auraSlowdownEffectiveness);
            EditorGUILayout.PropertyField(_auraSlowdownEffectivenessLimit);
        }

        if(_needTarget.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Missile Appearance", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_missilePrefab);
            EditorGUILayout.PropertyField(_missileColliderOffset);
            EditorGUILayout.PropertyField(_missileColliderSize);
            EditorGUILayout.PropertyField(_missileSpriteSize);
            EditorGUILayout.PropertyField(_missileSprite);
            EditorGUILayout.PropertyField(_missileMaterial);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Appearance", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_turretIcon);
        EditorGUILayout.PropertyField(_turretIconMaterial);
        EditorGUILayout.PropertyField(_turretSprite);
        EditorGUILayout.PropertyField(_turretMaterial);

        if (_needTarget.boolValue)
        {
            EditorGUILayout.PropertyField(_cannonPrefab);
            EditorGUILayout.PropertyField(_cannonSprite);
            EditorGUILayout.PropertyField(_cannonMaterial);

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionPrefab);
                EditorGUILayout.PropertyField(_explosionSprite);
                EditorGUILayout.PropertyField(_explosionMaterial);
            }
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.PropertyField(_auraPrefab);
            EditorGUILayout.PropertyField(_auraSprite);
            EditorGUILayout.PropertyField(_auraMaterial);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
