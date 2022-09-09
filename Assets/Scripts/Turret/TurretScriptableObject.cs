using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretScriptableObject", order = 3)]
public class TurretScriptableObject : ScriptableObject
{
    [SerializeField] 
    private bool _needTarget;
    public bool needTarget;
    [SerializeField]    //TODO: todo
    private bool _aura;
    public bool aura;

    [SerializeField]
    private bool _missile;
    public bool missile;
    [SerializeField]
    private bool _laser;
    public bool laser;

    [SerializeField]    //TODO: todo
    private bool _dealDamageOverTime;
    public bool dealDamageOverTime;
    [SerializeField]    //TODO: todo
    private bool _explosiveMissile;
    public bool explosiveMissile;
    [SerializeField]    //TODO: todo
    private bool _slowdownOnMissileHit;
    public bool slowdownOnMissileHit;
    [SerializeField]    //TODO: todo
    private bool _penetrationMissile;
    public bool penetrationMissile;
    [SerializeField]    //TODO: todo
    private bool _trackingMissile;
    public bool trackingMissile;

    [SerializeField]    //TODO: todo
    private bool _auraSlowdown;
    public bool auraSlowdown;

    [SerializeField]
    private float _damage;
    public float damage;
    [SerializeField]    //TODO: todo
    private float _damageOverTime;
    public float damageOverTime;
    [SerializeField]
    private float _missilesPerSecond;
    public float missilesPerSecond;
    [SerializeField]
    private float _missileSpeed;
    public float missileSpeed;
    [SerializeField]
    private float _range;
    public float range;
    [SerializeField]    //TODO: todo
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
    [SerializeField]    //TODO: todo
    private float _slowdownEffectiveness;
    public float slowdownEffectiveness;

    [SerializeField]    //TODO: todo
    private float _auraDamage;
    public float auraDamage;
    [SerializeField]    //TODO: todo
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
    private Sprite _cannonSprite;
    public Sprite cannonSprite;
    [SerializeField]
    private Material _cannonMaterial;
    public Material cannonMaterial;
    [SerializeField]
    private GameObject _missilePrefab;
    public GameObject missilePrefab;
    [SerializeField]
    private Vector2 _missileSize;
    public Vector2 missileSize;
    [SerializeField]
    private Sprite _missileSprite;
    public Sprite missileSprite;
    [SerializeField]
    private Material _missileMaterial;
    public Material missileMaterial;
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
        damageOverTime = _damageOverTime;
        missilesPerSecond = _missilesPerSecond;
        missileSpeed = _missileSpeed;
        range = _range;
        explosionRange = _explosionRange;
        rotationSpeed = _rotationSpeed;
        laserActivationTime = _laserActivationTime;
        timeToDeactiveLaser = _timeToDeactiveLaser;
        slowdownEffectiveness = _slowdownEffectiveness;

        auraDamage = _auraDamage;
        auraRange = _auraRange;
        auraSlowdownEffectiveness = _auraSlowdownEffectiveness;

        turretSprite = _turretSprite;
        turretMaterial = _turretMaterial;
        cannonSprite = _cannonSprite;
        cannonMaterial = _cannonMaterial;
        missilePrefab = _missilePrefab;
        missileSize = _missileSize;
        missileSprite = _missileSprite;
        missileMaterial = _missileMaterial;
        auraSprite = _auraSprite;
        auraMaterial = _auraMaterial;

        lightSource = _lightSource;
        lightSourceInnerRadius = _lightSourceInnerRadius;
        lightSourceOuterRadius = _lightSourceOuterRadius;
    }
}
