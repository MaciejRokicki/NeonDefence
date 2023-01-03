using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    private UIManager uiManager;
    private StatisticsManager statisticsManager;

    public Vector2 mapSize = new Vector2(50.0f, 30.0f);
    public float Health;
    [SerializeField]
    public float MaxHealth = 100.0f;
    [SerializeField]
    private int NeonBlocks;

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
        uiManager = UIManager.instance;
        statisticsManager = StatisticsManager.instance;

        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            uiManager.ShowPauseAndGameOverMenu(true);
        }

        OnHealthChange(Health);
    }

    public void IncreaseHealth(float health)
    {
        this.Health += health;

        if(this.Health > MaxHealth)
        {
            this.Health = MaxHealth;
        }

        OnHealthChange(this.Health);
    }

    public void IncreaseMaxHealth(float health, bool addHealth)
    {
        MaxHealth += health;

        if(addHealth)
        {
            this.Health += health;
        }

        OnHealthChange(this.Health);
    }

    public int GetNeonBlocks() => NeonBlocks;

    public void AddNeonBlocks(int amount)
    {
        NeonBlocks += amount;
        statisticsManager.AddEarnedNeonBlocksCount(amount);
        OnNeonBlockChange(NeonBlocks);
    }

    public void RemoveNeonBlocks(int amount)
    {
        NeonBlocks -= amount;
        OnNeonBlockChange(NeonBlocks);
    }
}
