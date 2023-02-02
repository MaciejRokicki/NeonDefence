using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TurretManager : MonoBehaviour
{
    private static TurretManager _instance;
    public static TurretManager Instance { get { return _instance; } }

    private GameManager gameManager;
    private InputManager inputManager;
    private UIManager uiManager;
    private StatisticsManager statisticsManager;

    private BuildingMenu buildingMenu;
    private TurretDetails turretDetails;

    [SerializeField]
    private Tilemap backgroundTilemap;

    [SerializeField]
    private GameObject turretPrefab;
    [SerializeField]
    private Transform turretParent;

    public TurretScriptableObject[] TurretVariants;
    public List<TurretScriptableObject> AvailableTurrets;

    private TurretPlaceholder turretPlaceholder;
    private TurretRange turretRange;

    private Turret selectedTurret;
    public TurretScriptableObject SelectedVariant;

    [SerializeField]
    private Transform turretsUI;

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
        uiManager = UIManager.instance;
        statisticsManager = StatisticsManager.instance;

        buildingMenu = BuildingMenu.instance;
        turretDetails = TurretDetails.instance;

        turretPlaceholder = TurretPlaceholder.instance;
        turretRange = TurretRange.instance;

        AvailableTurrets = new List<TurretScriptableObject>();

        for(int i = 0; i < TurretVariants.Length; i++)
        {
            AvailableTurrets.Add(TurretVariants[i]);
            TurretVariants[i].SetDefaultProperties();
        }
    }

    private void Update()
    {
        if (SelectedVariant)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(inputManager.GetClickPosition());
            Vector3Int tilePosition;

            GetTurretBuildingTile(worldPoint, out tilePosition);

            turretPlaceholder.transform.position = tilePosition;
        }
    }

    public void SelectVariant(TurretScriptableObject variant)
    {
        SelectedVariant = variant;
        turretPlaceholder.gameObject.SetActive(true);
        turretPlaceholder.ShowPlaceholder(variant);
        buildingMenu.Hide();
    }

    public void UnselectVariant()
    {
        SelectedVariant = null;
        turretRange.HideTurretRange();
        turretPlaceholder.HidePlaceholder();
        buildingMenu.Show();
        turretDetails.Hide();
    }

    public void BuildingManagerClickHandler(InputAction.CallbackContext ctxt)
    {
        if (!uiManager.blockGameInteraction && ctxt.started)
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
        if (SelectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(inputManager.GetClickPosition());
            Vector3Int tilePosition;

            TileBase tile = GetTurretBuildingTile(worldPoint, out tilePosition);

            if (tile)
            {
                BuildTurret(SelectedVariant, tilePosition);
            }

            UnselectVariant();
        }
    }

    public void BuildTurret(TurretScriptableObject turretVariant, Vector3 position)
    {
        GameObject turret = Instantiate(turretPrefab, position, Quaternion.identity, turretParent);

        turret.GetComponent<Turret>().variant = turretVariant;
        turret.GetComponent<Turret>().Cost = Mathf.RoundToInt(turretVariant.Cost * 0.9f);

        gameManager.RemoveNeonBlocks(turretVariant.Cost);
        turretVariant.Cost += Mathf.RoundToInt(turretVariant.Cost * 0.2f);

        for(int i = 0; i < turretsUI.childCount; i++)
        {
            turretsUI.GetChild(i).GetComponent<BuildingTurretUI>().UpdateCost();
        }

        statisticsManager.AddBuildedTurret(turretVariant.name);
    }

    public void SellTurret()
    {
        if (selectedTurret)
        {
            gameManager.IncreaseNeonBlocks(selectedTurret.Cost);
            Destroy(selectedTurret.gameObject);

            UnselectVariant();
        }
    }

    private void ShowTurretDetails(TurretScriptableObject variant)
    {
        turretDetails.Show(variant, selectedTurret);
        turretRange.ShowTurretRange(selectedTurret.transform.position, variant);
    }
}
