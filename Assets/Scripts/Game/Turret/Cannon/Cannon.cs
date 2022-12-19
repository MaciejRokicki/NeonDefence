using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D cannonCollider;

    private Turret turret;
    #nullable enable
    private GameObject? laser;
    #nullable disable
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
        spriteRenderer.sprite = turret.variant.cannonSprite;
        spriteRenderer.material = turret.variant.cannonMaterial;

        cannonCollider.radius = turret.range + 0.5f;

        if(!turret.variant.laser)
        {
            cannonTypeStrategy = new CannonBasicTypeStrategy(this, turret);
        }
        else
        {
            laser = Instantiate(turret.variant.missilePrefab, transform.parent.position, transform.rotation, transform);
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
        nearestDistance = turret.range;
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

        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Time.deltaTime * turret.rotationSpeed);
        transform.rotation = rotation;
    }

    public void SetTurret(Turret turret)
    {
        this.turret = turret;
    }

    public void UpdateLaserMissileEffects()
    {
        if(turret.variant.laser && laser)
        {
            laser.GetComponent<Missile>().PrepareMissile();
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
            Debug.DrawRay(transform.position, transform.rotation * Vector2.up * (turret.range + transform.localScale.x / 2), Color.red);
        }
    }
}
