using UnityEngine;

public class MissileBasicTypeStrategy : MissileTypeStrategy
{
    public MissileBasicTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent) : base(baseGameObject, turret, enemyHitEffectComponent) { }

    public override void Start()
    {
        spriteRenderer.sprite = turret.data.missileSprite;
        spriteRenderer.material = turret.data.missileMaterial;
        baseGameObject.GetComponent<BoxCollider2D>().offset = turret.data.missileColliderOffset;
        baseGameObject.GetComponent<BoxCollider2D>().size = turret.data.missileColliderSize;
        spriteRenderer.size = turret.data.missileSpriteSize;
    }

    public override void OnEnemyTriggerEnter2D(Collider2D collision)
    {
        enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
        collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);
    }
}
