using UnityEngine;
using UnityEngine.UIElements;

public class TurretInfoUI : MonoBehaviour
{
    [SerializeField]
    private LineRenderer cannonLineRenderer;
    [SerializeField]
    private LineRenderer auraLineRenderer;

    private Label damageLabel;
    private Label damageOverTimeDurationLabel;
    private Label damageOverTimeCooldownLabel;
    private Label damageOverTimeLabel;
    private Label missilesPerSecondLabel;
    private Label missileSpeedLabel;
    private Label laserHitsPerSecondLabel;
    private Label rangeLabel;
    private Label explosionDamageLabel;
    private Label explosionRangeLabel;
    private Label rotationSpeedLabel;
    private Label laserActivationTimeLabel;
    private Label laserDeactivationTimeLabel;
    private Label slowdownEffectDurationLabel;
    private Label slowdownEffectivenessLabel;
    private Label auraDamageLabel;
    private Label auraRangeLabel;
    private Label auraSlowdownEffectivenessLabel;

    private void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        damageLabel = rootVisualElement.Q<Label>("damage-label");
        damageOverTimeDurationLabel = rootVisualElement.Q<Label>("damage-over-time-duration-label");
        damageOverTimeCooldownLabel = rootVisualElement.Q<Label>("damage-over-time-cooldown-label");
        damageOverTimeLabel = rootVisualElement.Q<Label>("damage-over-time-label");
        missilesPerSecondLabel = rootVisualElement.Q<Label>("missiles-per-second-label");
        missileSpeedLabel = rootVisualElement.Q<Label>("missile-speed-label");
        laserHitsPerSecondLabel = rootVisualElement.Q<Label>("laser-hits-per-second-label");
        rangeLabel = rootVisualElement.Q<Label>("range-label");
        explosionDamageLabel = rootVisualElement.Q<Label>("explosion-damage-label");
        explosionRangeLabel = rootVisualElement.Q<Label>("explosion-range-label");
        rotationSpeedLabel = rootVisualElement.Q<Label>("rotation-speed-label");
        laserActivationTimeLabel = rootVisualElement.Q<Label>("laser-activation-time-label");
        laserDeactivationTimeLabel = rootVisualElement.Q<Label>("laser-deactivation-time-label");
        slowdownEffectDurationLabel = rootVisualElement.Q<Label>("slowdown-effect-duration-label");
        slowdownEffectivenessLabel = rootVisualElement.Q<Label>("slowdown-effectiveness-label");
        auraDamageLabel = rootVisualElement.Q<Label>("aura-damage-label");
        auraRangeLabel = rootVisualElement.Q<Label>("aura-range-label");
        auraSlowdownEffectivenessLabel = rootVisualElement.Q<Label>("aura-slowdown-effectiveness-label");
    }

    public void Show(Turret turret)
    {
        gameObject.SetActive(true);

        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;

        if (turret.variant.needTarget)
        {
            DrawCircle(cannonLineRenderer, turret.transform.position, turret.range + 0.5f);
        }

        if(turret.variant.aura)
        {
            DrawCircle(auraLineRenderer, turret.transform.position, turret.auraRange / 2);
        }
        
        damageLabel.text = $"Damage: {turret.damage}";
        damageOverTimeDurationLabel.text = $"Damage over time duration: {turret.damageOverTimeDuration}";
        damageOverTimeCooldownLabel.text = $"Damage over time cooldown: {turret.damageOverTimeCooldown}";
        damageOverTimeLabel.text = $"Damage over time: {turret.damageOverTime}";
        missilesPerSecondLabel.text = $"Missiles per second: {turret.missilesPerSecond}";
        missileSpeedLabel.text = $"Missile speed: {turret.missileSpeed}";
        laserHitsPerSecondLabel.text = $"Laser hits per second: {turret.laserHitsPerSecond}";
        rangeLabel.text = $"Range: {turret.range}";
        explosionDamageLabel.text = $"Explosion damage: {turret.explosionDamage}";
        explosionRangeLabel.text = $"Explosion range: {turret.explosionRange}";
        rotationSpeedLabel.text = $"Rotation speed: {turret.rotationSpeed}";
        laserActivationTimeLabel.text = $"Laser activation time: {turret.laserActivationTime}";
        laserDeactivationTimeLabel.text = $"Laser deactivation time: {turret.laserDeactivationTime}";
        slowdownEffectDurationLabel.text = $"Slowdown effect duration: {turret.slowdownEffectDuration}";
        slowdownEffectivenessLabel.text = $"Slowdown effectiveness: {turret.slowdownEffectDuration}";
        auraDamageLabel.text = $"Aura damage: {turret.auraDamage}";
        auraRangeLabel.text = $"Aura range: {(turret.auraRange - 1) / 2}";
        auraSlowdownEffectivenessLabel.text = $"Aura slowdown effectiveness: {turret.auraSlowdownEffectiveness}";
    }

    public void Hide()
    {
        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;
        gameObject.SetActive(false);
    }

    private void DrawCircle(LineRenderer lineRenderer, Vector3 origin, float radius)
    {
        int iterations = 100;
        lineRenderer.positionCount = iterations;

        for(int i = 0; i < iterations; i++)
        {
            float progress = (float)i / (iterations-2);
            float rad = progress * 2.0f * Mathf.PI;

            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            Vector3 pos = origin + new Vector3(x, y, 0.0f);

            lineRenderer.SetPosition(i, pos);
        }
    }
}
