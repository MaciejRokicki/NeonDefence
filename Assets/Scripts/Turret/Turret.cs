using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject data;

    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private GameObject aura;

    [SerializeField]
    private SpriteRenderer turretSpriteRenderer;
    [SerializeField]
    private SpriteRenderer cannonSpriteRenderer;
    [SerializeField]
    private SpriteRenderer missileSpriteRenderer;
    [SerializeField]
    private SpriteRenderer auraSpriteRenderer;

    [SerializeField]
    private CircleCollider2D cannonCollider;
    [SerializeField]
    private CircleCollider2D auraCollider;


    public float rotationSpeed;

    private void Awake()
    {
        cannon = transform.Find("Cannon").gameObject;
        missile = transform.Find("Missile").gameObject;
        aura = transform.Find("Aura").gameObject;

        turretSpriteRenderer = GetComponent<SpriteRenderer>();

        if(data.needTarget)
        {
            cannonSpriteRenderer = cannon.GetComponent<SpriteRenderer>();
            missileSpriteRenderer = missile.GetComponent<SpriteRenderer>();

            cannonCollider = cannon.GetComponent<CircleCollider2D>();
        }

        if (data.aura)
        {
            auraSpriteRenderer = aura.GetComponent<SpriteRenderer>();

            auraCollider = aura.GetComponent<CircleCollider2D>();
        }
    }

    private void Start()
    {
        turretSpriteRenderer.sprite = data.turretSprite;
        turretSpriteRenderer.material = data.turretMaterial;
        
        if(data.needTarget)
        {
            cannonSpriteRenderer.sprite = data.cannonSprite;
            cannonSpriteRenderer.material = data.cannonMaterial;

            missileSpriteRenderer.sprite = data.missileSprite;
            missileSpriteRenderer.material = data.missileMaterial;

            cannonCollider.radius = data.range + 0.5f;

            rotationSpeed = data.rotationSpeed;
        }


        if (data.aura)
        {
            auraSpriteRenderer.sprite = data.cannonSprite;
            auraSpriteRenderer.material = data.cannonMaterial;

            auraCollider.radius = data.auraRange + 0.5f;
        }
    }

    private void Update()
    {
        
    }
}
