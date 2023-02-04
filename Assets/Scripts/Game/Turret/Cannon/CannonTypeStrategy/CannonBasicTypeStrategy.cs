using UnityEngine;

public class CannonBasicTypeStrategy : CannonTypeStrategy
{
    private float shootTimer = 0.0f;

    public CannonBasicTypeStrategy(Cannon cannon, Turret turret) : base(cannon, turret) { }

    public override void Update()
    {
        if (cannon.target != null && cannon.target.activeSelf)
        {
            cannon.GetComponent<Cannon>().RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(
                cannon.transform.position, 
                cannon.transform.rotation * Vector2.up, 
                turret.Range + cannon.transform.localScale.x / 2,
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

        if (shootTimer > 1.0f / turret.MissilesPerSecond)
        {
            cannon.missilePool
                .Get()
                .SetTurret(turret)
                .SetTarget(cannon.target)
                .PrepareMissile();

            shootTimer = 0.0f;
        }
    }
}