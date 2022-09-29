using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BuildingMenu : MonoBehaviour
{
    private SideMenu gameUI;

    private VisualElement root;
    [SerializeField]
    private VisualTreeAsset turretVariantDocument;
    private VisualElement content;

    [SerializeField]
    private GameObject turretPlaceholder;
    private TurretScriptableObject selectedVariant;

    private BuildingManager buildingManager;

    private void Awake()
    {
        gameUI = GetComponent<SideMenu>();

        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("BuildingMenu");
        content = root.Q<VisualElement>("building-menu-content");
    }

    private void Start()
    {
        buildingManager = BuildingManager.instance;

        foreach(TurretScriptableObject variant in buildingManager.availableTurrets)
        {
            TemplateContainer templateContainer = turretVariantDocument.Instantiate();

            templateContainer.Q<VisualElement>("turret-variant-image").style.backgroundImage = new StyleBackground(variant.turretIcon);
            templateContainer.RegisterCallback<MouseDownEvent, TurretScriptableObject>(SelectVariant, variant);

            templateContainer.Q<Label>("turret-variant-label").text = variant.cost.ToString();

            content.Add(templateContainer);
        }
    }

    private void Update()
    {
        if(selectedVariant)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            buildingManager.GetTurretBuildingTile(worldPoint, out tilePosition);

            turretPlaceholder.transform.position = tilePosition;
        }
    }

    private void SelectVariant(MouseDownEvent e, TurretScriptableObject variant)
    {
        PrepareTurretPlaceholder(variant);
        selectedVariant = variant;
    }

    public void BuildTurret(InputAction.CallbackContext ctxt)
    {
        if(selectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            UnityEngine.Tilemaps.TileBase tile = buildingManager.GetTurretBuildingTile(worldPoint, out tilePosition);

            if(tile)
            {
                buildingManager.BuildTurret(selectedVariant, tilePosition);
            }

            turretPlaceholder.SetActive(false);
            selectedVariant = null;
        }
    }

    private void PrepareTurretPlaceholder(TurretScriptableObject variant)
    {
        turretPlaceholder.SetActive(true);
        turretPlaceholder.GetComponent<TurretPlaceholder>().SetTurretVariant(variant);
        turretPlaceholder.GetComponent<SpriteRenderer>().sprite = variant.turretSprite;
        turretPlaceholder.GetComponent<SpriteMask>().sprite = variant.turretSprite;
    }

    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}
