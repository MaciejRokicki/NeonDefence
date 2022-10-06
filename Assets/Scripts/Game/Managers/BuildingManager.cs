using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public static BuildingManager instance { get { return _instance; } }

    private GameManager gameManager;
    private BuildingMenu buildingMenu;
    private TurretDetails turretDetails;

    [SerializeField]
    private Tilemap backgroundTilemap;

    [SerializeField]
    private GameObject turretPrefab;

    public TurretScriptableObject[] turretVariants;
    public List<TurretScriptableObject> availableTurrets;

    private TurretPlaceholder turretPlaceholder;
    private TurretRange turretRange;

    private Turret selectedTurret;
    private TurretScriptableObject selectedVariant;

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
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        buildingMenu = BuildingMenu.instance;
        turretDetails = TurretDetails.instance;
        turretPlaceholder = TurretPlaceholder.instance;
        turretRange = TurretRange.instance;

        availableTurrets = new List<TurretScriptableObject>();

        for(int i = 0; i < turretVariants.Length; i++)
        {
            availableTurrets.Add(turretVariants[i]);
        }
    }

    private void Update()
    {
        if (selectedVariant)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            GetTurretBuildingTile(worldPoint, out tilePosition);

            turretPlaceholder.transform.position = tilePosition;
        }
    }

    public void OnBackgroundTileClick(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;
            TileBase tile = GetTurretBuildingTile(worldPoint, out tilePosition, true);

            if (selectedTurret)
            {
                buildingMenu.Show();
                turretRange.HideTurretRange();
                selectedTurret = null;
            }

            if (tile)
            {
                selectedTurret = GetTurret(tilePosition);

                if(selectedTurret)
                {
                    TurretScriptableObject variant = selectedTurret.variant;
                    turretDetails.Show(variant, true);
                    
                    if(variant.needTarget)
                    {
                        turretRange.ShowCannonRange(selectedTurret.transform.position, variant.range);
                    }

                    if(variant.aura)
                    {
                        turretRange.ShowAuraRange(selectedTurret.transform.position, variant.auraRange);
                    }

                    buildingMenu.Hide();

                    return;
                }
            }

            turretDetails.Hide();
            turretRange.HideTurretRange();
        }
    }

    public void OnTurretDrop(InputAction.CallbackContext ctxt)
    {
        if (selectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            TileBase tile = GetTurretBuildingTile(worldPoint, out tilePosition);

            if (tile)
            {
                BuildTurret(selectedVariant, tilePosition);
            }

            turretPlaceholder.HidePlaceholder();
            selectedVariant = null;
            buildingMenu.Show();
            turretDetails.Hide();
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

    public void SelectVariant(TurretScriptableObject variant)
    {
        selectedVariant = variant;
        turretPlaceholder.gameObject.SetActive(true);
        turretPlaceholder.ShowPlaceholder(variant);
        buildingMenu.Hide();
    }

    public void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);

        turret.GetComponent<Turret>().variant = turretVariant;

        gameManager.RemoveNeonBlocks(turretVariant.cost);
    }

    public void SellTurret()
    {
        if(selectedTurret)
        {
            gameManager.AddNeonBlocks((int)(selectedTurret.variant.cost * 0.9f));
            Destroy(selectedTurret.gameObject);
        }
    }
}
