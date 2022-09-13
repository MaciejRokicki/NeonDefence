using UnityEngine;

public class EnemyDamageOverTimeEffect : EnemyEffect
{
    private float effectTimer = 0.0f;
    private float damageCooldownTimer = 0.0f;
    private float damageCooldown;
    private float effectDuration;
    private float damageOverTime;

    public EnemyDamageOverTimeEffect(Enemy enemy, float effectDuration, float damageCooldown, float damageOverTime) : base(enemy)
    {
        this.effectDuration = effectDuration;
        this.damageCooldown = damageCooldown;
        this.damageOverTime = damageOverTime;

        ApplyEffect();
    }

    protected override void ApplyEffect()
    {
        enemy.TakeDamage(damageOverTime);
    }

    public override bool CheckDuplicates(EnemyEffect enemyEffect)
    {
        EnemyDamageOverTimeEffect enemySlowdownEffect = enemyEffect as EnemyDamageOverTimeEffect;

        if (enemySlowdownEffect != null)
        {
            return true;
        }

        return false;
    }

    public override void EffectUpdate()
    {
        effectTimer += Time.deltaTime;
        damageCooldownTimer += Time.deltaTime;

        if(damageCooldownTimer > damageCooldown)
        {
            ApplyEffect();
            damageCooldownTimer = 0.0f;
        }

        if (effectTimer > effectDuration)
        {
            enemy.RemoveEffect(this);
        }
    }
}