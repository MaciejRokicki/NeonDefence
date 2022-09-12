using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject data;

    private GameObject cannon;
    private GameObject aura;

    private SpriteRenderer turretSpriteRenderer;
    private SpriteRenderer cannonSpriteRenderer;
    private SpriteRenderer auraSpriteRenderer;

    private CircleCollider2D cannonCollider;
    private CircleCollider2D auraCollider;

    private void Awake()
    {
        turretSpriteRenderer = GetComponent<SpriteRenderer>();

        if(data.needTarget)
        {
            cannon = Instantiate(data.cannonPrefab, transform.position, Quaternion.identity, transform);

            cannonSpriteRenderer = cannon.GetComponent<SpriteRenderer>();
            cannonCollider = cannon.GetComponent<CircleCollider2D>();
        }

        if (data.aura)
        {
            cannon = Instantiate(data.auraPrefab, transform.position, Quaternion.identity, transform);

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
            if(!data.laser)
            {
                cannon.AddComponent<MissileCannon>();
            }
            else
            {
                cannon.AddComponent<LaserCannon>();
            }

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
