using UnityEngine;

public class LaserMissile : Missile
{
    private float timer = 0.0f;

    private new void Awake()
    {
        turret = transform.parent.parent.GetComponent<Turret>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private new void Start()
    {
        base.Start();

        spriteRenderer.size = GetComponent<BoxCollider2D>().size = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            timer += Time.deltaTime;

            if(timer > turret.data.missilesPerSecond)
            {
                collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 0.0f;
    }
}
