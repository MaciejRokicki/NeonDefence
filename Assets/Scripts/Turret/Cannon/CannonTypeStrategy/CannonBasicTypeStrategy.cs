using UnityEngine;

public class CannonBasicTypeStrategy : CannonTypeStrategy
{
    private float shootTimer = 0.0f;

    public CannonBasicTypeStrategy(Cannon cannon, Turret turret) : base(cannon, turret) { }

    public override void Update()
    {
        if (cannon.target != null)
        {
            cannon.GetComponent<Cannon>().RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(
                cannon.transform.position, 
                cannon.transform.rotation * Vector2.up, 
                turret.range + cannon.transform.localScale.x / 2,
                cannon.enemyLayerMask);

            if (hit && hit.transform.tag == "Enemy")
            {
                Shoot();
            }
        }
        else
        {
            cannon.FindTarget();
        }
    }

    protected override void Shoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > 1.0f / turret.missilesPerSecond)
        {
            GameObject missile = Object.Instantiate(turret.variant.missilePrefab, cannon.transform.position, cannon.transform.rotation, cannon.transform.parent);
            missile.GetComponent<Missile>()
                .SetTurret(turret)
                .SetTarget(cannon.target);

            shootTimer = 0.0f;
        }
    }
}