using UnityEditor;

[CustomEditor(typeof(TurretScriptableObject))]
public class TurreteScriptableObjectEditor : Editor
{
    SerializedProperty _needTarget;
    SerializedProperty _aura;

    SerializedProperty _missile;
    SerializedProperty _laser;

    SerializedProperty _dealDamageOverTime;
    SerializedProperty _explosiveMissile;
    SerializedProperty _copyMissileEffects;
    SerializedProperty _slowdownOnMissileHit;
    SerializedProperty _penetrationMissile;
    SerializedProperty _trackingMissile;

    SerializedProperty _auraSlowdown;

    SerializedProperty _damage;
    SerializedProperty _damageOverTimeDuration;
    SerializedProperty _damageOverTimeCooldown;
    SerializedProperty _damageOverTime;
    SerializedProperty _missilesPerSecond;
    SerializedProperty _missileSpeed;
    SerializedProperty _laserHitsPerSecond;
    SerializedProperty _range;
    SerializedProperty _explosionDamage;
    SerializedProperty _explosionRange;
    SerializedProperty _rotationSpeed;
    SerializedProperty _laserActivationTime;
    SerializedProperty _laserDeactivationTime;
    SerializedProperty _slowdownEffectDuration;
    SerializedProperty _slowdownEffectiveness;

    SerializedProperty _auraDamage;
    SerializedProperty _auraRange;
    SerializedProperty _auraSlowdownEffectiveness;

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
        _needTarget = serializedObject.FindProperty("needTarget");
        _aura = serializedObject.FindProperty("aura");

        _missile = serializedObject.FindProperty("missile");
        _laser = serializedObject.FindProperty("laser");

        _dealDamageOverTime = serializedObject.FindProperty("dealDamageOverTime");
        _explosiveMissile = serializedObject.FindProperty("explosiveMissile");
        _copyMissileEffects = serializedObject.FindProperty("_copyMissileEffects");
        _slowdownOnMissileHit = serializedObject.FindProperty("slowdownOnMissileHit");
        _penetrationMissile = serializedObject.FindProperty("penetrationMissile");
        _trackingMissile = serializedObject.FindProperty("trackingMissile");

        _auraSlowdown = serializedObject.FindProperty("auraSlowdown");

        _damage = serializedObject.FindProperty("_damage");
        _damageOverTimeDuration = serializedObject.FindProperty("_damageOverTimeDuration");
        _damageOverTimeCooldown = serializedObject.FindProperty("_damageOverTimeCooldown");
        _damageOverTime = serializedObject.FindProperty("_damageOverTime");
        _missilesPerSecond = serializedObject.FindProperty("_missilesPerSecond");
        _missileSpeed = serializedObject.FindProperty("_missileSpeed");
        _laserHitsPerSecond = serializedObject.FindProperty("_laserHitsPerSecond");
        _range = serializedObject.FindProperty("_range");
        _explosionDamage = serializedObject.FindProperty("_explosionDamage");
        _explosionRange = serializedObject.FindProperty("_explosionRange");
        _rotationSpeed = serializedObject.FindProperty("_rotationSpeed");
        _laserActivationTime = serializedObject.FindProperty("_laserActivationTime");
        _laserDeactivationTime = serializedObject.FindProperty("_laserDeactivationTime");
        _slowdownEffectDuration = serializedObject.FindProperty("_slowdownEffectDuration");
        _slowdownEffectiveness = serializedObject.FindProperty("_slowdownEffectiveness");

        _auraDamage = serializedObject.FindProperty("_auraDamage");
        _auraRange = serializedObject.FindProperty("_auraRange");
        _auraSlowdownEffectiveness = serializedObject.FindProperty("_auraSlowdownEffectiveness");

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
        _explosionPrefab = serializedObject.FindProperty("explosionPrefab");
        _explosionSprite = serializedObject.FindProperty("explosionSprite");
        _explosionMaterial = serializedObject.FindProperty("explosionMaterial");
        _auraPrefab = serializedObject.FindProperty("auraPrefab");
        _auraSprite = serializedObject.FindProperty("auraSprite");
        _auraMaterial = serializedObject.FindProperty("auraMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

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
                EditorGUILayout.PropertyField(_dealDamageOverTime);
                EditorGUILayout.PropertyField(_explosiveMissile);
                if (_explosiveMissile.boolValue)
                {
                    EditorGUILayout.PropertyField(_copyMissileEffects);
                }
                EditorGUILayout.PropertyField(_trackingMissile);
            }

            if(_missile.boolValue || _laser.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownOnMissileHit);
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

            if(_missile.boolValue)
            {
                EditorGUILayout.PropertyField(_missilesPerSecond);
                EditorGUILayout.PropertyField(_missileSpeed);
            }

            if (_laser.boolValue)
            {
                EditorGUILayout.PropertyField(_laserHitsPerSecond);
            }

            if (_dealDamageOverTime.boolValue)
            {
                EditorGUILayout.PropertyField(_damageOverTimeDuration);
                EditorGUILayout.PropertyField(_damageOverTimeCooldown);
                EditorGUILayout.PropertyField(_damageOverTime);
            }

            EditorGUILayout.PropertyField(_range);

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionDamage);
                EditorGUILayout.PropertyField(_explosionRange);
            }

            EditorGUILayout.PropertyField(_rotationSpeed);

            if(_laser.boolValue)
            {
                EditorGUILayout.PropertyField(_laserActivationTime);
                EditorGUILayout.PropertyField(_laserDeactivationTime);
            }

            if (_slowdownOnMissileHit.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownEffectDuration);
                EditorGUILayout.PropertyField(_slowdownEffectiveness);
            }
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Aura Statistics", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_auraDamage);
            EditorGUILayout.PropertyField(_auraRange);
        }

        if (_auraSlowdown.boolValue)
        {
            EditorGUILayout.PropertyField(_auraSlowdownEffectiveness);
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
