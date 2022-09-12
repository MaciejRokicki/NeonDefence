using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
    protected Turret turret;
    public GameObject target;
    protected SpriteRenderer spriteRenderer;

    private TrackingMissileStrategy trackingMissileStrategy;
    private bool destroyOnHit = true;

    protected void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        spriteRenderer.sprite = turret.data.missileSprite;
        spriteRenderer.material = turret.data.missileMaterial;
        GetComponent<BoxCollider2D>().offset = turret.data.missileColliderOffset;
        GetComponent<BoxCollider2D>().size = turret.data.missileColliderSize;
        spriteRenderer.size = turret.data.missileSpriteSize;

        if(turret.data.trackingMissile)
        {
            trackingMissileStrategy = new AutoTrackingMissileStrategy(gameObject, turret, target);
        }
        else
        {
            trackingMissileStrategy = new BasicTrackingMissileStrategy(gameObject, turret);
        }

        if(turret.data.laser)
        {
            destroyOnHit = false;
        }
    }

    private void Update()
    {
        trackingMissileStrategy.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);

            if(destroyOnHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
