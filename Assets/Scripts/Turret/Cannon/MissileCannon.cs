using UnityEngine;

public class MissileCannon : Cannon
{
    private float shootTimer = 0.0f;

    private void Update()
    {
        if (target != null)
        {
            RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation * Vector2.up, turret.data.range + transform.localScale.x / 2, enemyLayerMask);

            if (hit && hit.transform.tag == "Enemy")
            {
                Shoot();
            }
        }
        else
        {
            FindTarget();
        }
    }

    protected override void Shoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > 1.0f / turret.data.missilesPerSecond)
        {
            GameObject missile = Instantiate(turret.data.missilePrefab, transform.parent.position, transform.rotation, transform.parent);
            missile.GetComponent<Missile>().target = target;

            shootTimer = 0.0f;
        }
    }
}
