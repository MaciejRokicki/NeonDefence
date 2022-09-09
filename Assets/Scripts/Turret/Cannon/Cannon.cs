using System.Collections.Generic;
using UnityEngine;

public abstract class Cannon : MonoBehaviour
{
    [SerializeField]
    protected Turret turret;
    [SerializeField]
    protected GameObject target;

    protected int enemyLayerMask;
    [SerializeField]
    private List<GameObject> enemiesInRange;
    private float nearestDistance;

    protected void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
    }

    protected void Start()
    {
        enemiesInRange = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    protected void FindTarget()
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

    protected void RotateToTarget()
    {
        Vector2 dir = target.transform.position - turret.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f;

        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Time.deltaTime * turret.data.rotationSpeed);
        transform.rotation = rotation;
    }

    protected abstract void Shoot();

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
