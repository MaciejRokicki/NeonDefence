using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
    private Turret turret;
    private GameObject target;

    private MissileTypeStrategy missileTypeStrategy;
    private TrackingMissileStrategy trackingMissileStrategy;

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

            if(!turret.data.penetrationMissile && !turret.data.laser)
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
