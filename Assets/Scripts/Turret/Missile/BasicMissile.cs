using UnityEngine;

public class BasicMissile : Missile
{
    private void Update()
    {
        transform.position += transform.rotation * Vector2.up * turret.data.missileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);

            Destroy(this.gameObject);
        }
    }
}
