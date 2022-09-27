using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager instance { get { return _instance; } }

    private UIManager uiManager;

    [SerializeField]
    private Tilemap backgroundTilemap;

    [SerializeField]
    private GameObject turretPrefab;

    [SerializeField]
    private TurretScriptableObject[] turretVariants;
    public List<TurretScriptableObject> availableTurrets;

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

        uiManager = UIManager.instance;
    }

    private void Start()
    {
        availableTurrets = new List<TurretScriptableObject>();

        for(int i = 0; i < turretVariants.Length; i++)
        {
            availableTurrets.Add(turretVariants[i]);
        }
    }

    public void OnBackgroundTileClick(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;
            TileBase tile = GetTurretBuildingTile(worldPoint, out tilePosition, true);

            if (tile)
            {
                Turret selectedTurret = GetTurret(tilePosition);

                if(selectedTurret)
                {
                    uiManager.ShowTurretInfo(selectedTurret);

                    return;
                }
            }
            
            uiManager.HideTurretInfo();
        }
    }

    private Turret GetTurret(Vector3 tilePosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(tilePosition, Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

        if(hit && hit.collider.tag == "Turret")
        {
            return hit.collider.GetComponent<Turret>();
        }
        else
        {
            return null;
        }
    }

    public TileBase GetTurretBuildingTile(Vector3 worldPoint, out Vector3Int tilePosition, bool ignoreNonBuildableLayer = false)
    {
        tilePosition = backgroundTilemap.WorldToCell(worldPoint);
        TileBase tile = backgroundTilemap.GetTile(tilePosition);

        if(!ignoreNonBuildableLayer && tile)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

            if(hit)
            {
                tile = null;
            }
        }

        tilePosition += Vector3Int.one;

        return tile;
    }

    public void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);

        turret.GetComponent<Turret>().variant = turretVariant;
    }
}
