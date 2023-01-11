using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretScriptableObject", order = 3)]
public class TurretScriptableObject : ScriptableObject
{
    [SerializeField]
    private int _cost;
    public int Cost;

    public bool NeedTarget;
    public bool Aura;

    public TurretLimitsScriptableObject TurretLimits;

    public bool Missile;
    public bool Laser;

    [SerializeField]
    private bool _poisonMissile;
    public bool PoisonMissile;
    [SerializeField]
    private bool _slowdownMissile;
    public bool SlowdownMissile;
    [SerializeField]
    private bool _explosiveMissile;
    public bool explosiveMissile;
    [SerializeField]
    private bool _copyMissileEffects;
    public bool CopyMissileEffects;

    [SerializeField]
    private bool _penetrationMissile;
    public bool PenetrationMissile;
    [SerializeField]
    private bool _trackingMissile;
    public bool TrackingMissile;

    [SerializeField]
    private bool _auraSlowdown;
    public bool AuraSlowdown;

    [SerializeField]
    private float _damage;
    public float Damage;
    [SerializeField]
    private float _range;
    public float Range;
    [SerializeField]
    private float _rotationSpeed;
    public float RotationSpeed;
    [SerializeField]
    private float _missilesPerSecond;
    public float MissilesPerSecond;
    [SerializeField]
    private float _missileSpeed;
    public float MissileSpeed;

    [SerializeField]
    private float _laserHitsPerSecond;
    public float LaserHitsPerSecond;
    [SerializeField]
    private float _laserActivationTime;
    public float LaserActivationTime;
    [SerializeField]
    private float _laserDeactivationTime;
    public float LaserDeactivationTime;

    [SerializeField]
    private float _poisonDamage;
    public float PoisonDamage;
    [SerializeField]
    private float _poisonHitRate;
    public float PoisonHitRate;
    [SerializeField]
    private float _poisonDuration;
    public float PoisonDuration;

    [SerializeField]
    private float _explosionDamage;
    public float ExplosionDamage;
    [SerializeField]
    private float _explosionRange;
    public float ExplosionRange;

    [SerializeField]
    private float _slowdownEffectiveness;
    public float SlowdownEffectiveness;
    [SerializeField]
    private float _slowdownEffectDuration;
    public float SlowdownEffectDuration;

    [SerializeField]
    private float _auraDamage;
    public float AuraDamage;
    [SerializeField]
    private float _auraRange;
    public float AuraRange;

    [SerializeField]
    private float _auraSlowdownEffectiveness;
    public float AuraSlowdownEffectiveness;

    public Sprite TurretIcon;
    public Material TurretIconMaterial;

    public Sprite TurretSprite;
    public Material TurretMaterial;
    public GameObject CannonPrefab;
    public Sprite CannonSprite;
    public Material CannonMaterial;
    public GameObject MissilePrefab;
    public Vector2 MissileColliderOffset;
    public Vector2 MissileColliderSize;
    public Vector2 MissileSpriteSize;
    public Sprite MissileSprite;
    public Material MissileMaterial;

    [SerializeField]
    private GameObject _explosionPrefab;
    public GameObject ExplosionPrefab;
    [SerializeField]
    private Sprite _explosionSprite;
    public Sprite ExplosionSprite;
    [SerializeField]
    private Material _explosionMaterial;
    public Material ExplosionMaterial;

    public GameObject AuraPrefab;
    public Sprite AuraSprite;
    public Material AuraMaterial;

    private void OnEnable()
    {
        SetDefaultProperties();
    }

    private void OnDisable()
    {
        SetDefaultProperties();
    }

    public float GetBaseDamage() => _damage;
    public float GetBaseRange() => _range;
    public float GetBaseRotationSpeed() => _rotationSpeed;
    public float GetBaseMissilesPerSecond() => _missilesPerSecond;
    public float GetBaseMissileSpeed() => _missileSpeed;
    public float GetBaseLaserHitsPerSecond() => _laserHitsPerSecond;
    public float GetBaseLaserActivationTime() => _laserActivationTime;
    public float GetBaseLaserDeactivationTime() => _laserDeactivationTime;
    public float GetBasePoisonDamage() => _poisonDamage;
    public float GetBasePoisonHitRate() => _poisonHitRate;
    public float GetBasePoisonDuration() => _poisonDuration;
    public float GetBaseExplosionDamage() => _explosionDamage;
    public float GetBaseExplosionRange() => _explosionRange;
    public float GetBaseSlowdownEffectiveness() => _slowdownEffectiveness;
    public float GetBaseSlowdownEffectDuration() => _slowdownEffectDuration;
    public float GetBaseAuraDamage() => _auraDamage;
    public float GetBaseAuraRange() => _auraRange;
    public float GetBaseAuraSlowdownEffectiveness() => _auraSlowdownEffectiveness;

    public void SetDefaultProperties()
    {
        Cost = _cost;

        PoisonMissile = _poisonMissile;
        SlowdownMissile = _slowdownMissile;
        explosiveMissile = _explosiveMissile;

        PenetrationMissile = _penetrationMissile;
        TrackingMissile = _trackingMissile;

        AuraSlowdown = _auraSlowdown;

        Damage = _damage;
        Range = _range;
        RotationSpeed = _rotationSpeed;
        MissilesPerSecond = _missilesPerSecond;
        MissileSpeed = _missileSpeed;

        LaserHitsPerSecond = _laserHitsPerSecond;
        LaserActivationTime = _laserActivationTime;
        LaserDeactivationTime = _laserDeactivationTime;

        SlowdownEffectiveness = _slowdownEffectiveness;
        SlowdownEffectDuration = _slowdownEffectDuration;

        PoisonDamage = _poisonDamage;
        PoisonHitRate = _poisonHitRate;
        PoisonDuration = _poisonDuration;

        ExplosionDamage = _explosionDamage;
        ExplosionRange = _explosionRange;

        AuraDamage = _auraDamage;
        AuraRange = _auraRange;

        AuraSlowdownEffectiveness = _auraSlowdownEffectiveness;

        ExplosionPrefab = _explosionPrefab;
        ExplosionSprite = _explosionSprite;
        ExplosionMaterial = _explosionMaterial;
    }
}
