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
    SerializedProperty _slowdownOnMissileHit;
    SerializedProperty _penetrationMissile;
    SerializedProperty _trackingMissile;

    SerializedProperty _auraSlowdown;

    SerializedProperty _damage;
    SerializedProperty _damageOverTime;
    SerializedProperty _missilesPerSecond;
    SerializedProperty _missileSpeed;
    SerializedProperty _range;
    SerializedProperty _explosionRange;
    SerializedProperty _rotationSpeed;
    SerializedProperty _laserActivationTime;
    SerializedProperty _timeToDeactiveLaser;
    SerializedProperty _slowdownEffectiveness;

    SerializedProperty _auraDamage;
    SerializedProperty _auraRange;
    SerializedProperty _auraSlowdownEffectiveness;

    SerializedProperty _turretSprite;
    SerializedProperty _turretMaterial;
    SerializedProperty _cannonSprite;
    SerializedProperty _cannonMaterial;
    SerializedProperty _missilePrefab;
    SerializedProperty _missileSize;
    SerializedProperty _missileSprite;
    SerializedProperty _missileMaterial;
    SerializedProperty _auraSprite;
    SerializedProperty _auraMaterial;

    SerializedProperty _lightSource;
    SerializedProperty _lightSourceInnerRadius;
    SerializedProperty _lightSourceOuterRadius;

    private void OnEnable()
    {
        _needTarget = serializedObject.FindProperty("_needTarget");
        _aura = serializedObject.FindProperty("_aura");

        _missile = serializedObject.FindProperty("_missile");
        _laser = serializedObject.FindProperty("_laser");

        _dealDamageOverTime = serializedObject.FindProperty("_dealDamageOverTime");
        _explosiveMissile = serializedObject.FindProperty("_explosiveMissile");
        _slowdownOnMissileHit = serializedObject.FindProperty("_slowdownOnMissileHit");
        _penetrationMissile = serializedObject.FindProperty("_penetrationMissile");
        _trackingMissile = serializedObject.FindProperty("_trackingMissile");

        _auraSlowdown = serializedObject.FindProperty("_auraSlowdown");

        _damage = serializedObject.FindProperty("_damage");
        _damageOverTime = serializedObject.FindProperty("_damageOverTime");
        _missilesPerSecond = serializedObject.FindProperty("_missilesPerSecond");
        _missileSpeed = serializedObject.FindProperty("_missileSpeed");
        _range = serializedObject.FindProperty("_range");
        _explosionRange = serializedObject.FindProperty("_explosionRange");
        _rotationSpeed = serializedObject.FindProperty("_rotationSpeed");
        _laserActivationTime = serializedObject.FindProperty("_laserActivationTime");
        _timeToDeactiveLaser = serializedObject.FindProperty("_timeToDeactiveLaser");
        _slowdownEffectiveness = serializedObject.FindProperty("_slowdownEffectiveness");

        _auraDamage = serializedObject.FindProperty("_auraDamage");
        _auraRange = serializedObject.FindProperty("_auraRange");
        _auraSlowdownEffectiveness = serializedObject.FindProperty("_auraSlowdownEffectiveness");

        _turretSprite = serializedObject.FindProperty("_turretSprite");
        _turretMaterial = serializedObject.FindProperty("_turretMaterial");
        _cannonSprite = serializedObject.FindProperty("_cannonSprite");
        _cannonMaterial = serializedObject.FindProperty("_cannonMaterial");
        _missilePrefab = serializedObject.FindProperty("_missilePrefab");
        _missileSize = serializedObject.FindProperty("_missileSize");
        _missileSprite = serializedObject.FindProperty("_missileSprite");
        _missileMaterial = serializedObject.FindProperty("_missileMaterial");
        _auraSprite = serializedObject.FindProperty("_auraSprite");
        _auraMaterial = serializedObject.FindProperty("_auraMaterial");

        _lightSource = serializedObject.FindProperty("_lightSource");
        _lightSourceInnerRadius = serializedObject.FindProperty("_lightSourceInnerRadius");
        _lightSourceOuterRadius = serializedObject.FindProperty("_lightSourceOuterRadius");
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

            if(_missile.boolValue)
            {
                EditorGUILayout.PropertyField(_dealDamageOverTime);
                EditorGUILayout.PropertyField(_explosiveMissile);
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
            EditorGUILayout.PropertyField(_missilesPerSecond);
            EditorGUILayout.PropertyField(_missileSpeed);

            if (_dealDamageOverTime.boolValue)
            {
                EditorGUILayout.PropertyField(_damageOverTime);
            }

            EditorGUILayout.PropertyField(_range);

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionRange);
            }

            EditorGUILayout.PropertyField(_rotationSpeed);

            if(_laser.boolValue)
            {
                EditorGUILayout.PropertyField(_laserActivationTime);
                EditorGUILayout.PropertyField(_timeToDeactiveLaser);
            }

            if (_slowdownOnMissileHit.boolValue)
            {
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
            EditorGUILayout.PropertyField(_missileSize);
            EditorGUILayout.PropertyField(_missileSprite);
            EditorGUILayout.PropertyField(_missileMaterial);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Appearance", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_turretSprite);
        EditorGUILayout.PropertyField(_turretMaterial);

        if (_needTarget.boolValue)
        {
            EditorGUILayout.PropertyField(_cannonSprite);
            EditorGUILayout.PropertyField(_cannonMaterial);
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.PropertyField(_auraSprite);
            EditorGUILayout.PropertyField(_auraMaterial);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Light options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_lightSource);
        EditorGUILayout.PropertyField(_lightSourceInnerRadius);
        EditorGUILayout.PropertyField(_lightSourceOuterRadius);

        serializedObject.ApplyModifiedProperties();
    }
}
