using UnityEngine;

public abstract class EnemyHitEffectBaseDecorator : EnemyHitEffectComponent
{
    protected readonly Turret turret;
    protected readonly GameObject missile;
    protected readonly EnemyHitEffectComponent enemyHitEffectComponent;

    public EnemyHitEffectBaseDecorator(Turret turret, GameObject missile, EnemyHitEffectComponent enemyHitEffectComponent)
    {
        this.turret = turret;
        this.missile = missile;
        this.enemyHitEffectComponent = enemyHitEffectComponent;
    }

    public override void OnEnemyEnter(Enemy enemy)
    {
        if(enemyHitEffectComponent != null)
        {
            enemyHitEffectComponent.OnEnemyEnter(enemy);
        }
    }

    public override void OnEnemyExit(Enemy enemy) 
    {
        if(enemyHitEffectComponent != null)
        {
            enemyHitEffectComponent.OnEnemyExit(enemy);
        }
    }
}
