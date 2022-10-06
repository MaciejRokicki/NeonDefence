using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretDetailsProperty : MonoBehaviour
{
    [SerializeField]
    private RectTransform propertyBar;
    [SerializeField]
    private TextMeshProUGUI propertyValue;

    [SerializeField]
    private Color barColor;

    public void SetValue(float value, float maxValue)
    {
        propertyBar.GetComponent<Image>().color = barColor;
        propertyValue.text = value.ToString();
    }
}
