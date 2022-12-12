using System.Text;
using System.Reflection;
using UnityEngine;
using TMPro;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

public class RollUpgradeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI description;

    public InRunUpgrade inRunUpgrade;

    private void OnEnable()
    {
        StringBuilder stringBuilder = new StringBuilder();

        FieldInfo[] properties = inRunUpgrade.GetType().GetFields();

        foreach (FieldInfo property in properties)
        {
            if (property.FieldType == typeof(int) && (int)property.GetValue(inRunUpgrade) != default)
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(property.GetValue(inRunUpgrade));
                stringBuilder.Append("\n");
            }

            if (property.FieldType == typeof(float) && (float)property.GetValue(inRunUpgrade) != default)
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(property.GetValue(inRunUpgrade));
                stringBuilder.Append("\n");
            }

            if (property.FieldType == typeof(bool) && (bool)property.GetValue(inRunUpgrade) != default)
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append("\n");
            }
        }

        description.text = stringBuilder.ToString();
    }
}
