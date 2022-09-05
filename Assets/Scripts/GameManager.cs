using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Map settings")]
    [SerializeField]
    private SpriteRenderer background;
    public Vector2 mapSize = new Vector2(50.0f, 30.0f);

    [Header("Wave & enemy settings")]
    public int currentWave = 0;
    public float currentWaveTime = 0.0f;
    public float nextWaveRefresh = 15.0f;
    public int spawnedEnemies = 0;
    public int enemiesPerSpawner = 0;
    public float enemySpawnTime = 1.0f;
    [SerializeField]
    private int enemiesCount = 0;
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    public EnemyScriptableObject[] enemiesVariants;
    [SerializeField]
    private EnemySpawner[] enemySpawners;

    private void Awake()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }

    private void Start()
    {
        background.size = mapSize;
        SetEnemiesCount();
    }

    private void Update()
    {
        if(spawnedEnemies == enemiesCount)
        {
            currentWaveTime += Time.deltaTime;

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

        int enemyMultiplier = currentWave / 10 == 0 ? 1 : currentWave / 10;

        if(currentWave > 9 && currentWave % 10 == 0)
        {
            enemiesPerSpawner = 0;
            //TODO: ulepszac statystyki przeciwnikow
        }

        enemiesPerSpawner += enemyMultiplier * 3;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetEnemies();
        }

        spawnedEnemies = 0;

        SetEnemiesCount();
    }

    private void SetEnemiesCount()
    {
        enemiesCount = 0;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if(spawner.spawnStartWave <= currentWave)
            {
                enemiesCount += enemiesPerSpawner;
            }
        }
    }
}
