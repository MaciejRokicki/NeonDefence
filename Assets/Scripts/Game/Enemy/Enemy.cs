using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(EnemyEffectHandler))]
public class Enemy : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    private StatisticsManager statisticsManager;
    private GameManager gameManager;

    [SerializeField]
    public EnemyScriptableObject variant;
    private Rigidbody2D rb;

    [SerializeField]
    private float health;
    [SerializeField]
    public float movementSpeed;
    [SerializeField]
    public float damage;

    public EnemyEffectHandler enemyEffectHandler;

    private int currentWaypointId = -1;
    private Transform waypointTarget;
    public Transform[] waypoints;

    private void Awake()
    {
        upgradeManager = UpgradeManager.instance;
        statisticsManager = StatisticsManager.instance;
        gameManager = GameManager.instance;

        rb = GetComponent<Rigidbody2D>();
        enemyEffectHandler = GetComponent<EnemyEffectHandler>();
    }

    void Start()
    {
        health = variant.health;
        movementSpeed = variant.movementSpeed;
        damage = variant.damage;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = variant.sprite;
        spriteRenderer.material = variant.material;

        SetNextWaypoint();
    }

    private void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, waypointTarget.position);

        if(dist < 0.2f)
        {
            SetNextWaypoint();
        }
    }

    public void SetVariant(EnemyScriptableObject variant)
    {
        this.variant = variant;
    }

    public float DealDamage()
    {
        return variant.damage;
    }

    public void TakeDamage(float dmg, Turret from)
    {
        health -= dmg;

        if(health <= 0.0f)
        {
            Death(from);
        }
    }

    public void Death(Turret killedBy)
    {
        if(killedBy)
        {
            gameManager.IncreaseScore(variant.scoreOnKill);
            gameManager.IncreaseNeonBlocks(variant.neonBlocksOnKill);
            statisticsManager.AddKilledBlocksByTurret(killedBy.variant.name);
        }

        statisticsManager.AddKilledBlocksCount();
        upgradeManager.IncreaseExperience();
        Destroy(gameObject);
    }

    private void SetNextWaypoint()
    {
        currentWaypointId++;

        if(currentWaypointId > waypoints.Length - 2)
        {
            return;
        }

        waypointTarget = waypoints[currentWaypointId + 1];

        Vector2 direction = (waypointTarget.position - transform.position).normalized;
        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRot);
        rb.velocity = direction * movementSpeed;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;

        Vector2 direction = (waypointTarget.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
    }
}
