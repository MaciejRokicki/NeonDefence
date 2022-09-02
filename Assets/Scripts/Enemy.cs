using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject data;
    private Rigidbody2D rb;

    [SerializeField] //TODO: usunac SerializeField
    private float currentHealth;

    private int currentWaypointId = -1;
    private Transform waypointTarget;
    public Transform[] waypoints;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetNextWaypoint();

        currentHealth = data.health;

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
    }

    private void Update()
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
        currentHealth -= dmg;

        if(currentHealth < 0.0f)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    private void SetNextWaypoint()
    {
        currentWaypointId++;

        if(currentWaypointId > waypoints.Length - 2)
        {
            return;
        }

        waypointTarget = waypoints[currentWaypointId + 1];

        //Vector2 pos = this.transform.position;

        //float xVelocity = 0.0f, yVelocity = 0.0f;

        //float x = Mathf.SmoothDamp(pos.x, Mathf.Round(pos.x), ref xVelocity, 0.02f);
        //float y = Mathf.SmoothDamp(pos.y, Mathf.Round(pos.y), ref yVelocity, 0.02f);

        //transform.position = new Vector2(x, y);

        //Vector2 direction = (waypoints[currentWaypointId + 1].position - waypoints[currentWaypointId].position).normalized;
        Vector2 direction = (waypointTarget.position - transform.position).normalized;
        rb.velocity = direction * data.movementSpeed;
    }
}
