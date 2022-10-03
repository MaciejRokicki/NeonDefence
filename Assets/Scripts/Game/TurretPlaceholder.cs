using UnityEngine;

public class TurretPlaceholder : MonoBehaviour
{
    //[SerializeField]
    //private TurretInfoMenu turretInfoMenu;

    [SerializeField]
    private SpriteRenderer turretAccessibility;

    private BuildingManager buildingManager;

    private Color availableColor = new Color(0.0f, 1.0f, 0.0f, 0.6f);
    private Color unavailableColor = new Color(1.0f, 0.0f, 0.0f, 0.6f);

    private float cannonRange = 0.0f;
    private float auraRange = 0.0f;

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

    private void Available()
    {
        turretAccessibility.color = availableColor;
        //turretInfoMenu.ShowCannonRange(transform.position, cannonRange);
        //turretInfoMenu.ShowAuraRange(transform.position, auraRange);
    }

    private void Unavailable()
    {
        turretAccessibility.color = unavailableColor;
        //turretInfoMenu.HideTurretRange();
    }

    public void SetTurretVariant(TurretScriptableObject turretVariant)
    {
        cannonRange = turretVariant.needTarget ? turretVariant.range : 0.0f;
        auraRange = turretVariant.aura ? turretVariant.auraRange : 0.0f;
    }

    private void OnDisable()
    {
        //turretInfoMenu.HideTurretRange();
    }
}
