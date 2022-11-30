using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject variant;

    private GameObject cannon;
    private GameObject aura;

    private SpriteRenderer spriteRenderer;

    //TODO: hide in inspector
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

    public float explosionDamage;
    public float explosionRange;

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

        explosionDamage = variant.explosionDamage;
        explosionRange = variant.explosionRange;

        auraDamage = variant.auraDamage;
        auraRange = variant.auraRange;

        auraSlowdownEffectiveness = variant.auraSlowdownEffectiveness;
    }
}
