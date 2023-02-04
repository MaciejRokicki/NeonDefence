using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Cannon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D cannonCollider;
    [SerializeField]
    private Transform shootTransformPosition;

    private Turret turret;
    #nullable enable
    private GameObject? laser;
    #nullable disable
    [HideInInspector]
    public GameObject target;

    private CannonTypeStrategy cannonTypeStrategy;

    public IObjectPool<Missile> missilePool;

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

        missilePool = new ObjectPool<Missile>(CreatePooledMissile, OnTakeFromPool, OnReturnedToPool);
    }

    private void Start()
    {
        spriteRenderer.sprite = turret.variant.CannonSprite;
        spriteRenderer.material = turret.variant.CannonMaterial;

        cannonCollider.radius = turret.Range + 0.5f;

        if(!turret.variant.Laser)
        {
            cannonTypeStrategy = new CannonBasicTypeStrategy(this, turret);
        }
        else
        {
            laser = Instantiate(turret.variant.MissilePrefab, transform.parent.position, transform.rotation, transform);
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
        nearestDistance = turret.Range;
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

        Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Time.deltaTime * turret.RotationSpeed);
        transform.rotation = rotation;
    }

    public void SetTurret(Turret turret)
    {
        this.turret = turret;
    }

    public void UpdateCannon()
    {
        if(turret.variant.Laser && laser)
        {
            laser.GetComponent<Missile>().PrepareMissile();
        }

        cannonCollider.radius = turret.Range + 0.5f;
    }

    private Missile CreatePooledMissile()
    {
        Missile missile = Instantiate(turret.variant.MissilePrefab, shootTransformPosition.position, transform.rotation, transform.parent).GetComponent<Missile>();

        return missile;
    }

    private void OnTakeFromPool(Missile missile)
    {
        missile.transform.position = shootTransformPosition.position;
        missile.transform.rotation = transform.rotation;

        missile.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Missile missile)
    {
        missile.gameObject.SetActive(false);
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
            Debug.DrawRay(transform.position, transform.rotation * Vector2.up * (turret.Range + transform.localScale.x / 2), Color.red);
        }
    }
}
