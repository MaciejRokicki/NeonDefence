using UnityEngine;

public abstract class MissileShotEffectBaseDecorator : MissileShotEffectComponent
{
    protected readonly Turret turret;
    protected readonly GameObject missile;
    protected readonly MissileShotEffectComponent missileComponent;

    public MissileShotEffectBaseDecorator(Turret turret, GameObject missile, MissileShotEffectComponent missileComponent)
    {
        this.turret = turret;
        this.missile = missile;
        this.missileComponent = missileComponent;
    }

    public override void OnHitEffect(Enemy enemy)
    {
        if(missileComponent != null)
        {
            missileComponent.OnHitEffect(enemy);
        }
    }
}
