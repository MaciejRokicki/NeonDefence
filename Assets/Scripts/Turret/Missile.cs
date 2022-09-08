using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Missile))]
public class Missile : MonoBehaviour
{
    private Turret turret;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        turret = transform.parent.GetComponent<Turret>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.localScale = turret.data.missileData.size;
        spriteRenderer.sprite = turret.data.missileData.sprite;
        spriteRenderer.material = turret.data.missileData.material;
        GetComponent<BoxCollider2D>().size = turret.data.missileData.size;
    }

    private void Update()
    {
        transform.position += transform.rotation * Vector2.up * turret.data.missileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);

            Destroy(this.gameObject);
        }
    }
}
