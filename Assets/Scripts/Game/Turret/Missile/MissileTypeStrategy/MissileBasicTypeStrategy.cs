using UnityEngine;

public class MissileBasicTypeStrategy : MissileTypeStrategy
{
    public MissileBasicTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent) : base(baseGameObject, turret, enemyHitEffectComponent) { }

    public override void Start()
    {
        spriteRenderer.sprite = turret.variant.MissileSprite;
        spriteRenderer.material = turret.variant.MissileMaterial;
        baseGameObject.GetComponent<BoxCollider2D>().offset = turret.variant.MissileColliderOffset;
        baseGameObject.GetComponent<BoxCollider2D>().size = turret.variant.MissileColliderSize;
        spriteRenderer.size = turret.variant.MissileSpriteSize;
    }

    public override void OnEnemyTriggerEnter2D(Collider2D collision)
    {
        enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
        collision.GetComponent<Enemy>().TakeDamage(turret.damage, turret);
    }
}
