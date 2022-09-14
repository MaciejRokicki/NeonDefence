public abstract class EnemyEffect
{
    protected Turret turret;
    protected Enemy enemy;

    protected EnemyEffect(Turret turret, Enemy enemy)
    {
        this.turret = turret;
        this.enemy = enemy;
    }

    public abstract void ApplyEffect();
    protected virtual void RemoveEffect() {  }
    public abstract bool CheckDuplicates(EnemyEffect enemyEffect);

    public abstract void EffectUpdate();
}