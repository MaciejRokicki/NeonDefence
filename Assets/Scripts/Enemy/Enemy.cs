using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer), typeof(EnemyEffectHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyScriptableObject data;
    private Rigidbody2D rb;

    [SerializeField]
    private float health;
    [SerializeField]
    public float movementSpeed;

    public EnemyEffectHandler enemyEffectHandler;

    private int currentWaypointId = -1;
    private Transform waypointTarget;
    public Transform[] waypoints;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyEffectHandler = GetComponent<EnemyEffectHandler>();
    }

    void Start()
    {
        health = data.health;
        movementSpeed = data.movementSpeed;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = data.sprite;
        spriteRenderer.material = data.material;

        if (data.lightSource)
        {
            Light2D lightSource = gameObject.AddComponent<Light2D>();
            lightSource.color = spriteRenderer.material.color;
            lightSource.pointLightInnerRadius = data.lightSourceInnerRadius;
            lightSource.pointLightOuterRadius = data.lightSourceOuterRadius;
        }

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
        data = variant;
    }

    public float DealDamage()
    {
        return data.damage;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if(health <= 0.0f)
        {
            Death();
        }
    }

    public void Death()
    {
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
