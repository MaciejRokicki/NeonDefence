using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private VisualElement root;
    private BuildingMenu buildingMenu;
    [SerializeField]
    private TurretInfoMenu turretInfoMenu;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        buildingMenu = GetComponent<BuildingMenu>();
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
