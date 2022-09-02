using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();

            TakeDamage(enemy.DealDamage());
            enemy.Death();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health < 0)
        {
            //TODO: GameOver
        }
    }
}
