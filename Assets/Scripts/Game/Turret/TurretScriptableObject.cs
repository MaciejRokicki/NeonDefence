using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretScriptableObject", order = 3)]
public class TurretScriptableObject : ScriptableObject
{
    [SerializeField]
    private float _cost;
    public float cost;

    public bool needTarget;
    public bool aura;

    public bool missile;
    public bool laser;

    public bool dealDamageOverTime;
    public bool explosiveMissile;
    public bool slowdownOnMissileHit;

    public bool penetrationMissile;
    public bool trackingMissile;
    public bool copyMissileEffects;

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
    private float _slowdownEffectiveness;
    public float slowdownEffectiveness;
    [SerializeField]
    private float _slowdownEffectDuration;
    public float slowdownEffectDuration;

    [SerializeField]
    private float _damageOverTime;
    public float damageOverTime;
    [SerializeField]
    private float _damageOverTimeHitCooldown;
    public float damageOverTimeHitCooldown;
    [SerializeField]
    private float _damageOverTimeDuration;
    public float damageOverTimeDuration;

    [SerializeField]
    private float _explosionDamage;
    public float explosionDamage;
    [SerializeField]
    private float _explosionRange;
    public float explosionRange;

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
    public GameObject explosionPrefab;
    public Sprite explosionSprite;
    public Material explosionMaterial;
    public GameObject auraPrefab;
    public Sprite auraSprite;
    public Material auraMaterial;

    private void OnEnable()
    {
        cost = _cost;

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

        damageOverTime = _damageOverTime;
        damageOverTimeHitCooldown = _damageOverTimeHitCooldown;
        damageOverTimeDuration = _damageOverTimeDuration;

        explosionDamage = _explosionDamage;
        explosionRange = _explosionRange;

        auraDamage = _auraDamage;
        auraRange = _auraRange;

        auraSlowdownEffectiveness = _auraSlowdownEffectiveness;
    }
}
