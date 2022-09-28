using UnityEngine;

public class Base : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();

            gameManager.TakeDamage(enemy.DealDamage());
            enemy.Death();
        }
    }
}
