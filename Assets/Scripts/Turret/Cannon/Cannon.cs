using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D cannonCollider;

    private Turret turret;
    [HideInInspector]
    public GameObject target;

    private CannonTypeStrategy cannonTypeStrategy;

    [HideInInspector]
    public int enemyLayerMask;
    private List<GameObject> enemiesInRange;
    private float nearestDistance;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cannonCollider = GetComponent<CircleCollider2D>();

        enemiesInRange = new List<GameObject>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void Start()
    {
        spriteRenderer.sprite = turret.data.cannonSprite;
        spriteRenderer.material = turret.data.cannonMaterial;

        cannonCollider.radius = turret.data.range + 0.5f;

        if(!turret.data.laser)
        {
            cannonTypeStrategy = new CannonBasicTypeStrategy(this, turret);
        }
        else
        {
            GameObject laser = Instantiate(turret.data.missilePrefab, transform.parent.position, transform.rotation, transform);
            cannonTypeStrategy = new CannonLaserTypeStrategy(this, turret, laser);
        }

        cannonTypeStrategy.Start();
    }

    private void Update()
    {
        cannonTypeStrategy.Update();
    }

    public void FindTarget()
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

    public void RotateToTarget()
    {
        Vector2 dir = target.transform.position - turret.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f;

        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Time.deltaTime * turret.data.rotationSpeed);
        transform.rotation = rotation;
    }

    public void SetTurret(Turret turret)
    {
        this.turret = turret;
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
