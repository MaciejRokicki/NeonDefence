using UnityEngine;

public class MissileLaserTypeStrategy : MissileTypeStrategy
{
    private float timer = 0.0f;

    private BoxCollider2D collider;

    public MissileLaserTypeStrategy(GameObject baseGameObject, Turret turret, MissileShotEffectComponent missileComponent) : base(baseGameObject, turret, missileComponent) { }

    public override void Start()
    {
        collider = baseGameObject.GetComponent<BoxCollider2D>();

        spriteRenderer.sprite = turret.data.missileSprite;
        spriteRenderer.material = turret.data.missileMaterial;
        spriteRenderer.size = Vector2.zero;
        collider.offset = turret.data.missileColliderOffset;
        collider.size = turret.data.missileColliderSize;
    }

    public override void OnEnemyTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;

        if (timer > 1.0f / turret.data.laserHitsPerSecond)
        {
            missileComponent.OnHitEffect(collision.GetComponent<Enemy>());
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);

            timer = 0.0f;
        }
    }

    public override void OnEnemyTriggerExit2D(Collider2D collision)
    {
        timer = 0.0f;
    }
}
