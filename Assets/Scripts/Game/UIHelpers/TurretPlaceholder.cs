using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpriteMask))]
public class TurretPlaceholder : MonoBehaviour
{
    private static TurretPlaceholder _instance;
    public static TurretPlaceholder instance { get { return _instance; } }

    [SerializeField]
    private SpriteRenderer turretAccessibility;
    private TurretRange turretRange;

    private BuildingManager buildingManager;

    private Color availableColor = new Color(0.0f, 1.0f, 0.0f, 0.6f);
    private Color unavailableColor = new Color(1.0f, 0.0f, 0.0f, 0.6f);

    private float cannonRange = 0.0f;
    private float auraRange = 0.0f;

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
        buildingManager = BuildingManager.instance;
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
        cannonRange = variant.needTarget ? variant.range : 0.0f;
        auraRange = variant.aura ? variant.auraRange : 0.0f;

        GetComponent<SpriteRenderer>().sprite = variant.turretSprite;
        GetComponent<SpriteMask>().sprite = variant.turretSprite;
    }

    public void HidePlaceholder() => gameObject.SetActive(false);

    private void Available()
    {
        turretAccessibility.color = availableColor;
        turretRange.ShowCannonRange(transform.position, cannonRange);
        turretRange.ShowAuraRange(transform.position, auraRange);
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
