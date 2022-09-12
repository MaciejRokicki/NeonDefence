using UnityEngine;

public class BasicMissile : Missile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(turret.data.damage);

            Destroy(this.gameObject);
        }
    }
}
