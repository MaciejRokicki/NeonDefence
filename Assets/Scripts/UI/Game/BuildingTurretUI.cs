using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingTurretUI : MonoBehaviour, IPointerDownHandler
{
    private BuildingMenu buildingMenu;
    [HideInInspector]
    public TurretScriptableObject variant;

    private void Awake()
    {
        buildingMenu = transform.parent.GetComponent<BuildingMenu>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buildingMenu.SelectVariant(variant);
    }
}
