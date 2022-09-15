using UnityEngine;

public class DamageOverTimeEffectDecorator : EnemyHitEffectBaseDecorator
{
    private readonly float effectDuration;
    private readonly float effectCooldown;
    private readonly float effectEffectiveness;

    public DamageOverTimeEffectDecorator(
        Turret turret,
        GameObject missile, 
        EnemyHitEffectComponent 
        enemyHitEffectComponent, 
        float effectDuration, 
        float effectCooldown, 
        float effectEffectiveness) : base(turret, missile, enemyHitEffectComponent) 
    { 
        this.effectDuration = effectDuration;
        this.effectCooldown = effectCooldown;
        this.effectEffectiveness = effectEffectiveness;
    }

    public override void OnEnemyEnter(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyEnter(enemy);
        enemy.ApplyEffect(new EnemyDamageOverTimeEffect(turret, enemy, effectDuration, effectCooldown, effectEffectiveness));
    }

    public override void OnEnemyExit(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyExit(enemy);
        enemy.RemoveEffects(turret);
    }
}