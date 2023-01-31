using UnityEngine;

public class EnemySpawnerPathPlaceholder : MonoBehaviour
{
    private WaveManager waveManager;

    [SerializeField]
    private EnemySpawner[] enemySpawners;

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
        if (gameObject.activeSelf && enemySpawners[0].spawnStartWave - 1 <= wave)
        {
            gameObject.SetActive(false);
        }
    }
}
