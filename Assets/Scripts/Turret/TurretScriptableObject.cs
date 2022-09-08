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
    private bool _auraSlowdown;
    public bool auraSlowdown;

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
    private float _auraDamage;
    public float auraDamage;
    [SerializeField]
    private float _auraRange;
    public float auraRange;
    [SerializeField]
    private float _auraSlowdownEffectiveness;
    public float auraSlowdownEffectiveness;

    [SerializeField]
    private float _damage;
    public float damage;
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
    private float _range;
    public float range;
    [SerializeField]
    private float _explosionRange;
    public float explosionRange;
    [SerializeField]
    private float _rotationSpeed;
    public float rotationSpeed;
    [SerializeField]
    private float _slowdownEffectiveness;
    public float slowdownEffectiveness;

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
    private MissileScriptableObject _missileData;
    public MissileScriptableObject missileData;
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

        auraSlowdown = _auraSlowdown;

        laser = _laser;
        dealDamageOverTime = _dealDamageOverTime;
        explosiveMissile = _explosiveMissile;
        slowdownOnMissileHit = _slowdownOnMissileHit;

        auraDamage = _auraDamage;
        auraRange = _auraRange;
        auraSlowdownEffectiveness = _auraSlowdownEffectiveness;

        damage = _damage;
        damageOverTime = _damageOverTime;
        missilesPerSecond = _missilesPerSecond;
        missileSpeed = _missileSpeed;
        range = _range;
        explosionRange = _explosionRange;
        rotationSpeed = _rotationSpeed;
        slowdownEffectiveness = _slowdownEffectiveness;

        turretSprite = _turretSprite;
        turretMaterial = _turretMaterial;
        cannonSprite = _cannonSprite;
        cannonMaterial = _cannonMaterial;
        missileData = _missileData;
        auraSprite = _auraSprite;
        auraMaterial = _auraMaterial;

        lightSource = _lightSource;
        lightSourceInnerRadius = _lightSourceInnerRadius;
        lightSourceOuterRadius = _lightSourceOuterRadius;
    }
}
