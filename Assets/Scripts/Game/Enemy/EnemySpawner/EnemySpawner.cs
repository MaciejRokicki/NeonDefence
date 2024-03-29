#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private WaveManager waveManager;
    public int spawnStartWave = 1;
    [SerializeField]
    private int spawnedEnemies = 0;
    private float spawnTime = 0.0f;
    public Transform[] waypoints;

    public delegate void EnemySpawnedCallback();
    public event EnemySpawnedCallback OnEnemySpawn;

    private void Awake()
    {
        waveManager = WaveManager.instance;
    }

    private void Update()
    {
        if (spawnStartWave <= waveManager.currentWave)
        {
            if (spawnedEnemies != waveManager.enemiesPerSpawner)
            {
                spawnTime += Time.deltaTime;

                if (spawnTime > waveManager.enemySpawnTime)
                {
                    SpawnEnemy();

                    spawnTime = 0.0f;
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        int randomEnemyVariant = Random.Range(0, waveManager.availableVariants.Count);
        EnemyScriptableObject enemyRandVariant = waveManager.availableVariants[randomEnemyVariant];

        Enemy enemy = waveManager.enemyPool
            .Get()
            .SetEnemySpawnerPosition(this)
            .SetVariant(enemyRandVariant)
            .SetWaypoints(waypoints)
            .SetNextWaypoint()
            .SetMovementSpeed(enemyRandVariant.movementSpeed);

        waveManager.spawnedEnemies++;
        spawnedEnemies++;

        OnEnemySpawn();
    }

    public void ResetEnemies()
    {
        spawnedEnemies = 0;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if(waypoints != null && waypoints.Length > 0)
        {
            Handles.color = Color.black;

            for (int i = 1; i < waypoints.Length; i++)
            {
                Handles.DrawLine(waypoints[i - 1].position, waypoints[i].position, 10.0f);
            }
        }
    }
#endif
}
