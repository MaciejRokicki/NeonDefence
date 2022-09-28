using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    public Vector2 mapSize = new Vector2(50.0f, 30.0f);
    public float health = 100.0f;

    public delegate void HealthChangeCallback(float health);
    public event HealthChangeCallback OnHealthChange;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            //TODO: GameOver
        }

        OnHealthChange(health);
    }
}
