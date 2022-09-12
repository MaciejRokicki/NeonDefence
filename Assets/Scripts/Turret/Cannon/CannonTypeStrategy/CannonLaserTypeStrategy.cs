using UnityEngine;

public class CannonLaserTypeStrategy : CannonTypeStrategy
{
    private GameObject laser;
    private SpriteRenderer laserSpriteRenderer;

    private bool isLaserActive = false;

    private float activationTimer = 0.0f;
    private float deactiveLaserTimer = 0.0f;

    public CannonLaserTypeStrategy(Cannon cannon, Turret turret, GameObject laser) : base(cannon, turret) 
    {
        this.laser = laser;
    }

    public override void Start()
    {
        laserSpriteRenderer = laser.GetComponent<SpriteRenderer>();
        laser.GetComponent<Missile>().SetTurret(turret);
    }

    public override void Update()
    {
        if (cannon.target != null)
        {
            cannon.GetComponent<Cannon>().RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(
                cannon.transform.position, 
                cannon.transform.rotation * Vector2.up, 
                turret.data.range + cannon.transform.localScale.x / 2, 
                cannon.enemyLayerMask);

            if (hit && hit.transform.tag == "Enemy")
            {
                if (!isLaserActive)
                {
                    ActivateLaser();
                }
                else
                {
                    Shoot();
                }
            }
            else
            {
                if (isLaserActive)
                {
                    DeactivateLaser();
                }
            }
        }
        else
        {
            DeactivateLaser();
            cannon.FindTarget();
        }
    }

    private void ActivateLaser()
    {
        deactiveLaserTimer = 0.0f;
        activationTimer += Time.deltaTime;

        float x = turret.data.missileSpriteSize.x * activationTimer;
        float y = turret.data.range + cannon.transform.localScale.x / 2;

        x = Mathf.Clamp(x, 0.0f, turret.data.missileSpriteSize.x);

        laser.transform.localPosition = new Vector2(0.0f, y / 2);
        laserSpriteRenderer.size = new Vector2(x, y);

        if (activationTimer > turret.data.laserActivationTime)
        {
            isLaserActive = true;

            activationTimer = 0.0f;
        }
    }

    private void DeactivateLaser()
    {
        deactiveLaserTimer += Time.deltaTime;

        if (deactiveLaserTimer > turret.data.timeToDeactiveLaser)
        {
            isLaserActive = false;
            laserSpriteRenderer.size = laser.GetComponent<BoxCollider2D>().size = Vector2.zero;

            deactiveLaserTimer = 0.0f;
        }
    }

    protected override void Shoot()
    {
        float y = turret.data.range + cannon.transform.localScale.x / 2;
        laser.transform.localPosition = new Vector2(0.0f, y / 2);
        laserSpriteRenderer.size = laser.GetComponent<BoxCollider2D>().size = new Vector2(turret.data.missileSpriteSize.x, y);
    }
}