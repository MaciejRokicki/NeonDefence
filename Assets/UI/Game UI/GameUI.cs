using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private VisualElement root;
    [SerializeField]
    private TurretInfoUI turretInfo;

    private VisualElement container;
    private Button backButton;

    private float menuContainerWidthPercentage = 0.75f;
    private bool isLeftSideMenu = true;
    private bool isMenuOpen = true;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        container = root.Q<VisualElement>("container");
        backButton = root.Q<Button>("back-button");

        backButton.RegisterCallback<ClickEvent>(ce => ToggleUI());
    }

    public void ToggleUI()
    {
        if(isMenuOpen)
        {
            HideSideMenu();
        }
        else
        {
            ShowSideMenu();
        }

        isMenuOpen = !isMenuOpen;
    }

    private void HideSideMenu()
    {
        container.ToggleInClassList("container-hide");
        //float containerWidth = container.resolvedStyle.width;

        //if(isLeftSideMenu)
        //{
        //    container.style.translate = new StyleTranslate(new Translate(containerWidth, 0.0f, 0.0f));
        //    //backButton.style.scale = new StyleScale(new Scale(new Vector3(-1.0f, 1.0f, 0.0f)));
        //}
        //else
        //{
        //    container.style.translate = new StyleTranslate(new Translate(-containerWidth, 0.0f, 0.0f));
        //    //backButton.style.scale = new StyleScale(new Scale(new Vector3(-1.0f, 1.0f, 0.0f)));
        //}
    }

    private void ShowSideMenu()
    {
        container.ToggleInClassList("container-hide");
        //container.style.translate = new StyleTranslate(new Translate(0, 0.0f, 0.0f));
        //backButton.style.scale = new StyleScale(new Scale(new Vector3(1.0f, 1.0f, 0.0f)));
    }

    public void ToggleMenuSide()
    {
        container.ToggleInClassList("container-right-side");
        backButton.ToggleInClassList("back-button-right-side");

        //if (isLeftSideMenu)
        //{
        //    if (isMenuOpen)
        //    {
        //        container.style.left = new StyleLength(StyleKeyword.Auto);
        //    }
        //    else
        //    {
        //        container.style.left = Screen.width * -menuContainerWidthPercentage;
        //    }

        //    container.style.right = new StyleLength(StyleKeyword.Auto);
        //    backButton.style.left = new StyleLength(StyleKeyword.Auto);
        //    backButton.style.right = -65.0f;

        //    backButton.style.scale = new StyleScale(new Scale(new Vector3(1.0f, 1.0f, 0.0f)));

        //    isLeftSideMenu = false;
        //}
        //else
        //{

        //    if (isMenuOpen)
        //    {
        //        container.style.right = Screen.width * -menuContainerWidthPercentage;
        //    }
        //    else
        //    {
        //        container.style.right = -Screen.width;
        //    }

        //    container.style.left = new StyleLength(StyleKeyword.Auto);
        //    backButton.style.right = new StyleLength(StyleKeyword.Auto);
        //    backButton.style.left = -65.0f;
        //    backButton.style.scale = new StyleScale(new Scale(new Vector3(-1.0f, 1.0f, 0.0f)));

        //    isLeftSideMenu = true;
        //}
    }

    public void ShowTurretInfo(Turret turret)
    {
        turretInfo.Show(turret);
        root.Q<VisualElement>("TurretInfo").style.display = DisplayStyle.Flex;
    }

    public void HideTurretInfo()
    {
        turretInfo.Hide();
        root.Q<VisualElement>("TurretInfo").style.display = DisplayStyle.None;
    }
}
