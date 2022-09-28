using System;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInfo : MonoBehaviour
{
    private GameManager gameManager;
    private WaveManager waveManager;

    private const string WAVE_LABEL = "game-info-wave-label";
    private const string WAVE_TIME_LABEL = "game-info-wave-time-label";
    private const string BASE_HEALTH_PROGRESS_BAR = "game-info-base-health-progress-bar";

    private VisualElement rootVisualElement;

    private float maxHealth;

    private Label waveLabel;
    private Label waveTimeLabel;
    private ProgressBar baseHealthProgressBar;

    private StringBuilder waveStringBuilder;
    private StringBuilder timeStringBuilder;
    private StringBuilder healthStringBuilder;

    private void Awake()
    {
        gameManager = GameManager.instance;
        waveManager = WaveManager.instance;

        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        waveLabel = rootVisualElement.Q<Label>(WAVE_LABEL);
        waveTimeLabel = rootVisualElement.Q<Label>(WAVE_TIME_LABEL);
        baseHealthProgressBar = rootVisualElement.Q<ProgressBar>(BASE_HEALTH_PROGRESS_BAR);

        gameManager.OnHealthChange += UpdateHealthBar;
        waveManager.OnNextWaveTimeChange += UpdateTime;
        waveManager.OnWaveChange += UpdateWave;

        waveStringBuilder = new StringBuilder();
        timeStringBuilder = new StringBuilder();
        healthStringBuilder = new StringBuilder();
    }

    private void Start()
    {
        maxHealth = gameManager.health;

        UpdateWave(0);
        UpdateTime(0.0f);
        UpdateHealthBar(gameManager.health);
    }
    public void UpdateWave(int wave)
    {
        waveStringBuilder.Append("Wave: ");
        waveStringBuilder.Append(wave);
        waveLabel.text = waveStringBuilder.ToString();

        waveStringBuilder.Clear();
    }

    public void UpdateTime(float time)
    {
        time = time < 0.0f ? 0.0f : time;

        timeStringBuilder.Append("Next wave: ");
        timeStringBuilder.Append(TimeSpan.FromSeconds(time).ToString(@"ss\.ff"));

        waveTimeLabel.text = timeStringBuilder.ToString();

        timeStringBuilder.Clear();
    }

    public void UpdateHealthBar(float health)
    {
        baseHealthProgressBar.value = health;

        healthStringBuilder.Append(health.ToString());
        healthStringBuilder.Append("/");
        healthStringBuilder.Append(maxHealth);

        baseHealthProgressBar.title = healthStringBuilder.ToString();

        healthStringBuilder.Clear();
    }
}
