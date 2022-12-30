using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private Animator animator;
    private StringBuilder stringBuilder;

    [HideInInspector]
    public int activeProperties = 0;
    private FloatRangeProperty boolPropertyLimit;

    [SerializeField]
    private List<TurretDetailsProperty> properties;

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

        animator = GetComponent<Animator>();
        stringBuilder = new StringBuilder();

        boolPropertyLimit = new();
        boolPropertyLimit.Min = 0.0f;
        boolPropertyLimit.Max = 1.0f;
    }

    public void Show(TurretScriptableObject variant, bool showSellButton = false)
    {
        sellButton.SetActive(false);

        foreach (TurretDetailsProperty property in properties)
        {
            property.gameObject.SetActive(false);
        }

        activeProperties = 0;

        if(variant.NeedTarget)
        {
            if(variant.Missile)
            {
                properties[0].SetValue(variant.Damage, variant.TurretLimits.DamageLimit);
                properties[1].SetValue(variant.Range, variant.TurretLimits.RangeLimit);
                properties[2].SetValue(variant.RotationSpeed, variant.TurretLimits.RotationSpeedLimit);
                properties[3].SetValue(variant.MissilesPerSecond, variant.TurretLimits.MissilesPerSecondLimit);
                properties[4].SetValue(variant.MissileSpeed, variant.TurretLimits.MissileSpeedLimit);
            }

            if(variant.Laser)
            {
                properties[0].SetValue(variant.Damage, variant.TurretLimits.DamageLimit);
                properties[1].SetValue(variant.Range, variant.TurretLimits.RangeLimit);
                properties[2].SetValue(variant.RotationSpeed, variant.TurretLimits.RotationSpeedLimit);

                properties[5].SetValue(variant.LaserHitsPerSecond, variant.TurretLimits.LaserHitsPerSecondLimit);
                properties[6].SetValue(variant.LaserActivationTime, variant.TurretLimits.LaserActivationTimeLimit);
                properties[7].SetValue(variant.LaserDeactivationTime, variant.TurretLimits.LaserDeactivationTimeLimit);
            }

            if(variant.SlowdownMissile)
            {
                properties[8].SetValue(variant.SlowdownEffectiveness, variant.TurretLimits.SlowdownEffectivenessLimit);
                properties[9].SetValue(variant.SlowdownEffectDuration, variant.TurretLimits.SlowdownEffectDurationLimit);
            }

            if(variant.PoisonMissile)
            {
                properties[10].SetValue(variant.PoisonDamage, variant.TurretLimits.PoisonDamageLimit);
                properties[11].SetValue(variant.PoisonHitRate, variant.TurretLimits.PoisonHitRateLimit);
                properties[12].SetValue(variant.PoisonDuration, variant.TurretLimits.PoisonDurationLimit);
            }

            if(variant.explosiveMissile)
            {
                properties[13].SetValue(variant.ExplosionDamage, variant.TurretLimits.ExplosionDamageLimit);
                properties[14].SetValue(variant.ExplosionRange, variant.TurretLimits.ExplosionRangeLimit);
                properties[15].SetValue(variant.CopyMissileEffects ? 1.0f : 0.0f, boolPropertyLimit);
            }

            if(variant.PenetrationMissile)
            {
                properties[19].SetValue(variant.PenetrationMissile ? 1.0f : 0.0f, boolPropertyLimit);
            }

            if(variant.TrackingMissile)
            {
                properties[20].SetValue(variant.TrackingMissile ? 1.0f : 0.0f, boolPropertyLimit);
            }
        }
        
        if(variant.Aura)
        {
            properties[16].SetValue(variant.AuraDamage, variant.TurretLimits.AuraDamageLimit);
            properties[17].SetValue(variant.AuraRange, variant.TurretLimits.AuraRangeLimit);

            if(variant.AuraSlowdown)
            {
                properties[18].SetValue(variant.AuraSlowdownEffectiveness, variant.TurretLimits.AuraSlowdownEffectivenessLimit);
            }
        }

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
                .Append((int)(variant.Cost * 0.9f))
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
