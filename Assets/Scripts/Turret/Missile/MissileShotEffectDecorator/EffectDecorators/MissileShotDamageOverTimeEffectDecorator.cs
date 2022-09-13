public class MissileShotDamageOverTimeEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotDamageOverTimeEffectDecorator(Turret turret, MissileShotEffectComponent missileComponent) : base(turret, missileComponent) { }

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        enemy.ApplyEffect(new EnemyDamageOverTimeEffect(enemy, turret.data.damageOverTimeDuration, turret.data.damageOverTimeCooldown, turret.data.damageOverTime));
    }
}