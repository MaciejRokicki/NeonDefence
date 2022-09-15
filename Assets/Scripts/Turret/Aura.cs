using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    private Turret turret;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    private EnemyHitEffectComponent enemyHitEffectComponent;

    private float pulseTimer = 0.0f;
    private float pulseAnimationTimer = 0.0f;
    [SerializeField]
    private List<GameObject> enemies;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        spriteRenderer.sprite = turret.data.auraSprite;
        spriteRenderer.material = turret.data.auraMaterial;
        spriteRenderer.size = new Vector2(turret.data.auraRange, turret.data.auraRange);
        circleCollider.radius = turret.data.auraRange / 2;

        enemyHitEffectComponent = new BasicEnemyHitEffectComponent();

        if (turret.data.auraSlowdown)
        {
            enemyHitEffectComponent = new SlowdownEffectDecorator(
                turret, 
                gameObject, 
                enemyHitEffectComponent,
                float.PositiveInfinity,
                turret.data.auraSlowdownEffectiveness
            );
        }

        enemies = new List<GameObject>();
    }

    private void Update()
    {
        pulseTimer += Time.deltaTime;
        pulseAnimationTimer += Time.deltaTime;

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f + Mathf.Sin(pulseAnimationTimer * Mathf.PI/ 2) / 2);

        if(pulseAnimationTimer > 2.0f)
        {
            pulseAnimationTimer = 0.0f;
        }

        if(pulseTimer > 1.0f)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i])
                {
                    enemies[i].GetComponent<Enemy>().TakeDamage(turret.data.auraDamage);
                }
            }

            pulseTimer = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemyHitEffectComponent.OnEnemyEnter(collision.GetComponent<Enemy>());
            enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyHitEffectComponent.OnEnemyExit(collision.GetComponent<Enemy>());
            enemies.Remove(collision.gameObject);
        }
    }

    public void SetTurret(Turret turret)
    {
        this.turret = turret;
    }
}
