using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class BuildingTurretUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    private TurretManager buildingManager;
    private TurretDetails turretDetails;

    public TurretScriptableObject variant;

    [SerializeField]
    private GameObject priceLabelUI;

    private bool availableToPurchase;
    private bool onTouchDrag = false;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color hoverColor;

    private Color availableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.75f);
    private Color unavailableBackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.25f);

    private void Start()
    {
        gameManager = GameManager.instance;
        buildingManager = TurretManager.instance;
        turretDetails = TurretDetails.instance;

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

    public void OnClickDown()
    {
        if(availableToPurchase)
        {
            if (Touch.activeFingers.Count == 0)
            {
                buildingManager.SelectVariant(variant);
                turretDetails.Show(variant);
            }
            else if (Touch.activeFingers.Count == 1)
            {
                if (Touch.activeFingers[0].currentTouch.phase == TouchPhase.Moved)
                {
                    if (!onTouchDrag)
                    {
                        buildingManager.SelectVariant(variant);
                        turretDetails.Show(variant);
                    }

                    onTouchDrag = true;
                }
            }
        }
    }

    public void OnDrop()
    {
        if(Touch.activeFingers.Count == 1)
        {
            onTouchDrag = false;
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
