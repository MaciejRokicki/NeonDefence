using UnityEngine;

public class EnemySpawnerPathPlaceholder : MonoBehaviour
{
    private WaveManager waveManager;

    [SerializeField]
    private EnemySpawner[] enemySpawners;
    [SerializeField]
    private EnemySpawnerPathMarker[] enemySpawnerPathMarkers;

    private void Awake()
    {
        waveManager = WaveManager.instance;
    }

    private void Start()
    {
        waveManager.OnWaveChange += UnlockPath;
    }

    private void UnlockPath(int wave)
    {
        if (enemySpawners[0].spawnStartWave - 1 <= wave)
        {
            foreach(EnemySpawnerPathMarker marker in enemySpawnerPathMarkers)
            {
                marker.gameObject.SetActive(true);
            }
        }

        if (enemySpawners[0].spawnStartWave == wave)
        {
            if(gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

            foreach (EnemySpawnerPathMarker marker in enemySpawnerPathMarkers)
            {
                marker.gameObject.SetActive(false);
            }
        }
    }
}
