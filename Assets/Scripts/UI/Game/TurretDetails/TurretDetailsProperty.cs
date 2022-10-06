using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretDetailsProperty : MonoBehaviour
{
    [SerializeField]
    private TurretDetails turretDetails;
    [SerializeField]
    private RectTransform propertyBar;
    [SerializeField]
    private TextMeshProUGUI propertyValue;

    private StringBuilder stringBuilder;

    [SerializeField]
    private Color barColor;
    [SerializeField]
    private Material barMaterial;
    [SerializeField]
    private bool isPercentageValue;
    [SerializeField]
    private bool secondSuffix;
    [SerializeField]
    private bool convertDiameterToRadius;

    private void Start()
    {
        turretDetails = TurretDetails.instance;
        stringBuilder = new StringBuilder();
    }

    public void SetValue(float value, float maxValue)
    {
        if(value == 0.0f)
        {
            gameObject.SetActive(false);

            return;
        }
        else
        {
            gameObject.SetActive(true);
            turretDetails.activeProperties++;
        }

        Vector2 propertyBarSize = propertyBar.transform.parent.GetComponent<RectTransform>().sizeDelta;

        propertyBar.GetComponent<Image>().color = barColor;
        propertyBar.GetComponent<Image>().material = barMaterial;
        propertyBar.offsetMax = new Vector2(-(propertyBarSize.x - propertyBarSize.x * value / maxValue), 0.0f);

        if(maxValue != 1.0f)
        {
            if(isPercentageValue)
            {
                stringBuilder
                    .Append(value * 100.0f)
                    .Append("%");
            }
            else if(secondSuffix)
            {
                stringBuilder
                    .Append(value)
                    .Append("s");
            }
            else if(convertDiameterToRadius)
            {
                stringBuilder
                    .Append((value - 1) / 2);
            }
            else
            {
                stringBuilder.Append(value);
            }
        }

        propertyValue.text = stringBuilder.ToString();

        stringBuilder.Clear();
    }
}
