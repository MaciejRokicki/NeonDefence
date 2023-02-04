using UnityEngine;

public class MissileBasicTypeStrategy : MissileTypeStrategy
{
    private GameManager gameManager;

    private Vector2 mapSize;

    public MissileBasicTypeStrategy(GameObject baseGameObject, Turret turret, EnemyHitEffectComponent enemyHitEffectComponent) : base(baseGameObject, turret, enemyHitEffectComponent) { }

    public override void Start()
    {
        gameManager = GameManager.instance;

        spriteRenderer.sprite = turret.variant.MissileSprite;
        spriteRenderer.material = turret.variant.MissileMaterial;
        baseGameObject.GetComponent<BoxCollider2D>().offset = turret.variant.MissileColliderOffset;
        baseGameObject.GetComponent<BoxCollider2D>().size = turret.variant.MissileColliderSize;
        spriteRenderer.size = turret.variant.MissileSpriteSize;

        mapSize = gameManager.mapSize / 2.0f;

        mapSize.x += 1.0f;
        mapSize.y += 1.0f;
    }
    public override void Update()
    {
        Vector3 pos = baseGameObject.transform.TransformPoint(Vector3.zero);

        if (pos.x > mapSize.x ||
            pos.y > mapSize.y ||
            pos.x< -mapSize.x ||
            pos.y < -mapSize.y)
        {
            turret.Cannon.GetComponent<Cannon>().missilePool.Release(baseGameObject.GetComponent<Missile>());
        }
    }

    public override void OnEnemyTriggerEnter2D(Collider2D collision)
    {
        enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
        collision.GetComponent<Enemy>().TakeDamage(turret.Damage, turret);

        if(!turret.variant.PenetrationMissile)
        {
            turret.Cannon.GetComponent<Cannon>().missilePool.Release(baseGameObject.GetComponent<Missile>());
        }
    }
}
