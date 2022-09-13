using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
    private Turret turret;
    private GameObject target;

    private MissileTypeStrategy missileTypeStrategy;
    private TrackingMissileStrategy trackingMissileStrategy;

    private MissileShotEffectComponent missileComponent;

    private void Start()
    {
        if(turret.data.needTarget)
        {
            missileTypeStrategy = new MissileBasicTypeStrategy(gameObject, turret);
        }
        else if(turret.data.laser)
        {
            missileTypeStrategy = new MissileLaserTypeStrategy(gameObject, turret);
        }

        if(turret.data.trackingMissile)
        {
            trackingMissileStrategy = new AutoTrackingMissileStrategy(gameObject, turret, target);
        }
        else
        {
            trackingMissileStrategy = new BasicTrackingMissileStrategy(gameObject, turret);
        }

        missileComponent = new BasicMissileShotEffectComponent();

        if(turret.data.dealDamageOverTime)
        {
            missileComponent = new MissileShotDamageOverTimeEffectDecorator(turret, missileComponent);
        }

        if(turret.data.explosiveMissile)
        {
            missileComponent = new MissileShotExplosiveEffectDecorator(turret, missileComponent);
        }

        if(turret.data.slowdownOnMissileHit)
        {
            missileComponent = new MissileShotSlowdownEffectDecorator(turret, missileComponent);
        }

        missileTypeStrategy.Start();
    }

    private void Update()
    {
        trackingMissileStrategy.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            missileTypeStrategy.OnEnemyTriggerEnter2D(collision);
            target = null;

            missileComponent.OnHitEffect(collision.GetComponent<Enemy>());

            if (!turret.data.penetrationMissile && !turret.data.laser)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            missileTypeStrategy.OnEnemyTriggerStay2D(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            missileTypeStrategy.OnEnemyTriggerExit2D(collision);
        }
    }

    public Missile SetTurret(Turret turret)
    {
        this.turret = turret;

        return this;
    }

    public Missile SetTarget(GameObject target)
    {
        this.target = target;

        return this;
    }
}
