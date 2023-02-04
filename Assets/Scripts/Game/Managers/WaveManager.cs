using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
    [HideInInspector]
    public List<EnemyScriptableObject> availableVariants;
    [HideInInspector]
    public EnemySpawner[] enemySpawners;

    public IObjectPool<Enemy> enemyPool;

    private float enemyMultiplier = 1.0f;

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

        enemyPool = new ObjectPool<Enemy>(CreatePooledEnemy, OnTakeFromPool, OnReturnedToPool);
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

        if (currentWave > 9 && currentWave % 10 == 0)
        {
            enemiesPerSpawner = 0;
            enemyMultiplier = 1.0f;

            UpgradeEnemies();
        }

        enemiesPerSpawner = 6 + Mathf.CeilToInt(currentWave * enemyMultiplier);

        enemyMultiplier += 0.5f;

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
            enemy.movementSpeed += 0.15f;
        }
    }

    private Enemy CreatePooledEnemy()
    {
        Enemy enemyObject = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, enemiesParent.transform).GetComponent<Enemy>();

        return enemyObject;
    }

    private void OnReturnedToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
}
