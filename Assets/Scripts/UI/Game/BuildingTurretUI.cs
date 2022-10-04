using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingTurretUI : MonoBehaviour, IPointerDownHandler
{
    private BuildingManager buildingManager;

    [SerializeField]
    private GameObject priceLabelUI;

    [HideInInspector]
    public TurretScriptableObject variant;

    private bool availableToPurchase;

    private Color availableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    private Color unavailableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.25f);

    private void Awake()
    {
        buildingManager = BuildingManager.instance;
    }

    private void Start()
    {
        GetComponent<Image>().sprite = variant.turretIcon;
        GetComponent<Image>().material = variant.turretIconMaterial;

        priceLabelUI.GetComponent<TextMeshProUGUI>().text = variant.cost.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(availableToPurchase)
        {
            buildingManager.SelectVariant(variant);
        }
    }

    public void SetAvailableToPurchase()
    {
        GetComponent<Image>().color = availableBackgroundColor;
        availableToPurchase = true;
    }

    public void SetUnavailableToPurchase()
    {
        GetComponent<Image>().color = unavailableBackgroundColor;
        availableToPurchase = false;
    }
}
