using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretDetails : MonoBehaviour
{
    private static TurretDetails _instance;
    public static TurretDetails instance { get { return _instance; } }

    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private GameObject sellButton;
    [SerializeField]
    private TextMeshProUGUI sellButtonValue;

    private TurretManager buildingManager;
    private Animator animator;
    private StringBuilder stringBuilder;

    [HideInInspector]
    public int activeProperties = 0;

    [SerializeField]
    private List<TurretDetailsProperty> properties;
    [SerializeField]
    private List<float> propertiesMaxValues;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        buildingManager = TurretManager.instance;
        animator = GetComponent<Animator>();
        stringBuilder = new StringBuilder();
    }

    private void Start()
    {
        propertiesMaxValues = new List<float>();

        SetMaxValues();
    }

    private void SetMaxValues()
    {
        propertiesMaxValues.Add(buildingManager.turretVariants[0].damage);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].range);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].rotationSpeed);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].missilesPerSecond);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].missileSpeed);

        propertiesMaxValues.Add(buildingManager.turretVariants[0].laserHitsPerSecond);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].laserActivationTime);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].laserDeactivationTime);

        propertiesMaxValues.Add(buildingManager.turretVariants[0].slowdownEffectiveness);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].slowdownEffectDuration);

        propertiesMaxValues.Add(buildingManager.turretVariants[0].poisonDamage);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].poisonHitRate);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].poisonDuration);

        propertiesMaxValues.Add(buildingManager.turretVariants[0].explosionDamage);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].explosionRange);
        propertiesMaxValues.Add(1.0f);

        propertiesMaxValues.Add(buildingManager.turretVariants[0].auraDamage);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].auraRange);
        propertiesMaxValues.Add(buildingManager.turretVariants[0].auraSlowdownEffectiveness);

        propertiesMaxValues.Add(1.0f);
        propertiesMaxValues.Add(1.0f);

        foreach (TurretScriptableObject turretVariant in buildingManager.turretVariants)
        {
            propertiesMaxValues[0] = propertiesMaxValues[0] < turretVariant.damage ? turretVariant.damage : propertiesMaxValues[0];
            propertiesMaxValues[1] = propertiesMaxValues[1] < turretVariant.range ? turretVariant.range : propertiesMaxValues[1];
            propertiesMaxValues[2] = propertiesMaxValues[2] < turretVariant.rotationSpeed ? turretVariant.rotationSpeed : propertiesMaxValues[2];
            propertiesMaxValues[3] = propertiesMaxValues[3] < turretVariant.missilesPerSecond ? turretVariant.missilesPerSecond : propertiesMaxValues[3];
            propertiesMaxValues[4] = propertiesMaxValues[4] < turretVariant.missileSpeed ? turretVariant.missileSpeed : propertiesMaxValues[4];

            propertiesMaxValues[5] = propertiesMaxValues[5] < turretVariant.laserHitsPerSecond ? turretVariant.laserHitsPerSecond : propertiesMaxValues[5];
            propertiesMaxValues[6] = propertiesMaxValues[6] < turretVariant.laserActivationTime ? turretVariant.laserActivationTime : propertiesMaxValues[6];
            propertiesMaxValues[7] = propertiesMaxValues[7] < turretVariant.laserDeactivationTime ? turretVariant.laserDeactivationTime : propertiesMaxValues[7];

            propertiesMaxValues[8] = propertiesMaxValues[8] < turretVariant.slowdownEffectiveness ? turretVariant.slowdownEffectiveness : propertiesMaxValues[8];
            propertiesMaxValues[9] = propertiesMaxValues[9] < turretVariant.slowdownEffectDuration ? turretVariant.slowdownEffectDuration : propertiesMaxValues[9];

            propertiesMaxValues[10] = propertiesMaxValues[10] < turretVariant.poisonDamage ? turretVariant.poisonDamage : propertiesMaxValues[10];
            propertiesMaxValues[11] = propertiesMaxValues[11] < turretVariant.poisonHitRate ? turretVariant.poisonHitRate : propertiesMaxValues[11];
            propertiesMaxValues[12] = propertiesMaxValues[12] < turretVariant.poisonDuration ? turretVariant.poisonDuration : propertiesMaxValues[12];

            propertiesMaxValues[13] = propertiesMaxValues[13] < turretVariant.explosionDamage ? turretVariant.explosionDamage : propertiesMaxValues[13];
            propertiesMaxValues[14] = propertiesMaxValues[14] < turretVariant.explosionRange ? turretVariant.explosionRange : propertiesMaxValues[14];

            propertiesMaxValues[16] = propertiesMaxValues[16] < turretVariant.auraDamage ? turretVariant.auraDamage : propertiesMaxValues[16];
            propertiesMaxValues[17] = propertiesMaxValues[17] < turretVariant.auraRange ? turretVariant.auraRange : propertiesMaxValues[17];
            propertiesMaxValues[18] = propertiesMaxValues[18] < turretVariant.auraSlowdownEffectiveness ? turretVariant.auraSlowdownEffectiveness : propertiesMaxValues[18];
        }
    }

    public void Show(TurretScriptableObject variant, bool showSellButton = false)
    {
        SetMaxValues();

        sellButton.SetActive(false);

        activeProperties = 0;

        properties[0].SetValue(variant.damage, propertiesMaxValues[0]);
        properties[1].SetValue(variant.range, propertiesMaxValues[1]);
        properties[2].SetValue(variant.rotationSpeed, propertiesMaxValues[2]);
        properties[3].SetValue(variant.missilesPerSecond, propertiesMaxValues[3]);
        properties[4].SetValue(variant.missileSpeed, propertiesMaxValues[4]);

        properties[5].SetValue(variant.laserHitsPerSecond, propertiesMaxValues[5]);
        properties[6].SetValue(variant.laserActivationTime, propertiesMaxValues[6]);
        properties[7].SetValue(variant.laserDeactivationTime, propertiesMaxValues[7]);

        properties[8].SetValue(variant.slowdownEffectiveness, propertiesMaxValues[8]);
        properties[9].SetValue(variant.slowdownEffectDuration, propertiesMaxValues[9]);

        properties[10].SetValue(variant.poisonDamage, propertiesMaxValues[10]);
        properties[11].SetValue(variant.poisonHitRate, propertiesMaxValues[11]);
        properties[12].SetValue(variant.poisonDuration, propertiesMaxValues[12]);

        properties[13].SetValue(variant.explosionDamage, propertiesMaxValues[13]);
        properties[14].SetValue(variant.explosionRange, propertiesMaxValues[14]);
        properties[15].SetValue(variant.copyMissileEffects ? 1.0f : 0.0f, propertiesMaxValues[15]);

        properties[16].SetValue(variant.auraDamage, propertiesMaxValues[16]);
        properties[17].SetValue(variant.auraRange, propertiesMaxValues[17]);

        properties[18].SetValue(variant.auraSlowdownEffectiveness, propertiesMaxValues[18]);

        properties[19].SetValue(variant.penetrationMissile ? 1.0f : 0.0f, propertiesMaxValues[19]);
        properties[20].SetValue(variant.trackingMissile ? 1.0f : 0.0f, propertiesMaxValues[20]);

        activeProperties = activeProperties % 2 == 0 ? activeProperties / 2 : (activeProperties + 1) / 2;

        Vector2 cellSize = content.GetComponent<GridLayoutGroup>().cellSize;
        float padding = 20.0f;
        float spacing = content.GetComponent<GridLayoutGroup>().spacing.y;
        float containerHeight = padding + activeProperties * cellSize.y + (activeProperties - 1) * spacing;

        GetComponent<RectTransform>().sizeDelta = new Vector2(450.0f, containerHeight);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(225.0f, containerHeight / 2 + 70.0f);

        if (showSellButton)
        {
            stringBuilder.Clear();

            stringBuilder
                .Append("Sell (")
                .Append((int)(variant.cost * 0.9f))
                .Append(")");

            sellButtonValue.text = stringBuilder.ToString();
            sellButton.SetActive(true);
        }

        animator.SetBool("isOpen", true);
    }

    public void Hide()
    {
        animator.SetBool("isOpen", false);
    }
}
