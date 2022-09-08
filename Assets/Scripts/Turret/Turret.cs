using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject data;

    public GameObject missilePrefab;

    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private GameObject aura;

    [SerializeField]
    private SpriteRenderer turretSpriteRenderer;
    [SerializeField]
    private SpriteRenderer cannonSpriteRenderer;
    [SerializeField]
    private SpriteRenderer auraSpriteRenderer;

    [SerializeField]
    private CircleCollider2D cannonCollider;
    [SerializeField]
    private CircleCollider2D auraCollider;

    private void Awake()
    {
        cannon = transform.Find("Cannon").gameObject;
        aura = transform.Find("Aura").gameObject;

        turretSpriteRenderer = GetComponent<SpriteRenderer>();

        if(data.needTarget)
        {
            cannonSpriteRenderer = cannon.GetComponent<SpriteRenderer>();

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

            cannonCollider.radius = data.range + 0.5f;
        }


        if (data.aura)
        {
            auraSpriteRenderer.sprite = data.cannonSprite;
            auraSpriteRenderer.material = data.cannonMaterial;

            auraCollider.radius = data.auraRange + 0.5f;
        }
    }
}
