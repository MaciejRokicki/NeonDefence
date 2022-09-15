using UnityEngine;

public class ExplosiveEffectDecorator : EnemyHitEffectBaseDecorator
{
    public ExplosiveEffectDecorator(
        Turret turret, 
        GameObject missile, 
        EnemyHitEffectComponent enemyHitEffectComponent) : base(turret, missile, enemyHitEffectComponent) { }

    public override void OnEnemyEnter(Enemy enemy)
    {
        enemyHitEffectComponent.OnEnemyEnter(enemy);
        GameObject explosion = Object.Instantiate(turret.data.explosionPrefab, enemy.transform.position, missile.transform.rotation, turret.transform) ;

        explosion.GetComponent<MissileExplosion>()
            .SetTurret(turret)
            .SetMissileShotEffectComponent(enemyHitEffectComponent);
    }
}