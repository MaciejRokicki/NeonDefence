using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretScriptableObject", order = 3)]
public class TurretScriptableObject : ScriptableObject
{
    [SerializeField] 
    private bool _needTarget;
    public bool needTarget;
    [SerializeField]
    private bool _aura;
    public bool aura;

    [SerializeField]
    private bool _missile;
    public bool missile;
    [SerializeField]
    private bool _laser;
    public bool laser;

    [SerializeField]
    private bool _dealDamageOverTime;
    public bool dealDamageOverTime;
    [SerializeField]
    private bool _explosiveMissile;
    public bool explosiveMissile;
    [SerializeField]
    private bool _slowdownOnMissileHit;
    public bool slowdownOnMissileHit;
    [SerializeField]
    private bool _penetrationMissile;
    public bool penetrationMissile;
    [SerializeField]
    private bool _trackingMissile;
    public bool trackingMissile;

    [SerializeField]    //TODO: todo
    private bool _auraSlowdown;
    public bool auraSlowdown;

    [SerializeField]
    private float _damage;
    public float damage;
    [SerializeField]
    private float _damageOverTimeDuration;
    public float damageOverTimeDuration;
    [SerializeField]
    private float _damageOverTimeCooldown;
    public float damageOverTimeCooldown;
    [SerializeField]
    private float _damageOverTime;
    public float damageOverTime;
    [SerializeField]
    private float _missilesPerSecond;
    public float missilesPerSecond;
    [SerializeField]
    private float _missileSpeed;
    public float missileSpeed;
    [SerializeField]
    private float _laserHitsPerSecond;
    public float laserHitsPerSecond;
    [SerializeField]
    private float _range;
    public float range;
    [SerializeField]
    private bool _copyMissileEffects;
    public bool copyMissileEffects;
    [SerializeField]
    private float _explosionDamage;
    public float explosionDamage;
    [SerializeField]
    private float _explosionRange;
    public float explosionRange;
    [SerializeField]
    private float _rotationSpeed;
    public float rotationSpeed;
    [SerializeField]
    private float _laserActivationTime;
    public float laserActivationTime;
    [SerializeField]
    private float _timeToDeactiveLaser;
    public float timeToDeactiveLaser;
    [SerializeField]
    private float _slowdownEffectDuration;
    public float slowdownEffectDuration;
    [SerializeField]
    private float _slowdownEffectiveness;
    public float slowdownEffectiveness;

    [SerializeField]
    private float _auraDamage;
    public float auraDamage;
    [SerializeField]
    private float _auraRange;
    public float auraRange;
    [SerializeField]    //TODO: todo
    private float _auraSlowdownEffectiveness;
    public float auraSlowdownEffectiveness;

    [SerializeField]
    private Sprite _turretSprite;
    public Sprite turretSprite;
    [SerializeField]
    private Material _turretMaterial;
    public Material turretMaterial;
    [SerializeField]
    private GameObject _cannonPrefab;
    public GameObject cannonPrefab;
    [SerializeField]
    private Sprite _cannonSprite;
    public Sprite cannonSprite;
    [SerializeField]
    private Material _cannonMaterial;
    public Material cannonMaterial;
    [SerializeField]
    private GameObject _missilePrefab;
    public GameObject missilePrefab;
    [SerializeField]
    private Vector2 _missileColliderOffset;
    public Vector2 missileColliderOffset;
    [SerializeField]
    private Vector2 _missileColliderSize;
    public Vector2 missileColliderSize;
    [SerializeField]
    private Vector2 _missileSpriteSize;
    public Vector2 missileSpriteSize;
    [SerializeField]
    private Sprite _missileSprite;
    public Sprite missileSprite;
    [SerializeField]
    private Material _missileMaterial;
    public Material missileMaterial;
    [SerializeField]
    private GameObject _explosionPrefab;
    public GameObject explosionPrefab;
    [SerializeField]
    private Sprite _explosionSprite;
    public Sprite explosionSprite;
    [SerializeField]
    private Material _explosionMaterial;
    public Material explosionMaterial;
    [SerializeField]
    private GameObject _auraPrefab;
    public GameObject auraPrefab;
    [SerializeField]
    private Sprite _auraSprite;
    public Sprite auraSprite;
    [SerializeField]
    private Material _auraMaterial;
    public Material auraMaterial;

    [SerializeField]
    private bool _lightSource;
    public bool lightSource;
    [SerializeField]
    private float _lightSourceInnerRadius;
    public float lightSourceInnerRadius;
    [SerializeField]
    private float _lightSourceOuterRadius;
    public float lightSourceOuterRadius;

    private void OnEnable()
    {
        needTarget = _needTarget;
        aura = _aura;

        missile = _missile;
        laser = _laser;

        dealDamageOverTime = _dealDamageOverTime;
        explosiveMissile = _explosiveMissile;
        slowdownOnMissileHit = _slowdownOnMissileHit;
        penetrationMissile = _penetrationMissile;
        trackingMissile = _trackingMissile;

        auraSlowdown = _auraSlowdown;

        damage = _damage;
        damageOverTimeDuration = _damageOverTimeDuration;
        damageOverTimeCooldown = _damageOverTimeCooldown;
        damageOverTime = _damageOverTime;
        missilesPerSecond = _missilesPerSecond;
        missileSpeed = _missileSpeed;
        laserHitsPerSecond = _laserHitsPerSecond;
        range = _range;
        copyMissileEffects = _copyMissileEffects;
        explosionDamage = _explosionDamage;
        explosionRange = _explosionRange;
        rotationSpeed = _rotationSpeed;
        laserActivationTime = _laserActivationTime;
        timeToDeactiveLaser = _timeToDeactiveLaser;
        slowdownEffectDuration = _slowdownEffectDuration;
        slowdownEffectiveness = _slowdownEffectiveness;

        auraDamage = _auraDamage;
        auraRange = _auraRange;
        auraSlowdownEffectiveness = _auraSlowdownEffectiveness;

        turretSprite = _turretSprite;
        turretMaterial = _turretMaterial;
        cannonPrefab = _cannonPrefab;
        cannonSprite = _cannonSprite;
        cannonMaterial = _cannonMaterial;
        missilePrefab = _missilePrefab;
        missileColliderOffset = _missileColliderOffset;
        missileColliderSize = _missileColliderSize;
        missileSpriteSize = _missileSpriteSize;
        missileSprite = _missileSprite;
        missileMaterial = _missileMaterial;
        explosionPrefab = _explosionPrefab;
        explosionSprite = _explosionSprite;
        explosionMaterial = _explosionMaterial;
        auraPrefab = _auraPrefab;
        auraSprite = _auraSprite;
        auraMaterial = _auraMaterial;

        lightSource = _lightSource;
        lightSourceInnerRadius = _lightSourceInnerRadius;
        lightSourceOuterRadius = _lightSourceOuterRadius;
    }
}
