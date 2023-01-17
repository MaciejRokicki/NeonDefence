using System;
using TMPro;
using UnityEngine;

public class WaveBarUI : MonoBehaviour
{
    private WaveManager waveManager;

    [SerializeField]
    private TextMeshProUGUI waveLabelValue;
    [SerializeField]
    private TextMeshProUGUI waveTimerLabelValue;
    [SerializeField]
    private RectTransform waveTimerFillBar;

    private TimeSpan waveTimeSpan;

    private void Awake()
    {
        waveManager = WaveManager.instance;
    }

    private void Start()
    {
        waveManager.OnNextWaveTimerChange += OnNextWaveTimer;
        waveManager.OnWaveChange += OnNextWave;

        foreach(EnemySpawner enemySpawner in waveManager.enemySpawners)
        {
            enemySpawner.OnEnemySpawn += OnEnemySpawn;
        }

        waveTimeSpan = new TimeSpan();

        OnNextWave(0);
    }

    private void OnNextWaveTimer(float time)
    {
        if(time < 0.0f)
        {
            waveTimerLabelValue.gameObject.SetActive(false);
            return;
        }
        else if(!waveTimerLabelValue.gameObject.activeSelf)
        {
            waveTimerLabelValue.gameObject.SetActive(true);
        }

        waveTimeSpan = TimeSpan.FromSeconds(time);

        waveTimerLabelValue.text = waveTimeSpan.ToString(@"ss\.ff\s");
        waveTimerFillBar.offsetMax = new Vector2(-(400.0f - 400.0f * time / waveManager.nextWaveRefresh), waveTimerFillBar.offsetMax.y);
    }

    private void OnEnemySpawn()
    {
        waveTimerFillBar.offsetMax = new Vector2(-(400.0f - 400.0f * waveManager.spawnedEnemies / waveManager.currentEnemiesCount), waveTimerFillBar.offsetMax.y);
    }

    private void OnNextWave(int wave)
    {
        waveLabelValue.text = wave.ToString();
    }
}
