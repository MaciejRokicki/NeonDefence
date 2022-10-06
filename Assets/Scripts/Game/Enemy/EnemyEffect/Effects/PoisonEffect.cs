using UnityEngine;

public class PoisonEffect : EnemyEffect
{
    private float damageCooldownTimer = 0.0f;
    private float damageCooldown;
    private float damageOverTime;

    public PoisonEffect(Turret turret, Enemy enemy, float effectDuration, float damageCooldown, float damageOverTime) : base(turret, enemy, effectDuration)
    {
        this.damageCooldown = damageCooldown;
        this.damageOverTime = damageOverTime;
    }

    public override void OnEffectHit()
    {
        enemy.TakeDamage(damageOverTime);
    }

    public override void Update()
    {
        effectTimer += Time.deltaTime;
        damageCooldownTimer += Time.deltaTime;

        if (damageCooldownTimer > damageCooldown)
        {
            OnEffectHit();
            damageCooldownTimer = 0.0f;
        }

        if (effectTimer > effectDuration)
        {
            enemy.enemyEffectHandler.RemoveEffect(this);
        }
    }
}