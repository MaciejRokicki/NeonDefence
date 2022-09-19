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
    public float damageOverTimeDuration;
    public float damageOverTimeCooldown;
    public float damageOverTime;
    public float missilesPerSecond;
    public float missileSpeed;
    public float laserHitsPerSecond;
    public float range;
    public float explosionDamage;
    public float explosionRange;
    public float rotationSpeed;
    public float laserActivationTime;
    public float laserDeactivationTime;
    public float slowdownEffectDuration;
    public float slowdownEffectiveness;

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

        spriteRenderer.sprite = variant.turretSprite;
        spriteRenderer.material = variant.turretMaterial;

        damage = variant.damage;
        damageOverTimeDuration = variant.damageOverTimeDuration;
        damageOverTimeCooldown = variant.damageOverTimeCooldown;
        damageOverTime = variant.damageOverTime;
        missilesPerSecond = variant.missilesPerSecond;
        missileSpeed = variant.missileSpeed;
        laserHitsPerSecond = variant.laserHitsPerSecond;
        range = variant.range;
        explosionDamage = variant.explosionDamage;
        explosionRange = variant.explosionRange;
        rotationSpeed = variant.rotationSpeed;
        laserActivationTime = variant.laserActivationTime;
        laserDeactivationTime = variant.laserDeactivationTime;
        slowdownEffectDuration = variant.slowdownEffectDuration;
        slowdownEffectiveness = variant.slowdownEffectiveness;

        auraDamage = variant.auraDamage;
        auraRange = variant.auraRange;
        auraSlowdownEffectiveness = variant.auraSlowdownEffectiveness;
    }

    public void Test(InputAction.CallbackContext ctxt)
    {
        Debug.Log(this.transform.position);
    }
}
