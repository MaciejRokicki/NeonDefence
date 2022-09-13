public abstract class MissileShotEffectBaseDecorator : MissileShotEffectComponent
{
    protected readonly Turret turret;
    protected readonly MissileShotEffectComponent missileComponent;

    public MissileShotEffectBaseDecorator(Turret turret, MissileShotEffectComponent missileComponent)
    {
        this.turret = turret;
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
