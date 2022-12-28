using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject variant;

    private GameObject cannon;
    private GameObject aura;

    private SpriteRenderer spriteRenderer;

    public bool poisonMissile;
    public bool explosiveMissile;
    public bool slowdownMissile;
    public bool trackingMissile;
    public bool penetrationMissile;

    public float damage;
    public float range;
    public float rotationSpeed;
    public float missilesPerSecond;
    public float missileSpeed;

    public float laserHitsPerSecond;
    public float laserActivationTime;
    public float laserDeactivationTime;

    public float slowdownEffectiveness;
    public float slowdownEffectDuration;

    public float poisonDamage;
    public float poisonHitRate;
    public float poisonDuration;

    public GameObject explosionPrefab;
    public Sprite explosionSprite;
    public Material explosionMaterial;
    public float explosionDamage;
    public float explosionRange;
    public bool explosionCopyMissileEffects;

    public float auraDamage;
    public float auraRange;

    public float auraSlowdownEffectiveness;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (variant.needTarget)
        {
            cannon = Instantiate(variant.cannonPrefab, transform.position, Quaternion.identity, transform);
            cannon.GetComponent<Cannon>().SetTurret(this);
        }

        if (variant.aura)
        {
            aura = Instantiate(variant.auraPrefab, transform.position, Quaternion.identity, transform);
            aura.GetComponent<Aura>().SetTurret(this);
        }

        UpdateProperties();
    }

    public void UpdateProperties()
    {
        ClampProperties();

        spriteRenderer.sprite = variant.turretSprite;
        spriteRenderer.material = variant.turretMaterial;

        poisonMissile = variant.poisonMissile;
        slowdownMissile = variant.slowdownMissile;
        explosiveMissile = variant.explosiveMissile;
        trackingMissile = variant.trackingMissile;
        penetrationMissile = variant.penetrationMissile;

        damage = variant.damage;
        range = variant.range;
        rotationSpeed = variant.rotationSpeed;
        missilesPerSecond = variant.missilesPerSecond;
        missileSpeed = variant.missileSpeed;

        laserHitsPerSecond = variant.laserHitsPerSecond;
        laserActivationTime = variant.laserActivationTime;
        laserDeactivationTime = variant.laserDeactivationTime;

        slowdownEffectiveness = variant.slowdownEffectiveness;
        slowdownEffectDuration = variant.slowdownEffectDuration;

        poisonDamage = variant.poisonDamage;
        poisonHitRate = variant.poisonHitRate;
        poisonDuration = variant.poisonDuration;

        explosionPrefab = variant.explosionPrefab;
        explosionSprite = variant.explosionSprite;
        explosionMaterial = variant.explosionMaterial;
        explosionDamage = variant.explosionDamage;
        explosionRange = variant.explosionRange;
        explosionCopyMissileEffects = variant.copyMissileEffects;

        auraDamage = variant.auraDamage;
        auraRange = variant.auraRange;

        auraSlowdownEffectiveness = variant.auraSlowdownEffectiveness;

        if(cannon)
        {
            cannon.GetComponent<Cannon>().UpdateLaserMissileEffects();
        }
    }

    private void ClampProperties()
    {
        void ClampProperty(ref float property, FloatRangeProperty limit)
        {
            property = Mathf.Clamp(property, limit.Min, limit.Max);
        }

        ClampProperty(ref variant.damage, variant.damageLimit);
        ClampProperty(ref variant.range, variant.rangeLimit);
        ClampProperty(ref variant.rotationSpeed, variant.rotationSpeedLimit);

        ClampProperty(ref variant.missilesPerSecond, variant.missilesPerSecondLimit);
        ClampProperty(ref variant.missileSpeed, variant.missileSpeedLimit);

        ClampProperty(ref variant.laserHitsPerSecond, variant.laserHitsPerSecondLimit);
        ClampProperty(ref variant.laserActivationTime, variant.laserActivationTimeLimit);
        ClampProperty(ref variant.laserDeactivationTime, variant.laserDeactivationTimeLimit);

        ClampProperty(ref variant.slowdownEffectiveness, variant.slowdownEffectivenessLimit);
        ClampProperty(ref variant.slowdownEffectDuration, variant.slowdownEffectDurationLimit);

        ClampProperty(ref variant.poisonDamage, variant.poisonDamageLimit);
        ClampProperty(ref variant.poisonHitRate, variant.poisonHitRateLimit);
        ClampProperty(ref variant.poisonDuration, variant.poisonDamageLimit);

        ClampProperty(ref variant.explosionDamage, variant.explosionDamageLimit);
        ClampProperty(ref variant.explosionRange, variant.explosionRangeLimit);

        ClampProperty(ref variant.auraDamage, variant.auraDamageLimit);
        ClampProperty(ref variant.auraRange, variant.auraRangeLimit);

        ClampProperty(ref variant.auraSlowdownEffectiveness, variant.auraSlowdownEffectivenessLimit);
    }
}
