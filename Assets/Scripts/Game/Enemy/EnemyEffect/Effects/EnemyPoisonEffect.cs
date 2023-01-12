using UnityEngine;

public class EnemyPoisonEffect : EnemyEffect
{
    private float damageCooldownTimer = 0.0f;
    private float damageCooldown;
    private float damageOverTime;

    public EnemyPoisonEffect(Turret turret, Enemy enemy, float effectDuration, float damageCooldown, float damageOverTime) : base(turret, enemy, effectDuration)
    {
        this.damageCooldown = damageCooldown;
        this.damageOverTime = damageOverTime;
    }

    public override void OnEffectStart()
    {
        enemy.enemyEffectHandler.poisonEffect = true;
    }

    public override void OnEffectHit()
    {
        enemy.TakeDamage(damageOverTime, turret);
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