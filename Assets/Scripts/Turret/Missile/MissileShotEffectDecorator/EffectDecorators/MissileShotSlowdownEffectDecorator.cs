public class MissileShotSlowdownEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotSlowdownEffectDecorator(Turret turret, MissileShotEffectComponent missileComponent) : base(turret, missileComponent) { }  

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        enemy.ApplyEffect(new EnemySlowdownEffect(turret, enemy, turret.data.slowdownEffectDuration, turret.data.slowdownEffectiveness));
    }
}