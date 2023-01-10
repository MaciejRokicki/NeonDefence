using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    private UIManager uiManager;
    private StatisticsManager statisticsManager;
    private WaveManager waveManager;

    public Vector2 mapSize = new Vector2(50.0f, 30.0f);
    private float health;
    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private int neonBlocks;

    private int score = 0;

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
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        statisticsManager = StatisticsManager.instance;
        waveManager = WaveManager.instance;

        health = maxHealth;

        waveManager.OnWaveChange += OnWaveChange;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            uiManager.ShowPauseAndGameOverMenu(true);
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

    public float GetHealth() => health;

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

    public void IncreaseNeonBlocks(int amount)
    {
        neonBlocks += amount;
        statisticsManager.AddEarnedNeonBlocksCount(amount);
        OnNeonBlockChange(neonBlocks);
    }

    public void RemoveNeonBlocks(int amount)
    {
        neonBlocks -= amount;
        OnNeonBlockChange(neonBlocks);
    }

    public void IncreaseScore(int score) => this.score += score;

    public int GetScore() => score;

    private void OnWaveChange(int wave)
    {
        score += 25;
    }
}
