using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TurretManager : MonoBehaviour
{
    private static TurretManager _instance;
    public static TurretManager instance { get { return _instance; } }

    private GameManager gameManager;
    private InputManager inputManager;

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
    public TurretScriptableObject selectedVariant;

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
        inputManager = InputManager.instance;

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
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputManager.GetClickPosition());
            Vector3Int tilePosition;

            GetTurretBuildingTile(worldPoint, out tilePosition);

            turretPlaceholder.transform.position = tilePosition;
        }
    }

    public void SelectVariant(TurretScriptableObject variant)
    {
        selectedVariant = variant;
        turretPlaceholder.gameObject.SetActive(true);
        turretPlaceholder.ShowPlaceholder(variant);
        buildingMenu.Hide();
    }

    public void BuildingManagerClickHandler(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(inputManager.GetClickPosition()), Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

            turretRange.HideTurretRange();

            if (hit)
            {
                if (hit.collider.CompareTag("Turret"))
                {
                    selectedTurret = hit.collider.GetComponent<Turret>();

                    if (selectedTurret)
                    {
                        ShowTurretDetails(selectedTurret.variant);
                        buildingMenu.Hide();

                        return;
                    }
                }
            }

            if (!inputManager.pressedUiButton)
            {
                selectedTurret = null;
                buildingMenu.Show();
            }

            if (!inputManager.pressedUiButton || (inputManager.pressedUiButton && inputManager.pressedUiButton.name == "ToggleMenuButton"))
            {
                turretDetails.Hide();
            }
        }
    }

    public TileBase GetTurretBuildingTile(Vector3 worldPoint, out Vector3Int tilePosition, bool ignoreNonBuildableLayer = false)
    {
        tilePosition = backgroundTilemap.WorldToCell(worldPoint);
        TileBase tile = backgroundTilemap.GetTile(tilePosition);

        if (!ignoreNonBuildableLayer && tile)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 0.0f, LayerMask.GetMask("NonBuildable"));

            if (hit)
            {
                tile = null;
            }
        }

        tilePosition += Vector3Int.one;

        return tile;
    }

    public void OnTurretDrop(InputAction.CallbackContext ctxt)
    {
        if (selectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(inputManager.GetClickPosition());
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

    public void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity);

        turret.GetComponent<Turret>().variant = turretVariant;

        gameManager.RemoveNeonBlocks(turretVariant.cost);
    }

    public void SellTurret()
    {
        if (selectedTurret)
        {
            gameManager.AddNeonBlocks((int)(selectedTurret.variant.cost * 0.9f));
            Destroy(selectedTurret.gameObject);

            selectedTurret = null;

            turretDetails.Hide();
            turretRange.HideTurretRange();

            buildingMenu.Show();
        }
    }

    private void ShowTurretDetails(TurretScriptableObject variant)
    {
        turretDetails.Show(variant, true);
        turretRange.ShowTurretRange(selectedTurret.transform.position, variant);
    }
}
