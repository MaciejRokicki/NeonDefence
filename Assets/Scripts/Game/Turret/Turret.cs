using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject variant;

    public int Cost;

    public GameObject Cannon;
    private GameObject aura;

    private SpriteRenderer spriteRenderer;

    public bool PoisonMissile;
    public bool ExplosiveMissile;
    public bool SlowdownMissile;
    public bool TrackingMissile;
    public bool PenetrationMissile;

    public float Damage;
    public float Range;
    public float RotationSpeed;
    public float MissilesPerSecond;
    public float MissileSpeed;

    public float LaserHitsPerSecond;
    public float LaserActivationTime;
    public float LaserDeactivationTime;

    public float SlowdownEffectiveness;
    public float SlowdownEffectDuration;

    public float PoisonDamage;
    public float PoisonHitRate;
    public float PoisonDuration;

    public GameObject ExplosionPrefab;
    public Sprite ExplosionSprite;
    public Material ExplosionMaterial;
    public float ExplosionDamage;
    public float ExplosionRange;
    public bool ExplosionCopyMissileEffects;

    public float AuraDamage;
    public float AuraRange;

    public float AuraSlowdownEffectiveness;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (variant.NeedTarget)
        {
            Cannon = Instantiate(variant.CannonPrefab, transform.position, Quaternion.identity, transform);
            Cannon.GetComponent<Cannon>().SetTurret(this);
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

        PoisonMissile = variant.PoisonMissile;
        SlowdownMissile = variant.SlowdownMissile;
        ExplosiveMissile = variant.explosiveMissile;
        TrackingMissile = variant.TrackingMissile;
        PenetrationMissile = variant.PenetrationMissile;

        Damage = variant.Damage;
        Range = variant.Range;
        RotationSpeed = variant.RotationSpeed;
        MissilesPerSecond = variant.MissilesPerSecond;
        MissileSpeed = variant.MissileSpeed;

        LaserHitsPerSecond = variant.LaserHitsPerSecond;
        LaserActivationTime = variant.LaserActivationTime;
        LaserDeactivationTime = variant.LaserDeactivationTime;

        SlowdownEffectiveness = variant.SlowdownEffectiveness;
        SlowdownEffectDuration = variant.SlowdownEffectDuration;

        PoisonDamage = variant.PoisonDamage;
        PoisonHitRate = variant.PoisonHitRate;
        PoisonDuration = variant.PoisonDuration;

        ExplosionPrefab = variant.ExplosionPrefab;
        ExplosionSprite = variant.ExplosionSprite;
        ExplosionMaterial = variant.ExplosionMaterial;
        ExplosionDamage = variant.ExplosionDamage;
        ExplosionRange = variant.ExplosionRange;
        ExplosionCopyMissileEffects = variant.CopyMissileEffects;

        AuraDamage = variant.AuraDamage;
        AuraRange = variant.AuraRange;

        AuraSlowdownEffectiveness = variant.AuraSlowdownEffectiveness;

        if(Cannon)
        {
            Cannon.GetComponent<Cannon>().UpdateCannon();
        }

        if(aura)
        {
            CircleCollider2D auraCircleCollider2D = transform.GetChild(0).GetComponent<CircleCollider2D>();

            auraCircleCollider2D.radius = AuraRange / 2f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(AuraRange, AuraRange);
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
