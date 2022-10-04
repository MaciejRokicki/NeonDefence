using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;
    public static WaveManager instance { get { return _instance; } }

    public delegate void NextWaveCallback(int wave);
    public event NextWaveCallback OnWaveChange;

    public delegate void TimeToNextWaveCallback(float time);
    public event TimeToNextWaveCallback OnNextWaveTimerChange;

    public int currentWave = 0;
    public float currentWaveTime = 0.0f;
    public float nextWaveRefresh = 15.0f;
    public int spawnedEnemies = 0;
    public int enemiesPerSpawner = 0;
    public float enemySpawnTime = 1.0f;
    public int currentEnemiesCount = 0;
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    [SerializeField]
    private EnemyScriptableObject[] enemyVariants;
    public List<EnemyScriptableObject> availableVariants; //TODO: HideInInspector
    [HideInInspector]
    public EnemySpawner[] enemySpawners;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }

    private void Start()
    {
        SetAvailableEnemyVariants();
        SetEnemiesCount();
    }

    private void Update()
    {
        if (spawnedEnemies == currentEnemiesCount)
        {
            currentWaveTime += Time.deltaTime;

            OnNextWaveTimerChange(nextWaveRefresh - currentWaveTime);

            if (currentWaveTime > nextWaveRefresh)
            {
                NextWave();
                currentWaveTime = 0.0f;
            }
        }
    }

    private void NextWave()
    {
        currentWave++;

        OnWaveChange(currentWave);

        SetAvailableEnemyVariants();

        int enemyMultiplier = currentWave / 10 == 0 ? 1 : currentWave / 10 + 1;

        if (currentWave > 9 && currentWave % 10 == 0)
        {
            enemiesPerSpawner = 0;

            UpgradeEnemies();
        }

        enemiesPerSpawner += enemyMultiplier * 2;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetEnemies();
        }

        spawnedEnemies = 0;

        SetEnemiesCount();
    }

    private void SetEnemiesCount()
    {
        currentEnemiesCount = 0;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.spawnStartWave <= currentWave)
            {
                currentEnemiesCount += enemiesPerSpawner;
            }
        }
    }

    private void SetAvailableEnemyVariants()
    {
        availableVariants.Clear();

        foreach (EnemyScriptableObject enemy in enemyVariants)
        {
            if (currentWave >= enemy.minWave)
            {
                availableVariants.Add(enemy);
            }
        }
    }

    private void UpgradeEnemies()
    {
        foreach (EnemyScriptableObject enemy in availableVariants)
        {
            enemy.health += enemy.health / 10.0f;
            enemy.damage++;
            enemy.movementSpeed += 0.5f;
        }
    }
}
