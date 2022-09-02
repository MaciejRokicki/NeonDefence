using System;
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

    //[Header("Environment")]
    //public GameObject environment;
    //public GameObject tilePrefab;

    private void Awake()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }

    private void Start()
    {
        background.size = mapSize;
        SetEnemiesCount();
        //PreparePath();
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
        enemiesCount = enemiesPerSpawner * enemySpawners.Length;
    }

    //private void PreparePath()
    //{
    //    foreach(EnemySpawner spawner in enemySpawners)
    //    {
    //        for(int i = 0; i < spawner.waypoints.Length - 1; i++)
    //        {
    //            Transform currentWaypoint = spawner.waypoints[i];
    //            Transform nextWaypoint = spawner.waypoints[i + 1];

    //            Vector2 direction = (nextWaypoint.position - currentWaypoint.position).normalized;

    //            int tilesCount = 0;

    //            if(direction == Vector2.up || direction == Vector2.down)
    //            {
    //                tilesCount = (int)Mathf.Abs(nextWaypoint.position.y - currentWaypoint.position.y);
    //            }
    //            else if(direction == Vector2.left || direction == Vector2.right)
    //            {
    //                tilesCount = (int)Mathf.Abs(nextWaypoint.position.x - currentWaypoint.position.x);
    //            }

    //            for(int j = 0; j < tilesCount; j++)
    //            {
    //                Vector2 newPosition = new Vector2(currentWaypoint.position.x, currentWaypoint.position.y) + direction * j;

    //                GameObject tile = Instantiate(tilePrefab, newPosition, Quaternion.identity, environment.transform);

    //                float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //                tile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, z);
    //            }
    //        }
    //    }
    //}
}
