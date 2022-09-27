using UnityEngine;
using UnityEngine.UIElements;

public class TurretInfoMenu : MonoBehaviour
{
    private VisualElement root;

    [SerializeField]
    private GameObject cannonRangeInfo;
    [SerializeField]
    private GameObject auraRangeInfo;

    private LineRenderer cannonLineRenderer;
    private SpriteRenderer cannonSpriteRenderer;
    private LineRenderer auraLineRenderer;
    private SpriteRenderer auraSpriteRenderer;

    private bool showExtraEffects = false;

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

    private void Awake()
    {
        cannonLineRenderer = cannonRangeInfo.GetComponent<LineRenderer>();
        cannonSpriteRenderer = cannonRangeInfo.GetComponent<SpriteRenderer>();
        auraLineRenderer = auraRangeInfo.GetComponent<LineRenderer>();
        auraSpriteRenderer = auraRangeInfo.GetComponent<SpriteRenderer>();

        root = transform.parent.GetComponent<UIDocument>().rootVisualElement;

        damageLabel = root.Q<Label>("damage-label");
        rangeLabel = root.Q<Label>("range-label");
        rotationSpeedLabel = root.Q<Label>("rotation-speed-label");
        missilesPerSecondLabel = root.Q<Label>("missiles-per-second-label");
        missileSpeedLabel = root.Q<Label>("missile-speed-label");

        laserHitsPerSecondLabel = root.Q<Label>("laser-hits-per-second-label");
        laserActivationTimeLabel = root.Q<Label>("laser-activation-time-label");
        laserDeactivationTimeLabel = root.Q<Label>("laser-deactivation-time-label");

        slowdownEffectivenessLabel = root.Q<Label>("slowdown-effectiveness-label");
        slowdownEffectDurationLabel = root.Q<Label>("slowdown-effect-duration-label");

        damageOverTimeLabel = root.Q<Label>("damage-over-time-label");
        damageOverTimeCooldownLabel = root.Q<Label>("damage-over-time-hit-cooldown-label");
        damageOverTimeDurationLabel = root.Q<Label>("damage-over-time-duration-label");

        explosionDamageLabel = root.Q<Label>("explosion-damage-label");
        explosionRangeLabel = root.Q<Label>("explosion-range-label");

        auraDamageLabel = root.Q<Label>("aura-damage-label");
        auraRangeLabel = root.Q<Label>("aura-range-label");
        auraSlowdownEffectivenessLabel = root.Q<Label>("aura-slowdown-effectiveness-label");
    }

    public void Show(Turret turret)
    {
        cannonRangeInfo.SetActive(true);
        auraRangeInfo.SetActive(true);

        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;
        cannonSpriteRenderer.size = auraSpriteRenderer.size = Vector2.zero;

        transform.position = turret.transform.position;

        if (turret.variant.needTarget)
        {
            root.Q<VisualElement>("cannon-section").style.display = DisplayStyle.Flex;

            root.Q<VisualElement>("laser-info").style.display = turret.variant.laser ? DisplayStyle.Flex : DisplayStyle.None;
            missilesPerSecondLabel.style.display = missileSpeedLabel.style.display = turret.variant.laser ? DisplayStyle.None : DisplayStyle.Flex;

            root.Q<VisualElement>("slowdown-effect-info").style.display = turret.variant.slowdownOnMissileHit ? DisplayStyle.Flex : DisplayStyle.None;
            root.Q<VisualElement>("damage-over-time-effect-info").style.display = turret.variant.dealDamageOverTime ? DisplayStyle.Flex : DisplayStyle.None;
            root.Q<VisualElement>("explosion-effect-info").style.display = turret.variant.explosiveMissile ? DisplayStyle.Flex : DisplayStyle.None;

            showExtraEffects = false;

            if(turret.variant.penetrationMissile)
            {
                root.Q<VisualElement>("penetration-missile-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                root.Q<VisualElement>("penetration-missile-label").style.display = DisplayStyle.None;
            }

            if(turret.variant.trackingMissile)
            {
                root.Q<VisualElement>("tracking-missile-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                root.Q<VisualElement>("tracking-missile-label").style.display = DisplayStyle.None;
            }

            if(turret.variant.copyMissileEffects)
            {
                root.Q<VisualElement>("copy-missile-effects-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                root.Q<VisualElement>("copy-missile-effects-label").style.display = DisplayStyle.None;
            }

            root.Q<VisualElement>("extra-effects-info").style.display = showExtraEffects ? DisplayStyle.Flex : DisplayStyle.None;

            root.Q<VisualElement>("aura-section").style.display = DisplayStyle.None;

            DrawCircle(cannonLineRenderer, turret.transform.position, turret.range + 0.5f);
            cannonSpriteRenderer.size = new Vector2(turret.range + 0.5f, turret.range + 0.5f) * 2;
        }

        if(turret.variant.aura)
        {
            root.Q<VisualElement>("cannon-section").style.display = DisplayStyle.None;

            root.Q<VisualElement>("aura-section").style.display = DisplayStyle.Flex;
            root.Q<VisualElement>("aura-slowdown-effect-info").style.display = turret.variant.auraSlowdown ? DisplayStyle.Flex : DisplayStyle.None;

            DrawCircle(auraLineRenderer, turret.transform.position, turret.auraRange / 2);
            auraSpriteRenderer.size = new Vector2(turret.auraRange, turret.auraRange);
        }

        damageLabel.text = $"Damage: {turret.damage}";
        rangeLabel.text = $"Range: {turret.range}";
        rotationSpeedLabel.text = $"Rotation speed: {turret.rotationSpeed}";
        missilesPerSecondLabel.text = $"Missiles per second: {turret.missilesPerSecond}";
        missileSpeedLabel.text = $"Missile speed: {turret.missileSpeed}";

        laserHitsPerSecondLabel.text = $"Laser hits per second: {turret.laserHitsPerSecond}";
        laserActivationTimeLabel.text = $"Laser activation time: {turret.laserActivationTime}s";
        laserDeactivationTimeLabel.text = $"Laser deactivation time: {turret.laserDeactivationTime}s";

        slowdownEffectDurationLabel.text = $"Slowdown effect duration: {turret.slowdownEffectDuration}s";
        slowdownEffectivenessLabel.text = $"Slowdown effectiveness: {turret.slowdownEffectDuration * 100.0f}%";

        damageOverTimeLabel.text = $"Damage over time: {turret.damageOverTime}";
        damageOverTimeCooldownLabel.text = $"Damage over time hit cooldown: {turret.damageOverTimeHitCooldown}s";
        damageOverTimeDurationLabel.text = $"Damage over time duration: {turret.damageOverTimeDuration}s";

        explosionDamageLabel.text = $"Explosion damage: {turret.explosionDamage}";
        explosionRangeLabel.text = $"Explosion range: {turret.explosionRange}";

        auraDamageLabel.text = $"Aura damage: {turret.auraDamage}";
        auraRangeLabel.text = $"Aura range: {(turret.auraRange - 1) / 2}";

        auraSlowdownEffectivenessLabel.text = $"Aura slowdown effectiveness: {turret.auraSlowdownEffectiveness * 100.0f}%";

        root.Q<VisualElement>("TurretInfo").style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.Q<VisualElement>("TurretInfo").style.display = DisplayStyle.None;

        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;
        cannonSpriteRenderer.size = auraSpriteRenderer.size = Vector2.zero;

        cannonRangeInfo.SetActive(false);
        auraRangeInfo.SetActive(false);
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
