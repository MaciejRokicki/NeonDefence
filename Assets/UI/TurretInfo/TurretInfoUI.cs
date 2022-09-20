using UnityEngine;
using UnityEngine.UIElements;

public class TurretInfoUI : MonoBehaviour
{
    private VisualElement rootVisualElement;

    [SerializeField]
    private GameObject cannonRangeInfo;
    [SerializeField]
    private GameObject auraRangeInfo;

    private LineRenderer cannonLineRenderer;
    private SpriteRenderer cannonSriteRenderer;
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

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        damageLabel = rootVisualElement.Q<Label>("damage-label");
        rangeLabel = rootVisualElement.Q<Label>("range-label");
        rotationSpeedLabel = rootVisualElement.Q<Label>("rotation-speed-label");
        missilesPerSecondLabel = rootVisualElement.Q<Label>("missiles-per-second-label");
        missileSpeedLabel = rootVisualElement.Q<Label>("missile-speed-label");

        laserHitsPerSecondLabel = rootVisualElement.Q<Label>("laser-hits-per-second-label");
        laserActivationTimeLabel = rootVisualElement.Q<Label>("laser-activation-time-label");
        laserDeactivationTimeLabel = rootVisualElement.Q<Label>("laser-deactivation-time-label");

        slowdownEffectivenessLabel = rootVisualElement.Q<Label>("slowdown-effectiveness-label");
        slowdownEffectDurationLabel = rootVisualElement.Q<Label>("slowdown-effect-duration-label");

        damageOverTimeLabel = rootVisualElement.Q<Label>("damage-over-time-label");
        damageOverTimeCooldownLabel = rootVisualElement.Q<Label>("damage-over-time-hit-cooldown-label");
        damageOverTimeDurationLabel = rootVisualElement.Q<Label>("damage-over-time-duration-label");

        explosionDamageLabel = rootVisualElement.Q<Label>("explosion-damage-label");
        explosionRangeLabel = rootVisualElement.Q<Label>("explosion-range-label");

        auraDamageLabel = rootVisualElement.Q<Label>("aura-damage-label");
        auraRangeLabel = rootVisualElement.Q<Label>("aura-range-label");
        auraSlowdownEffectivenessLabel = rootVisualElement.Q<Label>("aura-slowdown-effectiveness-label");
    }

    public void Show(Turret turret)
    {
        gameObject.SetActive(true);

        if(Camera.main.transform.position.x < 0)
        {
            Debug.Log("TEST");
            rootVisualElement.style.right = new StyleLength(new Length(-100, LengthUnit.Percent));
        }

        cannonLineRenderer = cannonRangeInfo.GetComponent<LineRenderer>();
        cannonSriteRenderer = cannonRangeInfo.GetComponent<SpriteRenderer>();
        auraLineRenderer = auraRangeInfo.GetComponent<LineRenderer>();
        auraSpriteRenderer = auraRangeInfo.GetComponent<SpriteRenderer>();

        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;
        cannonSriteRenderer.size = auraSpriteRenderer.size = Vector2.zero;

        transform.position = turret.transform.position;

        if (turret.variant.needTarget)
        {
            rootVisualElement.Q<VisualElement>("cannon-section").style.display = DisplayStyle.Flex;

            rootVisualElement.Q<VisualElement>("laser-info").style.display = turret.variant.laser ? DisplayStyle.Flex : DisplayStyle.None;
            missilesPerSecondLabel.style.display = missileSpeedLabel.style.display = turret.variant.laser ? DisplayStyle.None : DisplayStyle.Flex;

            rootVisualElement.Q<VisualElement>("slowdown-effect-info").style.display = turret.variant.slowdownOnMissileHit ? DisplayStyle.Flex : DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("damage-over-time-effect-info").style.display = turret.variant.dealDamageOverTime ? DisplayStyle.Flex : DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("explosion-effect-info").style.display = turret.variant.explosiveMissile ? DisplayStyle.Flex : DisplayStyle.None;

            showExtraEffects = false;

            if(turret.variant.penetrationMissile)
            {
                rootVisualElement.Q<VisualElement>("penetration-missile-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                rootVisualElement.Q<VisualElement>("penetration-missile-label").style.display = DisplayStyle.None;
            }

            if(turret.variant.trackingMissile)
            {
                rootVisualElement.Q<VisualElement>("tracking-missile-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                rootVisualElement.Q<VisualElement>("tracking-missile-label").style.display = DisplayStyle.None;
            }

            if(turret.variant.copyMissileEffects)
            {
                rootVisualElement.Q<VisualElement>("copy-missile-effects-label").style.display = DisplayStyle.Flex;
                showExtraEffects = true;
            }
            else
            {
                rootVisualElement.Q<VisualElement>("copy-missile-effects-label").style.display = DisplayStyle.None;
            }

            rootVisualElement.Q<VisualElement>("extra-effects-info").style.display = showExtraEffects ? DisplayStyle.Flex : DisplayStyle.None;

            rootVisualElement.Q<VisualElement>("aura-section").style.display = DisplayStyle.None;

            DrawCircle(cannonLineRenderer, turret.transform.position, turret.range + 0.5f);
            cannonSriteRenderer.size = new Vector2(turret.range + 0.5f, turret.range + 0.5f) * 2;
        }

        if(turret.variant.aura)
        {
            rootVisualElement.Q<VisualElement>("cannon-section").style.display = DisplayStyle.None;

            rootVisualElement.Q<VisualElement>("aura-section").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("aura-slowdown-effect-info").style.display = turret.variant.auraSlowdown ? DisplayStyle.Flex : DisplayStyle.None;

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
    }

    public void Hide()
    {
        cannonLineRenderer.positionCount = auraLineRenderer.positionCount = 0;
        cannonSriteRenderer.size = auraSpriteRenderer.size = Vector2.zero;
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
