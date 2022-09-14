using UnityEngine;

public class MissileShotSlowdownEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotSlowdownEffectDecorator(Turret turret,  GameObject missile, MissileShotEffectComponent missileComponent) : base(turret, missile, missileComponent) { }  

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        enemy.ApplyEffect(new EnemySlowdownEffect(turret, enemy, turret.data.slowdownEffectDuration, turret.data.slowdownEffectiveness));
    }
}