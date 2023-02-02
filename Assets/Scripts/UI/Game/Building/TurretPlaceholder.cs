using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpriteMask))]
public class TurretPlaceholder : MonoBehaviour
{
    private static TurretPlaceholder _instance;
    public static TurretPlaceholder instance { get { return _instance; } }

    [SerializeField]
    private SpriteRenderer turretAccessibility;
    private TurretRange turretRange;

    private TurretManager buildingManager;

    private TurretScriptableObject variant;

    private Color availableColor = new Color(0.0f, 1.0f, 0.0f, 0.6f);
    private Color unavailableColor = new Color(1.0f, 0.0f, 0.0f, 0.6f);

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        turretRange = TurretRange.instance;
    }

    private void Start()
    {
        buildingManager = TurretManager.Instance;
    }

    private void Update()
    {
        UnityEngine.Tilemaps.TileBase tile = buildingManager.GetTurretBuildingTile(transform.position, out _);

        if(tile)
        {
            Available();
        }
        else
        {
            Unavailable();
        }
    }

    public void ShowPlaceholder(TurretScriptableObject variant)
    {
        this.variant = variant;

        GetComponent<SpriteRenderer>().sprite = variant.TurretSprite;
        GetComponent<SpriteMask>().sprite = variant.TurretSprite;
    }

    public void HidePlaceholder() => gameObject.SetActive(false);

    private void Available()
    {
        turretAccessibility.color = availableColor;
        turretRange.ShowTurretRange(transform.position, variant);
    }

    private void Unavailable()
    {
        turretAccessibility.color = unavailableColor;
        turretRange.HideTurretRange();
    }

    private void OnDisable()
    {
        turretRange.HideTurretRange();
    }
}
