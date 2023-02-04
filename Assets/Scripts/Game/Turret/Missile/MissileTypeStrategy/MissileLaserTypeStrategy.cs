using UnityEngine;

public class MissileLaserTypeStrategy : MissileTypeStrategy
{
    private float timer = 0.0f;

    private BoxCollider2D collider;

    public MissileLaserTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent) : base(baseGameObject, turret, enemyHitEffectComponent) { }

    public override void Start()
    {
        collider = baseGameObject.GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = turret.variant.MissileSprite;
        spriteRenderer.material = turret.variant.MissileMaterial;
        spriteRenderer.size = Vector2.zero;
        collider.offset = turret.variant.MissileColliderOffset;
        collider.size = turret.variant.MissileColliderSize;
        spriteRenderer.sortingOrder = 4;
    }

    public override void OnEnemyTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;

        if (timer > 1.0f / turret.LaserHitsPerSecond)
        {
            enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
            collision.GetComponent<Enemy>().TakeDamage(turret.Damage, turret);

            timer = 0.0f;
        }
    }

    public override void OnEnemyTriggerExit2D(Collider2D collision)
    {
        timer = 0.0f;
    }
}
