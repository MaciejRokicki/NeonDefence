using UnityEditor;

[CustomEditor(typeof(TurretScriptableObject))]
public class TurreteScriptableObjectEditor : Editor
{
    SerializedProperty _needTarget;
    SerializedProperty _aura;

    SerializedProperty _auraSlowdown;

    SerializedProperty _laser;
    SerializedProperty _dealDamageOverTime;
    SerializedProperty _explosiveMissile;
    SerializedProperty _slowdownOnMissileHit;

    SerializedProperty _auraDamage;
    SerializedProperty _auraRange;
    SerializedProperty _auraSlowdownEffectiveness;

    SerializedProperty _damage;
    SerializedProperty _damageOverTime;
    SerializedProperty _missilesPerSecond;
    SerializedProperty _missileSpeed;
    SerializedProperty _range;
    SerializedProperty _explosionRange;
    SerializedProperty _rotationSpeed;
    SerializedProperty _slowdownEffectiveness;

    SerializedProperty _turretSprite;
    SerializedProperty _turretMaterial;
    SerializedProperty _cannonSprite;
    SerializedProperty _cannonMaterial;
    SerializedProperty _missileData;
    SerializedProperty _auraSprite;
    SerializedProperty _auraMaterial;

    SerializedProperty _lightSource;
    SerializedProperty _lightSourceInnerRadius;
    SerializedProperty _lightSourceOuterRadius;

    private void OnEnable()
    {
        _needTarget = serializedObject.FindProperty("_needTarget");
        _aura = serializedObject.FindProperty("_aura");

        _auraSlowdown = serializedObject.FindProperty("_auraSlowdown");

        _laser = serializedObject.FindProperty("_laser");
        _dealDamageOverTime = serializedObject.FindProperty("_dealDamageOverTime");
        _explosiveMissile = serializedObject.FindProperty("_explosiveMissile");
        _slowdownOnMissileHit = serializedObject.FindProperty("_slowdownOnMissileHit");

        _auraDamage = serializedObject.FindProperty("_auraDamage");
        _auraRange = serializedObject.FindProperty("_auraRange");
        _auraSlowdownEffectiveness = serializedObject.FindProperty("_auraSlowdownEffectiveness");

        _damage = serializedObject.FindProperty("_damage");
        _damageOverTime = serializedObject.FindProperty("_damageOverTime");
        _missilesPerSecond = serializedObject.FindProperty("_missilesPerSecond");
        _missileSpeed = serializedObject.FindProperty("_missileSpeed");
        _range = serializedObject.FindProperty("_range");
        _explosionRange = serializedObject.FindProperty("_explosionRange");
        _rotationSpeed = serializedObject.FindProperty("_rotationSpeed");
        _slowdownEffectiveness = serializedObject.FindProperty("_slowdownEffectiveness");

        _turretSprite = serializedObject.FindProperty("_turretSprite");
        _turretMaterial = serializedObject.FindProperty("_turretMaterial");
        _cannonSprite = serializedObject.FindProperty("_cannonSprite");
        _cannonMaterial = serializedObject.FindProperty("_cannonMaterial");
        _missileData = serializedObject.FindProperty("_missileData");
        _auraSprite = serializedObject.FindProperty("_auraSprite");
        _auraMaterial = serializedObject.FindProperty("_auraMaterial");

        _lightSource = serializedObject.FindProperty("_lightSource");
        _lightSourceInnerRadius = serializedObject.FindProperty("_lightSourceInnerRadius");
        _lightSourceOuterRadius = serializedObject.FindProperty("_lightSourceOuterRadius");

    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();

        EditorGUILayout.LabelField("Turret Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_needTarget);
        EditorGUILayout.PropertyField(_aura);

        if(_aura.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Aura Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_auraSlowdown);
        }

        if(_needTarget.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Missile Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_laser);
            EditorGUILayout.PropertyField(_dealDamageOverTime);
            EditorGUILayout.PropertyField(_explosiveMissile);
            EditorGUILayout.PropertyField(_slowdownOnMissileHit);
        }

        if (_aura.boolValue)
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Aura Statistics", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_auraDamage);
            EditorGUILayout.PropertyField(_auraRange);

            if (_auraSlowdown.boolValue)
            {
                EditorGUILayout.PropertyField(_auraSlowdownEffectiveness);
            }
        }

        if (_needTarget.boolValue)
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

            if (_needTarget.boolValue)
            {
                EditorGUILayout.PropertyField(_range);
            }

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionRange);
            }

            if (_needTarget.boolValue)
            {
                EditorGUILayout.PropertyField(_rotationSpeed);
            }

            if (_slowdownOnMissileHit.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownEffectiveness);
            }
        }

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Appearance", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_turretSprite);
        EditorGUILayout.PropertyField(_turretMaterial);

        if (_needTarget.boolValue)
        {
            EditorGUILayout.PropertyField(_cannonSprite);
            EditorGUILayout.PropertyField(_cannonMaterial);
            EditorGUILayout.PropertyField(_missileData);
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
