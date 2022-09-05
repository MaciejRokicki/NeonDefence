#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;
    public int spawnStartWave = 1;
    [SerializeField]
    private int spawnedEnemies = 0;
    private float spawnTime = 0.0f;
    public Transform[] waypoints;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (spawnStartWave <= gameManager.currentWave)
        {
            if (spawnedEnemies != gameManager.enemiesPerSpawner)
            {
                spawnTime += Time.deltaTime;

                if (spawnTime > gameManager.enemySpawnTime)
                {
                    SpawnEnemy();

                    spawnTime = 0.0f;
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemyVariant = UnityEngine.Random.Range(0, gameManager.enemiesVariants.Length);

        GameObject enemy = Instantiate(gameManager.enemyPrefab, this.transform.position, Quaternion.identity, gameManager.enemiesParent.transform);
        enemy.GetComponent<Enemy>().SetVariant(gameManager.enemiesVariants[randomEnemyVariant]);

        enemy.GetComponent<Enemy>().waypoints = waypoints;

        gameManager.spawnedEnemies++;
        spawnedEnemies++;
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
