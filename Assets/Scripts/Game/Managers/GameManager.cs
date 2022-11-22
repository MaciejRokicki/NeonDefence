using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    public Vector2 mapSize = new Vector2(50.0f, 30.0f);
    private float health;
    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private int neonBlocks;

    public delegate void HealthChangeCallback(float health);
    public event HealthChangeCallback OnHealthChange;

    public delegate void NeonBlockChangeCallback(int neonBlocks);
    public event NeonBlockChangeCallback OnNeonBlockChange;

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

        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = 60;
        }
    }

    private void Start()
    {
        health = maxHealth;
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

    public void IncreaseHealth(float health)
    {
        this.health += health;

        if(this.health > maxHealth)
        {
            this.health = maxHealth;
        }

        OnHealthChange(this.health);
    }

    public void IncreaseMaxHealth(float health, bool addHealth)
    {
        maxHealth += health;

        if(addHealth)
        {
            this.health += health;
        }

        OnHealthChange(this.health);
    }

    public float GetMaxHealth() => maxHealth;

    public int GetNeonBlocks() => neonBlocks;

    public void AddNeonBlocks(int amount)
    {
        neonBlocks += amount;
        OnNeonBlockChange(neonBlocks);
    }

    public void RemoveNeonBlocks(int amount)
    {
        neonBlocks -= amount;
        OnNeonBlockChange(neonBlocks);
    }
}
