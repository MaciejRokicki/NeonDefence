using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyScriptableObject data;

    [SerializeField] //TODO: usunac SerializeField
    private float currentHealth;

    private void Awake()
    {
    }

    void Start()
    {
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
        
    }

    public void SetVariant(EnemyScriptableObject variant)
    {
        data = variant;
    }

    public void DealDamage()
    {

    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth < 0.0f)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
