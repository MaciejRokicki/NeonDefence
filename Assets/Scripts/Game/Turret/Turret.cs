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

        SetProperites();
    }

    public void SetProperites()
    {
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
}
