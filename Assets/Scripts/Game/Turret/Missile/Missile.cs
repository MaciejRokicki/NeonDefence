using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Missile : MonoBehaviour
{
    private Turret turret;
    public GameObject target;

    private MissileTypeStrategy missileTypeStrategy;
    private TrackingMissileStrategy trackingMissileStrategy;

    private EnemyHitEffectComponent enemyHitEffectComponent;

    private bool hittedOnce = false;

    private void Start()
    {
        PrepareMissile();
    }

    private void Update()
    {
        missileTypeStrategy.Update();
        trackingMissileStrategy.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if(turret.PenetrationMissile || turret.variant.Laser)
            {
                trackingMissileStrategy.OnTriggerEnter2D(collision);
                missileTypeStrategy.OnEnemyTriggerEnter2D(collision);
                target = null;
            }
            else
            {
                if (!hittedOnce)
                {
                    hittedOnce = true;

                    trackingMissileStrategy.OnTriggerEnter2D(collision);
                    missileTypeStrategy.OnEnemyTriggerEnter2D(collision);
                    target = null;
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

    public Missile PrepareMissile()
    {
        hittedOnce = false;

        enemyHitEffectComponent = new BasicEnemyHitEffectComponent();

        if (turret.PoisonMissile)
        {
            enemyHitEffectComponent = new PoisonEffectDecorator(
                turret,
                gameObject,
                enemyHitEffectComponent,
                turret.PoisonDuration,
                turret.PoisonHitRate,
                turret.PoisonDamage
            );
        }

        if (turret.SlowdownMissile)
        {
            enemyHitEffectComponent = new SlowdownEffectDecorator(
                turret,
                gameObject,
                enemyHitEffectComponent,
                turret.SlowdownEffectDuration,
                turret.SlowdownEffectiveness
            );
        }

        if (turret.ExplosiveMissile)
        {
            enemyHitEffectComponent = new ExplosiveEffectDecorator(turret, gameObject, enemyHitEffectComponent);
        }

        if (turret.variant.NeedTarget)
        {
            if (turret.variant.Laser)
            {
                missileTypeStrategy = new MissileLaserTypeStrategy(gameObject, turret, enemyHitEffectComponent);
            }
            else
            {
                missileTypeStrategy = new MissileBasicTypeStrategy(gameObject, turret, enemyHitEffectComponent);
            }
        }

        if (turret.TrackingMissile)
        {
            trackingMissileStrategy = new AutoTrackingMissileStrategy(gameObject, turret, target);
        }
        else
        {
            trackingMissileStrategy = new BasicTrackingMissileStrategy(gameObject, turret);
        }

        missileTypeStrategy.Start();

        return this;
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
