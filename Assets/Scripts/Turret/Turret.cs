using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public TurretScriptableObject data;

    private GameObject cannon;
    private GameObject aura; //TODO: todo

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(data.needTarget)
        {
            cannon = Instantiate(data.cannonPrefab, transform.position, Quaternion.identity, transform);
        }

        if (data.aura)
        {
            aura = Instantiate(data.auraPrefab, transform.position, Quaternion.identity, transform);
        }

        cannon.GetComponent<Cannon>().SetTurret(this);
    }

    private void Start()
    {
        spriteRenderer.sprite = data.turretSprite;
        spriteRenderer.material = data.turretMaterial;
    }
}
