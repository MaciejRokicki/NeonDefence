using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingTurretUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private GameManager gameManager;
    private BuildingManager buildingManager;

    public TurretScriptableObject variant;

    [SerializeField]
    private GameObject priceLabelUI;

    private bool availableToPurchase;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color hoverColor;

    private Color availableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    private Color unavailableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.25f);

    private void Awake()
    {
        gameManager = GameManager.instance;
        buildingManager = BuildingManager.instance;
    }

    private void Start()
    {
        GetComponent<Image>().sprite = variant.turretIcon;
        GetComponent<Image>().material = variant.turretIconMaterial;

        priceLabelUI.GetComponent<TextMeshProUGUI>().text = variant.cost.ToString();

        gameManager.OnNeonBlockChange += OnNeonBlocksChange;

        OnNeonBlocksChange(gameManager.GetNeonBlocks());
    }

    private void OnNeonBlocksChange(int neonBlocks)
    {
        if (neonBlocks >= variant.cost)
        {
            SetAvailableToPurchase();
        }
        else
        {
            SetUnavailableToPurchase();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = defaultColor;
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
