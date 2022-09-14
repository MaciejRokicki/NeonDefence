using UnityEngine;

public class MissileBasicTypeStrategy : MissileTypeStrategy
{
    public MissileBasicTypeStrategy(GameObject baseGameObject, Turret turret, MissileShotEffectComponent missileComponent) : base(baseGameObject, turret, missileComponent) { }

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
        missileComponent.OnHitEffect(collision.GetComponent<Enemy>());
        collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);
    }
}
