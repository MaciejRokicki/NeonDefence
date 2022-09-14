using UnityEngine;

public class MissileShotDamageOverTimeEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotDamageOverTimeEffectDecorator(Turret turret, GameObject missile, MissileShotEffectComponent missileComponent) : base(turret, missile, missileComponent) { }

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        enemy.ApplyEffect(new EnemyDamageOverTimeEffect(turret, enemy, turret.data.damageOverTimeDuration, turret.data.damageOverTimeCooldown, turret.data.damageOverTime));
    }
}