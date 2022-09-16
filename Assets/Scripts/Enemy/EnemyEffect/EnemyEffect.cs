public abstract class EnemyEffect
{
    public Turret turret;
    protected Enemy enemy;

    protected EnemyEffect(Turret turret, Enemy enemy)
    {
        this.turret = turret;
        this.enemy = enemy;
    }

    public abstract void ApplyEffect();
    public virtual void RemoveEffect() { }
    public abstract void EffectUpdate();
    public abstract bool CheckDuplicates(EnemyEffect enemyEffect);
}