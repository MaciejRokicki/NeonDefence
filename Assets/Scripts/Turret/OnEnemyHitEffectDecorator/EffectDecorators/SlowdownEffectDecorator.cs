using UnityEngine;

public class SlowdownEffectDecorator : EnemyHitEffectBaseDecorator
{
    private readonly float effectDuration;
    private readonly float effectEffectiveness;

    public SlowdownEffectDecorator(
        Turret turret,
        GameObject missile, 
        EnemyHitEffectComponent enemyHitEffectComponent, 
        float effectDuration, 
        float effectEffectiveness) : base(turret, missile, enemyHitEffectComponent)
    {
        this.effectDuration = effectDuration;
        this.effectEffectiveness = effectEffectiveness;
    }

    public override void OnEnemyEnter(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyEnter(enemy);
        enemy.enemyEffectHandler.ApplyEffect(new EnemySlowdownEffect(turret, enemy, effectDuration, effectEffectiveness));
    }

    public override void OnEnemyExit(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyExit(enemy);
        enemy.enemyEffectHandler.RemoveEffects(turret);
    }
}