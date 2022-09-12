public abstract class CannonTypeStrategy
{
    protected Cannon cannon;
    protected Turret turret;

    public CannonTypeStrategy(Cannon cannon, Turret turret)
    {
        this.cannon = cannon;
        this.turret = turret;
    }

    public virtual void Start() { }

    public virtual void Update() { }

    protected abstract void Shoot();
}