using UnityEngine;

public class MissileLaserTypeStrategy : MissileTypeStrategy
{
    private float timer = 0.0f;

    public MissileLaserTypeStrategy(GameObject baseGameObject, Turret turret) : base(baseGameObject, turret) { }

    public override void Start()
    {
        spriteRenderer.sprite = turret.data.missileSprite;
        spriteRenderer.material = turret.data.missileMaterial;
        baseGameObject.GetComponent<BoxCollider2D>().offset = turret.data.missileColliderOffset;
        spriteRenderer.size = baseGameObject.GetComponent<BoxCollider2D>().size = Vector2.zero;
    }

    public override void OnEnemyTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;

        if (timer > 1.0f / turret.data.laserHitsPerSecond)
        {
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);
            timer = 0.0f;
        }
    }

    public override void OnEnemyTriggerExit2D(Collider2D collision)
    {
        timer = 0.0f;
    }
}
