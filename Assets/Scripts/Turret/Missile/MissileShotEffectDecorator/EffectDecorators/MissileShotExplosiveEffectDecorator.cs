using UnityEngine;

public class MissileShotExplosiveEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotExplosiveEffectDecorator(Turret turret, GameObject missile, MissileShotEffectComponent missileComponent) : base(turret, missile, missileComponent) { }

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        GameObject explosion = Object.Instantiate(turret.data.explosionPrefab, enemy.transform.position, missile.transform.rotation, turret.transform) ;

        explosion.GetComponent<MissileExplosion>()
            .SetTurret(turret)
            .SetMissileShotEffectComponent(missileComponent);
    }
}