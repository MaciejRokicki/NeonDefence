using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private const string ELEMENT_CONTAINER = "container";
    private const string ELEMENT_HIDE_MENU_BUTTON = "hide-menu-button";
    private const string ELEMENT_HIDE_MENU_BUTTON_LABEL = "hide-menu-button-label";

    private const string STYLE_CONTAINER_RIGHT_SIDE = "container-right-side";
    private const string STYLE_HIDE_MENU_BUTTON_RIGHT_SIDE = "hide-menu-button-right-side";

    private const string STYLE_HIDE_MENU_BUTTON_LABEL_RIGHT_SIDE = "hide-menu-button-label-right-side";

    private const string STYLE_NO_TRANSITION = "no-transition";

    private const string STYLE_CONTAINER_HIDE = "container-hide";
    private const string STYLE_CONTAINER_RIGHT_SIDE_HIDE = "container-right-side-hide";

    private VisualElement root;
    private BuildingMenu buildingMenu;
    [SerializeField]
    private TurretInfoMenu turretInfoMenu;

    private VisualElement container;
    private Button hideButton;
    private Label hideButtonLabel;

    private bool isLeftSideMenu = true;
    private bool isMenuOpen = true;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        buildingMenu = GetComponent<BuildingMenu>();
    }

    private void Start()
    {
        container = root.Q<VisualElement>(ELEMENT_CONTAINER);
        hideButton = root.Q<Button>(ELEMENT_HIDE_MENU_BUTTON);
        hideButtonLabel = root.Q<Label>(ELEMENT_HIDE_MENU_BUTTON_LABEL);

        hideButton.RegisterCallback<ClickEvent>(ce => ToggleUI());
    }

    public void ToggleUI()
    {
        container.RemoveFromClassList(STYLE_NO_TRANSITION);
        hideButtonLabel.RemoveFromClassList(STYLE_NO_TRANSITION);

        string containerStyle = isLeftSideMenu ? STYLE_CONTAINER_HIDE : STYLE_CONTAINER_RIGHT_SIDE_HIDE;

        container.ToggleInClassList(containerStyle);
        hideButtonLabel.EnableInClassList(STYLE_HIDE_MENU_BUTTON_LABEL_RIGHT_SIDE, !isLeftSideMenu && !isMenuOpen || isLeftSideMenu && isMenuOpen);

        isMenuOpen = !isMenuOpen;
    }

    public void ToggleMenuSide()
    {
        isLeftSideMenu = !isLeftSideMenu;

        container.EnableInClassList(STYLE_CONTAINER_RIGHT_SIDE, !isLeftSideMenu);
        hideButton.EnableInClassList(STYLE_HIDE_MENU_BUTTON_RIGHT_SIDE, !isLeftSideMenu);
        hideButtonLabel.AddToClassList(STYLE_NO_TRANSITION);

        if (!isMenuOpen)
        {
            container.AddToClassList(STYLE_NO_TRANSITION);

            container.EnableInClassList(STYLE_CONTAINER_HIDE, isLeftSideMenu);
            container.EnableInClassList(STYLE_CONTAINER_RIGHT_SIDE_HIDE, !isLeftSideMenu);
        }

        hideButtonLabel.EnableInClassList(STYLE_HIDE_MENU_BUTTON_LABEL_RIGHT_SIDE, isLeftSideMenu && !isMenuOpen || !isLeftSideMenu && isMenuOpen);
    }

    public void ShowTurretInfo(Turret turret)
    {
        turretInfoMenu.Show(turret);
        buildingMenu.Hide();
    }

    public void HideTurretInfo()
    {
        buildingMenu.Show();
        turretInfoMenu.Hide();
    }
}
