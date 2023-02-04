using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(EnemyEffectHandler))]
public class Enemy : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    private StatisticsManager statisticsManager;
    private GameManager gameManager;
    private WaveManager waveManager;

    [SerializeField]
    public EnemyScriptableObject variant;
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer effectSprite;

    [SerializeField]
    private float health;
    [SerializeField]
    public float movementSpeed;
    [SerializeField]
    public float damage;

    public EnemyEffectHandler enemyEffectHandler;
    public Color slowdownEffectSpriteColor;
    public Color poisonEffectSpriteColor;
    public Color poisonAndSlowdownEffectSpriteColor;

    private int currentWaypointId = -1;
    private Transform waypointTarget;
    private Transform[] waypoints;

    private void Awake()
    {
        upgradeManager = UpgradeManager.instance;
        statisticsManager = StatisticsManager.instance;
        gameManager = GameManager.instance;
        waveManager = WaveManager.instance;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyEffectHandler = GetComponent<EnemyEffectHandler>();
        effectSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    private void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, waypointTarget.position);

        if(dist < 0.2f)
        {
            SetNextWaypoint();
        }
    }

    public Enemy SetVariant(EnemyScriptableObject variant)
    {
        this.variant = variant;
        currentWaypointId = -1;

        health = variant.health;
        movementSpeed = variant.movementSpeed;
        damage = variant.damage;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = variant.sprite;
        spriteRenderer.material = variant.material;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        GetComponent<SpriteMask>().sprite = variant.sprite;
        effectSprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        animator.Play("Idle", 0, 0f);

        enemyEffectHandler.Reset();

        return this;
    }

    public float DealDamage()
    {
        return variant.damage;
    }

    public void TakeDamage(float dmg, Turret from)
    {
        if(!gameObject.activeSelf)
        {
            return;
        }

        if(dmg <= 0.0f)
        {
            return;
        }

        if(gameObject.activeSelf)
            animator.Play("EnemyHitAnimation");

        health -= dmg;

        if(health <= 0.0f)
        {
            Death(from);
        }
    }

    public void Death(Turret killedBy)
    {
        if (killedBy)
        {
            gameManager.IncreaseScore(variant.scoreOnKill);
            gameManager.IncreaseNeonBlocks(variant.neonBlocksOnKill);
            statisticsManager.AddKilledBlocksByTurret(killedBy.variant.name);
            statisticsManager.AddKilledBlocksCount();
            upgradeManager.IncreaseExperience();
        }

        waveManager.enemyPool.Release(this);
    }

    public Enemy SetEnemySpawnerPosition(EnemySpawner enemySpawner)
    {
        this.transform.position = enemySpawner.transform.position;

        return this;
    }

    public Enemy SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = variant.GetBaseMovementSpeed() < movementSpeed ? variant.GetBaseMovementSpeed() : movementSpeed;

        Vector2 direction = (waypointTarget.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        return this;
    }

    public Enemy SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;

        return this;
    }

    public Enemy SetNextWaypoint()
    {
        currentWaypointId++;

        if(currentWaypointId > waypoints.Length - 2)
        {
            return this;
        }

        waypointTarget = waypoints[currentWaypointId + 1];

        Vector2 direction = (waypointTarget.position - transform.position).normalized;
        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, zRot);
        rb.velocity = direction * movementSpeed;

        return this;
    }
}
