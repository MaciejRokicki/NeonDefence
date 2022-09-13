public abstract class EnemyEffect
{
    protected Enemy enemy;

    protected EnemyEffect(Enemy enemy)
    {
        this.enemy = enemy;
    }

    protected abstract void ApplyEffect();
    protected virtual void RemoveEffect() {  }
    public abstract bool CheckDuplicates(EnemyEffect enemyEffect);

    public abstract void EffectUpdate();
}