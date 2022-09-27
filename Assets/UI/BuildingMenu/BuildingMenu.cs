using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BuildingMenu : MonoBehaviour
{
    private GameUI gameUI;

    private VisualElement root;
    [SerializeField]
    private VisualTreeAsset turretVariantDocument;
    private VisualElement content;

    private TurretScriptableObject selectedVariant;

    private BuildingManager buildingManager;

    private void Awake()
    {
        gameUI = GetComponent<GameUI>();

        root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("BuildingMenu");
        content = root.Q<VisualElement>("building-menu-content");
    }

    private void Start()
    {
        buildingManager = BuildingManager.instance;

        foreach(TurretScriptableObject variant in buildingManager.availableTurrets)
        {
            TemplateContainer templateContainer = turretVariantDocument.Instantiate();

            templateContainer.Q<VisualElement>("turret-variant-image").style.backgroundImage = new StyleBackground(variant.turretSprite);
            templateContainer.RegisterCallback<MouseDownEvent, TurretScriptableObject>(SelectVariant, variant);

            templateContainer.Q<Label>("turret-variant-label").text = variant.cost.ToString();

            content.Add(templateContainer);
        }
    }

    private void Update()
    {
        if(selectedVariant)
        {
            Debug.Log("TESt");
        }
    }

    private void SelectVariant(MouseDownEvent e, TurretScriptableObject variant)
    {
        selectedVariant = variant;
        gameUI.ToggleUI();
    }

    public void BuildTurret(InputAction.CallbackContext ctxt)
    {
        if(selectedVariant && ctxt.canceled)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3Int tilePosition;

            buildingManager.GetTurretBuildingTile(worldPoint, out tilePosition);
            tilePosition += Vector3Int.one;

            buildingManager.BuildTurret(selectedVariant, tilePosition);

            selectedVariant = null;
            gameUI.ToggleUI();
        }
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
