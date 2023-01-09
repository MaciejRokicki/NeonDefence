using UnityEngine;

public abstract class MissileTypeStrategy
{
    protected GameObject baseGameObject;
    protected Turret turret;
    protected EnemyHitEffectComponent enemyHitEffectComponent;
    protected SpriteRenderer spriteRenderer;

    public MissileTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent)
    {
        this.baseGameObject = baseGameObject;
        this.turret = turret;
        this.enemyHitEffectComponent = enemyHitEffectComponent;
        spriteRenderer = baseGameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void OnEnemyTriggerEnter2D(Collider2D collision) { }

    public virtual void OnEnemyTriggerStay2D(Collider2D collision) { }

    public virtual void OnEnemyTriggerExit2D(Collider2D collision) { }
}