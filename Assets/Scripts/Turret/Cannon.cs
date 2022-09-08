using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Turret turret;
    [SerializeField]
    private GameObject target;

    private float shootTimer = 0.0f;

    private int enemyLayerMask;
    [SerializeField]
    private List<GameObject> enemiesInRange;
    private float nearestDistance;

    private void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
    }

    private void Start()
    {
        enemiesInRange = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        if (target != null)
        {
            RotateToTarget();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.rotation * Vector2.up, turret.data.range + transform.localScale.x / 2, enemyLayerMask);

            if(hit && hit.transform.tag == "Enemy")
            {
                Shoot();
            }
        }
        else
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        nearestDistance = turret.data.range;
        GameObject targetTmp = null;

        foreach(GameObject enemy in enemiesInRange)
        {
            if (!enemy)
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance < nearestDistance)
            {
                targetTmp = enemy;
                nearestDistance = distance;
            }
        }

        target = targetTmp;
    }

    private void RotateToTarget()
    {
        Vector2 dir = target.transform.position - turret.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f;

        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Time.deltaTime * turret.data.rotationSpeed);
        transform.rotation = rotation;
    }

    private void Shoot()
    {
        shootTimer += Time.deltaTime;

        if(shootTimer > 1.0f / turret.data.missilesPerSecond)
        {
            Instantiate(turret.missilePrefab, transform.parent.position, transform.rotation, transform.parent);

            shootTimer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if(target == collision.gameObject)
            {
                target = null;
            }

            enemiesInRange.Remove(collision.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(target != null)
        {
            Debug.DrawRay(transform.position, transform.rotation * Vector2.up * (turret.data.range + transform.localScale.x / 2), Color.red);
        }
    }
}
