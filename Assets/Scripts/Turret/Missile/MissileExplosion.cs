using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    private Turret turret;
    private EnemyHitEffectComponent missileShotEffectComponent;

    private SpriteRenderer spriteRenderer;

    private float timer = 0.0f;
    private float growingTime = 0.2f;
    private float destroyTimer = 0.0f;
    private float reductionTime = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = turret.data.explosionSprite;
        spriteRenderer.material = turret.data.explosionMaterial;

        RaycastHit2D[] hitAll = Physics2D.CircleCastAll(transform.position, turret.data.explosionRange, Vector2.zero, 0.0f, LayerMask.GetMask("Enemy"));

        if(turret.data.copyMissileEffects)
        {
            foreach (RaycastHit2D hit in hitAll)
            {
                missileShotEffectComponent.OnEnemyEnter(hit.transform.gameObject.GetComponent<Enemy>());
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(turret.data.explosionDamage);
            }
        }
        else
        {
            foreach (RaycastHit2D hit in hitAll)
            {
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(turret.data.explosionDamage);
            }
        }


        Destroy(gameObject, growingTime + reductionTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer < growingTime)
        {
            float size = turret.data.explosionRange * 1 / growingTime * timer;
            spriteRenderer.size = new Vector2(size, size);
        }
        else    
        {
            destroyTimer += Time.deltaTime;

            if(destroyTimer < reductionTime)
            {
                float size = turret.data.explosionRange * (reductionTime - destroyTimer);
                spriteRenderer.size = new Vector2(size, size);
            }
        }
    }

    public MissileExplosion SetTurret(Turret turret)
    {
        this.turret = turret;

        return this;
    }

    public MissileExplosion SetMissileShotEffectComponent(EnemyHitEffectComponent missileShotEffectComponent)
    {
        this.missileShotEffectComponent = missileShotEffectComponent;

        return this;
    }
}
