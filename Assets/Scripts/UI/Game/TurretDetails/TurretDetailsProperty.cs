using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    private bool boolValue;

    private void Start()
    {
        turretDetails = TurretDetails.instance;
        stringBuilder = new StringBuilder();
    }

    public void SetValue(float value, FloatRangeProperty limit)
    {
        gameObject.SetActive(true);

        turretDetails.activeProperties++;

        Vector2 propertyBarSize = propertyBar.transform.parent.GetComponent<RectTransform>().sizeDelta;

        propertyBar.GetComponent<Image>().color = barColor;
        propertyBar.GetComponent<Image>().material = barMaterial;
        propertyBar.offsetMax = new Vector2(-(propertyBarSize.x - propertyBarSize.x * value / limit.Max), 0.0f);

        if (!boolValue)
        {
            if (isPercentageValue)
            {
                stringBuilder
                    .Append(value * 100.0f)
                    .Append("%");
            }
            else if (secondSuffix)
            {
                stringBuilder
                    .Append(value)
                    .Append("s");
            }
            else if (convertDiameterToRadius)
            {
                stringBuilder
                    .Append((value - 1) / 2);
            }
            else
            {
                stringBuilder.Append(value.ToString("0.0"));
            }
        }

        propertyValue.text = stringBuilder.ToString();

        stringBuilder.Clear();
    }
}
