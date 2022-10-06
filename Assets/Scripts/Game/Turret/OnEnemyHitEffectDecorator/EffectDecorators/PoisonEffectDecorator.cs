using UnityEngine;

public class PoisonEffectDecorator : EnemyHitEffectBaseDecorator
{
    private readonly float effectDuration;
    private readonly float effectCooldown;
    private readonly float effectEffectiveness;

    public PoisonEffectDecorator(
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
        enemy.enemyEffectHandler.ApplyEffect(new PoisonEffect(turret, enemy, effectDuration, effectCooldown, effectEffectiveness));
    }

    public override void OnEnemyExit(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyExit(enemy);
        enemy.enemyEffectHandler.RemoveEffects(turret);
    }
}