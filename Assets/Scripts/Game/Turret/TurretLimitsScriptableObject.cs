using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretLimitsScriptableObject", order = 6)]
public class TurretLimitsScriptableObject : ScriptableObject
{
    public FloatRangeProperty AuraDamageLimit;
    public FloatRangeProperty AuraRangeLimit;
    public FloatRangeProperty AuraSlowdownEffectivenessLimit;
    public FloatRangeProperty DamageLimit;
    public FloatRangeProperty RangeLimit;
    public FloatRangeProperty RotationSpeedLimit;
    public FloatRangeProperty MissilesPerSecondLimit;
    public FloatRangeProperty MissileSpeedLimit;
    public FloatRangeProperty LaserHitsPerSecondLimit;
    public FloatRangeProperty LaserActivationTimeLimit;
    public FloatRangeProperty LaserDeactivationTimeLimit;
    public FloatRangeProperty SlowdownEffectivenessLimit;
    public FloatRangeProperty SlowdownEffectDurationLimit;
    public FloatRangeProperty PoisonDamageLimit;
    public FloatRangeProperty PoisonHitRateLimit;
    public FloatRangeProperty PoisonDurationLimit;
    public FloatRangeProperty ExplosionDamageLimit;
    public FloatRangeProperty ExplosionRangeLimit;
}
