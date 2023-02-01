using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject variant;

    public GameObject cannon;
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
        if (variant.NeedTarget)
        {
            cannon = Instantiate(variant.CannonPrefab, transform.position, Quaternion.identity, transform);
            cannon.GetComponent<Cannon>().SetTurret(this);
        }

        if (variant.Aura)
        {
            aura = Instantiate(variant.AuraPrefab, transform.position, Quaternion.identity, transform);
            aura.GetComponent<Aura>().SetTurret(this);
        }

        UpdateProperties();
    }

    public void UpdateProperties()
    {
        ClampProperties();

        spriteRenderer.sprite = variant.TurretSprite;
        spriteRenderer.material = variant.TurretMaterial;

        poisonMissile = variant.PoisonMissile;
        slowdownMissile = variant.SlowdownMissile;
        explosiveMissile = variant.explosiveMissile;
        trackingMissile = variant.TrackingMissile;
        penetrationMissile = variant.PenetrationMissile;

        damage = variant.Damage;
        range = variant.Range;
        rotationSpeed = variant.RotationSpeed;
        missilesPerSecond = variant.MissilesPerSecond;
        missileSpeed = variant.MissileSpeed;

        laserHitsPerSecond = variant.LaserHitsPerSecond;
        laserActivationTime = variant.LaserActivationTime;
        laserDeactivationTime = variant.LaserDeactivationTime;

        slowdownEffectiveness = variant.SlowdownEffectiveness;
        slowdownEffectDuration = variant.SlowdownEffectDuration;

        poisonDamage = variant.PoisonDamage;
        poisonHitRate = variant.PoisonHitRate;
        poisonDuration = variant.PoisonDuration;

        explosionPrefab = variant.ExplosionPrefab;
        explosionSprite = variant.ExplosionSprite;
        explosionMaterial = variant.ExplosionMaterial;
        explosionDamage = variant.ExplosionDamage;
        explosionRange = variant.ExplosionRange;
        explosionCopyMissileEffects = variant.CopyMissileEffects;

        auraDamage = variant.AuraDamage;
        auraRange = variant.AuraRange;

        auraSlowdownEffectiveness = variant.AuraSlowdownEffectiveness;

        if(cannon)
        {
            cannon.GetComponent<Cannon>().UpdateCannon();
        }

        if(aura)
        {
            CircleCollider2D auraCircleCollider2D = transform.GetChild(0).GetComponent<CircleCollider2D>();

            auraCircleCollider2D.radius = auraRange / 2f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(auraRange, auraRange);
        }
    }

    private void ClampProperties()
    {
        void ClampProperty(ref float property, FloatRangeProperty limit)
        {
            property = Mathf.Clamp(property, limit.Min, limit.Max);
        }

        ClampProperty(ref variant.Damage, variant.TurretLimits.DamageLimit);
        ClampProperty(ref variant.Range, variant.TurretLimits.RangeLimit);
        ClampProperty(ref variant.RotationSpeed, variant.TurretLimits.RotationSpeedLimit);

        ClampProperty(ref variant.MissilesPerSecond, variant.TurretLimits.MissilesPerSecondLimit);
        ClampProperty(ref variant.MissileSpeed, variant.TurretLimits.MissileSpeedLimit);

        ClampProperty(ref variant.LaserHitsPerSecond, variant.TurretLimits.LaserHitsPerSecondLimit);
        ClampProperty(ref variant.LaserActivationTime, variant.TurretLimits.LaserActivationTimeLimit);
        ClampProperty(ref variant.LaserDeactivationTime, variant.TurretLimits.LaserDeactivationTimeLimit);

        ClampProperty(ref variant.SlowdownEffectiveness, variant.TurretLimits.SlowdownEffectivenessLimit);
        ClampProperty(ref variant.SlowdownEffectDuration, variant.TurretLimits.SlowdownEffectDurationLimit);

        ClampProperty(ref variant.PoisonDamage, variant.TurretLimits.PoisonDamageLimit);
        ClampProperty(ref variant.PoisonHitRate, variant.TurretLimits.PoisonHitRateLimit);
        ClampProperty(ref variant.PoisonDuration, variant.TurretLimits.PoisonDamageLimit);

        ClampProperty(ref variant.ExplosionDamage, variant.TurretLimits.ExplosionDamageLimit);
        ClampProperty(ref variant.ExplosionRange, variant.TurretLimits.ExplosionRangeLimit);

        ClampProperty(ref variant.AuraDamage, variant.TurretLimits.AuraDamageLimit);
        ClampProperty(ref variant.AuraRange, variant.TurretLimits.AuraRangeLimit);

        ClampProperty(ref variant.AuraSlowdownEffectiveness, variant.TurretLimits.AuraSlowdownEffectivenessLimit);
    }
}
