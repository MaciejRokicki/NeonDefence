using UnityEngine;

public class LaserCannon : Cannon
{
    private GameObject laser;
    private SpriteRenderer laserSpriteRenderer;
    private float activationTimer = 0.0f;
    private float deactiveLaserTimer = 0.0f;
    private bool isLaserActive = false;

    private new void Start()
    {
        base.Start();

        laser = Instantiate(turret.data.missilePrefab, transform.parent.position, transform.rotation, transform);
        laserSpriteRenderer = laser.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation * Vector2.up, turret.data.range + transform.localScale.x / 2, enemyLayerMask);

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
                if(isLaserActive)
                {
                    DeactivateLaser();
                }
            }
        }
        else
        {
            DeactivateLaser();
            FindTarget();
        }
    }

    private void ActivateLaser()
    {
        deactiveLaserTimer = 0.0f;
        activationTimer += Time.deltaTime;

        float x = turret.data.missileSize.x * activationTimer;
        float y = turret.data.range + transform.localScale.x / 2;

        x = Mathf.Clamp(x, 0.0f, turret.data.missileSize.x);

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
        float y = turret.data.range + transform.localScale.x / 2;
        laser.transform.localPosition = new Vector2(0.0f, y / 2);
        laserSpriteRenderer.size = laser.GetComponent<BoxCollider2D>().size = new Vector2(turret.data.missileSize.x, y);
    }
}
