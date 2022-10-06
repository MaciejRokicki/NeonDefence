using UnityEngine;

public class TurretDetails : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject variant;

    [SerializeField]
    private TurretDetailsProperty damageProperty;
    [SerializeField]
    private TurretDetailsProperty rangeProperty;
    [SerializeField]
    private TurretDetailsProperty rotationSpeedProperty;
    [SerializeField]
    private TurretDetailsProperty missilePerSecondProperty;
    [SerializeField]
    private TurretDetailsProperty missileSpeedProperty;
    [SerializeField]
    private TurretDetailsProperty slowdownEffectivenessProperty;
    [SerializeField]
    private TurretDetailsProperty slowdownDurationProperty;
    [SerializeField]
    private TurretDetailsProperty poisonDamageProperty;
    [SerializeField]
    private TurretDetailsProperty poisonHitRateProperty;
    [SerializeField]
    private TurretDetailsProperty poisonDurationProperty;
    [SerializeField]
    private TurretDetailsProperty explosionDamageProperty;
    [SerializeField]
    private TurretDetailsProperty explosionRangeProperty;
    [SerializeField]
    private TurretDetailsProperty explosionCopyMissileEffectsProperty;
    [SerializeField]
    private TurretDetailsProperty penetrationMissileProperty;
    [SerializeField]
    private TurretDetailsProperty trackingMissileProperty;

    public void Show()
    {
        damageProperty.SetValue(variant.damage, 0.0f);
    }
}
