using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;
using System.Collections.Generic;
using System.Linq;

public class RollUpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private Transform iconsParent;

    private Dictionary<string, Image> icons;

    public InRunUpgrade inRunUpgrade;

    private void Awake()
    {
        icons = new();

        Image[] images = iconsParent.GetComponentsInChildren<Image>()
            .Skip(1)
            .ToArray();

        foreach(Image image in images)
        {
            icons.Add(image.name, image);
        }
    }

    private void OnEnable()
    {
        StringBuilder stringBuilder = new StringBuilder();

        background.color = inRunUpgrade.Tier.color;
        background.material = inRunUpgrade.Tier.material;

        if(inRunUpgrade is InRunGameUpgradeScriptableObject)
        {
            icons["RollUpgradeBaseIcon"].color = Color.white;
        }
        else
        {
            InRunTurretUpgradeScriptableObject turretUpgrade = inRunUpgrade as InRunTurretUpgradeScriptableObject;

            if(!turretUpgrade.turret)
            {
                foreach(Image image in icons.Values.Skip(1))
                {
                    image.color = Color.white;
                }
            }
            else
            {
                stringBuilder.Append("RollUpgrade");
                stringBuilder.Append(turretUpgrade.turret.name);
                stringBuilder.Append("Icon");

                icons[stringBuilder.ToString()].color = Color.white;
            }
        }

        stringBuilder.Clear();

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

    private void OnDisable()
    {
        Color color = new Color(0.4f, 0.4f, 0.4f, 0.4f);

        foreach (Image image in icons.Values)
        {
            image.color = color;
        }
    }
}
