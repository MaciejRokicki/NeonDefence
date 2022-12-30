using UnityEditor;

[CustomEditor(typeof(TurretScriptableObject))]
public class TurreteScriptableObjectEditor : Editor
{
    private SerializedProperty _cost;

    private SerializedProperty _needTarget;
    private SerializedProperty _aura;

    private SerializedProperty _turretLimits;

    private SerializedProperty _missile;
    private SerializedProperty _laser;

    private SerializedProperty _poisonMissile;
    private SerializedProperty _explosiveMissile;
    private SerializedProperty _slowdownMissile;

    private SerializedProperty _penetrationMissile;
    private SerializedProperty _trackingMissile;
    private SerializedProperty _copyMissileEffects;

    private SerializedProperty _auraSlowdown;


    private SerializedProperty _damage;
    private SerializedProperty _range;
    private SerializedProperty _rotationSpeed;
    private SerializedProperty _missilesPerSecond;
    private SerializedProperty _missileSpeed;

    private SerializedProperty _laserHitsPerSecond;
    private SerializedProperty _laserActivationTime;
    private SerializedProperty _laserDeactivationTime;

    private SerializedProperty _slowdownEffectiveness;
    private SerializedProperty _slowdownEffectDuration;

    private SerializedProperty _poisonDamage;
    private SerializedProperty _poisonHitRate;
    private SerializedProperty _poisonDuration;

    private SerializedProperty _explosionDamage;
    private SerializedProperty _explosionRange;

    private SerializedProperty _auraDamage;
    private SerializedProperty _auraRange;

    private SerializedProperty _auraSlowdownEffectiveness;


    private SerializedProperty _turretIcon;
    private SerializedProperty _turretIconMaterial;

    private SerializedProperty _turretSprite;
    private SerializedProperty _turretMaterial;
    private SerializedProperty _cannonPrefab;
    private SerializedProperty _cannonSprite;
    private SerializedProperty _cannonMaterial;
    private SerializedProperty _missilePrefab;
    private SerializedProperty _missileColliderOffset;
    private SerializedProperty _missileColliderSize;
    private SerializedProperty _missileSpriteSize;
    private SerializedProperty _missileSprite;
    private SerializedProperty _missileMaterial;
    private SerializedProperty _explosionPrefab;
    private SerializedProperty _explosionSprite;
    private SerializedProperty _explosionMaterial;
    private SerializedProperty _auraPrefab;
    private SerializedProperty _auraSprite;
    private SerializedProperty _auraMaterial;

    private void OnEnable()
    {
        _cost = serializedObject.FindProperty("_cost");

        _needTarget = serializedObject.FindProperty("NeedTarget");
        _aura = serializedObject.FindProperty("Aura");

        _turretLimits = serializedObject.FindProperty("TurretLimits");

        _missile = serializedObject.FindProperty("Missile");
        _laser = serializedObject.FindProperty("Laser");

        _poisonMissile = serializedObject.FindProperty("_poisonMissile");
        _explosiveMissile = serializedObject.FindProperty("_explosiveMissile");
        _copyMissileEffects = serializedObject.FindProperty("_copyMissileEffects");
        _slowdownMissile = serializedObject.FindProperty("_slowdownMissile");
        _penetrationMissile = serializedObject.FindProperty("_penetrationMissile");
        _trackingMissile = serializedObject.FindProperty("_trackingMissile");

        _auraSlowdown = serializedObject.FindProperty("_auraSlowdown");


        _damage = serializedObject.FindProperty("_damage");
        _range = serializedObject.FindProperty("_range");
        _rotationSpeed = serializedObject.FindProperty("_rotationSpeed");
        _missilesPerSecond = serializedObject.FindProperty("_missilesPerSecond");
        _missileSpeed = serializedObject.FindProperty("_missileSpeed");

        _laserHitsPerSecond = serializedObject.FindProperty("_laserHitsPerSecond");
        _laserActivationTime = serializedObject.FindProperty("_laserActivationTime");
        _laserDeactivationTime = serializedObject.FindProperty("_laserDeactivationTime");

        _slowdownEffectiveness = serializedObject.FindProperty("_slowdownEffectiveness");
        _slowdownEffectDuration = serializedObject.FindProperty("_slowdownEffectDuration");

        _poisonDamage = serializedObject.FindProperty("_poisonDamage");
        _poisonHitRate = serializedObject.FindProperty("_poisonHitRate");
        _poisonDuration = serializedObject.FindProperty("_poisonDuration");

        _explosionDamage = serializedObject.FindProperty("_explosionDamage");
        _explosionRange = serializedObject.FindProperty("_explosionRange");

        _auraDamage = serializedObject.FindProperty("_auraDamage");
        _auraRange = serializedObject.FindProperty("_auraRange");

        _auraSlowdownEffectiveness = serializedObject.FindProperty("_auraSlowdownEffectiveness");


        _turretIcon = serializedObject.FindProperty("TurretIcon");
        _turretIconMaterial = serializedObject.FindProperty("TurretIconMaterial");

        _turretSprite = serializedObject.FindProperty("TurretSprite");
        _turretMaterial = serializedObject.FindProperty("TurretMaterial");
        _cannonPrefab = serializedObject.FindProperty("CannonPrefab");
        _cannonSprite = serializedObject.FindProperty("CannonSprite");
        _cannonMaterial = serializedObject.FindProperty("CannonMaterial");
        _missilePrefab = serializedObject.FindProperty("MissilePrefab");
        _missileColliderOffset = serializedObject.FindProperty("MissileColliderOffset");
        _missileColliderSize = serializedObject.FindProperty("MissileColliderSize");
        _missileSpriteSize = serializedObject.FindProperty("MissileSpriteSize");
        _missileSprite = serializedObject.FindProperty("MissileSprite");
        _missileMaterial = serializedObject.FindProperty("MissileMaterial");

        _explosionPrefab = serializedObject.FindProperty("_explosionPrefab");
        _explosionSprite = serializedObject.FindProperty("_explosionSprite");
        _explosionMaterial = serializedObject.FindProperty("_explosionMaterial");

        _auraPrefab = serializedObject.FindProperty("AuraPrefab");
        _auraSprite = serializedObject.FindProperty("AuraSprite");
        _auraMaterial = serializedObject.FindProperty("AuraMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_cost);

        EditorGUILayout.LabelField("Turret Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_needTarget);
        EditorGUILayout.PropertyField(_aura);

        EditorGUILayout.PropertyField(_turretLimits);

        if (_needTarget.boolValue)
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
            EditorGUILayout.PropertyField(_range);
            EditorGUILayout.PropertyField(_rotationSpeed);        

            if (_missile.boolValue)
            {
                EditorGUILayout.PropertyField(_missilesPerSecond);
                EditorGUILayout.PropertyField(_missileSpeed);
            }

            if (_laser.boolValue)
            {
                EditorGUILayout.PropertyField(_laserHitsPerSecond);
                EditorGUILayout.PropertyField(_laserActivationTime);
                EditorGUILayout.PropertyField(_laserDeactivationTime);
            }

            if (_slowdownMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_slowdownEffectiveness);
                EditorGUILayout.PropertyField(_slowdownEffectDuration);
            }

            if (_poisonMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_poisonDamage);
                EditorGUILayout.PropertyField(_poisonHitRate);
                EditorGUILayout.PropertyField(_poisonDuration);
            }

            if (_explosiveMissile.boolValue)
            {
                EditorGUILayout.PropertyField(_explosionDamage);
                EditorGUILayout.PropertyField(_explosionRange);
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
