using UnityEngine;

public class MissileBasicTypeStrategy : MissileTypeStrategy
{
    public MissileBasicTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent) : base(baseGameObject, turret, enemyHitEffectComponent) { }

    public override void Start()
    {
        spriteRenderer.sprite = turret.variant.missileSprite;
        spriteRenderer.material = turret.variant.missileMaterial;
        baseGameObject.GetComponent<BoxCollider2D>().offset = turret.variant.missileColliderOffset;
        baseGameObject.GetComponent<BoxCollider2D>().size = turret.variant.missileColliderSize;
        spriteRenderer.size = turret.variant.missileSpriteSize;
    }

    public override void OnEnemyTriggerEnter2D(Collider2D collision)
    {
        enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
        collision.GetComponent<Enemy>().TakeDamage(turret.damage);
    }
}
