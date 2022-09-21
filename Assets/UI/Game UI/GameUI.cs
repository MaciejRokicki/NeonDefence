using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private VisualElement root;
    [SerializeField]
    private TurretInfoUI turretInfo;

    private VisualElement container;
    private Button backButton;

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
        float containerWidth = container.resolvedStyle.width;
        Debug.Log(containerWidth);

        if(isMenuOpen)
        {
            container.style.translate = new StyleTranslate(new Translate(containerWidth, 0.0f, 0.0f));
            backButton.Q<VisualElement>("back-button-label").style.scale = new StyleScale(new Scale(new Vector3(-1.0f, 1.0f, 0.0f)));
        }
        else
        {
            container.style.translate = new StyleTranslate(new Translate(0, 0.0f, 0.0f));
            backButton.Q<VisualElement>("back-button-label").style.scale = new StyleScale(new Scale(new Vector3(1.0f, 1.0f, 0.0f)));
        }

        isMenuOpen = !isMenuOpen;
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
