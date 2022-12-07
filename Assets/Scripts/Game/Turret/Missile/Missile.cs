using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
    private Turret turret;
    private GameObject target;

    private MissileTypeStrategy missileTypeStrategy;
    private TrackingMissileStrategy trackingMissileStrategy;

    private EnemyHitEffectComponent enemyHitEffectComponent;

    private bool hittedOnce = false;

    private void Start()
    {
        enemyHitEffectComponent = new BasicEnemyHitEffectComponent();

        if (turret.poisonMissile)
        {
            enemyHitEffectComponent = new PoisonEffectDecorator(
                turret, 
                gameObject, 
                enemyHitEffectComponent, 
                turret.poisonDuration, 
                turret.poisonHitRate, 
                turret.poisonDamage
            );
        }

        if (turret.slowdownMissile)
        {
            enemyHitEffectComponent = new SlowdownEffectDecorator(
                turret, 
                gameObject, 
                enemyHitEffectComponent, 
                turret.slowdownEffectDuration, 
                turret.slowdownEffectiveness
            );
        }

        if (turret.explosiveMissile)
        {
            enemyHitEffectComponent = new ExplosiveEffectDecorator(turret, gameObject, enemyHitEffectComponent);
        }

        if (turret.variant.needTarget)
        {
            if (turret.variant.laser)
            {
                missileTypeStrategy = new MissileLaserTypeStrategy(gameObject, turret, enemyHitEffectComponent);
            }
            else
            {
                missileTypeStrategy = new MissileBasicTypeStrategy(gameObject, turret, enemyHitEffectComponent);
            }
        }

        if(turret.trackingMissile)
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
            if(turret.penetrationMissile || turret.variant.laser)
            {
                missileTypeStrategy.OnEnemyTriggerEnter2D(collision);
                target = null;
            }
            else
            {
                if (!hittedOnce)
                {
                    hittedOnce = true;

                    missileTypeStrategy.OnEnemyTriggerEnter2D(collision);
                    target = null;

                    Destroy(gameObject);
                }
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

    public GameObject GetTarget() => target;
}
