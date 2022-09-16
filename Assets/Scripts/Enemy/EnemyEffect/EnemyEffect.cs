public abstract class EnemyEffect
{
    public Turret turret;
    protected Enemy enemy;
    protected float effectTimer = 0.0f;
    protected float effectDuration;

    protected EnemyEffect(Turret turret, Enemy enemy, float effectDuration)
    {
        this.turret = turret;
        this.enemy = enemy;
        this.effectDuration = effectDuration;
    }

    public virtual void OnEffectStart() { }
    public virtual void OnEffectHit() { }
    public virtual void OnEffectEnd() { }
    public abstract void Update();
    public bool CheckDuplicates<T>(T enemyEffect) where T : EnemyEffect
    {
        if(GetType() != enemyEffect.GetType() || enemyEffect.turret.data != turret.data)
            return false;

        effectTimer = 0.0f;

        return true;
    }
}