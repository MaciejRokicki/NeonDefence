using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretScriptableObject", order = 3)]
public class TurretScriptableObject : ScriptableObject
{
    [SerializeField]
    private int _cost;
    public int cost;

    public bool needTarget;
    public bool aura;

    public bool missile;
    public bool laser;

    [SerializeField]
    private bool _poisonMissile;
    public bool poisonMissile;
    [SerializeField]
    private bool _slowdownMissile;
    public bool slowdownMissile;
    [SerializeField]
    private bool _explosiveMissile;
    public bool explosiveMissile;
    [SerializeField]
    private bool _copyMissileEffects;
    public bool copyMissileEffects;

    [SerializeField]
    private bool _penetrationMissile;
    public bool penetrationMissile;
    [SerializeField]
    private bool _trackingMissile;
    public bool trackingMissile;

    public bool auraSlowdown;

    [SerializeField]
    private float _damage;
    public float damage;
    [SerializeField]
    private float _range;
    public float range;
    [SerializeField]
    private float _rotationSpeed;
    public float rotationSpeed;
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
    private float _laserActivationTime;
    public float laserActivationTime;
    [SerializeField]
    private float _laserDeactivationTime;
    public float laserDeactivationTime;

    [SerializeField]
    private float _poisonDamage;
    public float poisonDamage;
    [SerializeField]
    private float _poisonHitRate;
    public float poisonHitRate;
    [SerializeField]
    private float _poisonDuration;
    public float poisonDuration;

    [SerializeField]
    private float _explosionDamage;
    public float explosionDamage;
    [SerializeField]
    private float _explosionRange;
    public float explosionRange;

    [SerializeField]
    private float _slowdownEffectiveness;
    public float slowdownEffectiveness;
    [SerializeField]
    private float _slowdownEffectDuration;
    public float slowdownEffectDuration;

    [SerializeField]
    private float _auraDamage;
    public float auraDamage;
    [SerializeField]
    private float _auraRange;
    public float auraRange;

    [SerializeField]
    private float _auraSlowdownEffectiveness;
    public float auraSlowdownEffectiveness;

    public Sprite turretIcon;
    public Material turretIconMaterial;

    public Sprite turretSprite;
    public Material turretMaterial;
    public GameObject cannonPrefab;
    public Sprite cannonSprite;
    public Material cannonMaterial;
    public GameObject missilePrefab;
    public Vector2 missileColliderOffset;
    public Vector2 missileColliderSize;
    public Vector2 missileSpriteSize;
    public Sprite missileSprite;
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

    public GameObject auraPrefab;
    public Sprite auraSprite;
    public Material auraMaterial;

    private void OnEnable()
    {
        SetProperties();
    }

    private void OnDisable()
    {
        SetProperties();
    }

    private void SetProperties()
    {
        cost = _cost;

        poisonMissile = _poisonMissile;
        slowdownMissile = _slowdownMissile;
        explosiveMissile = _explosiveMissile;

        penetrationMissile = _penetrationMissile;
        trackingMissile = _trackingMissile;

        damage = _damage;
        range = _range;
        rotationSpeed = _rotationSpeed;
        missilesPerSecond = _missilesPerSecond;
        missileSpeed = _missileSpeed;

        laserHitsPerSecond = _laserHitsPerSecond;
        laserActivationTime = _laserActivationTime;
        laserDeactivationTime = _laserDeactivationTime;

        slowdownEffectiveness = _slowdownEffectiveness;
        slowdownEffectDuration = _slowdownEffectDuration;

        poisonDamage = _poisonDamage;
        poisonHitRate = _poisonHitRate;
        poisonDuration = _poisonDuration;

        explosionDamage = _explosionDamage;
        explosionRange = _explosionRange;

        auraDamage = _auraDamage;
        auraRange = _auraRange;

        auraSlowdownEffectiveness = _auraSlowdownEffectiveness;

        explosionPrefab = _explosionPrefab;
        explosionSprite = _explosionSprite;
        explosionMaterial = _explosionMaterial;
    }
}
