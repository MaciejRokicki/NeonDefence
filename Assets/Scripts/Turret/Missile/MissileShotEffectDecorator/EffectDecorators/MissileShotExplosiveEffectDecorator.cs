using UnityEngine;

public class MissileShotExplosiveEffectDecorator : MissileShotEffectBaseDecorator
{
    public MissileShotExplosiveEffectDecorator(Turret turret, MissileShotEffectComponent missileComponent) : base(turret, missileComponent) { }

    public override void OnHitEffect(Enemy enemy)
    {
        missileComponent.OnHitEffect(enemy);
        Debug.Log("Exolosive effect");
    }
}