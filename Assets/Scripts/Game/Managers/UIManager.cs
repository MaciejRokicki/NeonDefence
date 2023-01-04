using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }

    private StatisticsManager statisticsManager;
    private GameManager gameManager;

    public bool blockGameInteraction = false;

    public GameObject PauseButton;
    public GameObject BuildingMenu;

    public GameObject PAGO;
    public TextMeshProUGUI PAGO_Title;
    public TextMeshProUGUI PAGO_GameInfoSection_Score;
    public TextMeshProUGUI PAGO_GameInfoSection_TimePlayed;
    public TextMeshProUGUI PAGO_GameInfoSection_KilledBlocks;
    public TextMeshProUGUI PAGO_GameInfoSection_PickedUpgrades;
    public TextMeshProUGUI PAGO_GameInfoSection_EarnedNeonBlocks;

    public TurretStatsInfo[] turretStatsInfos;
    private readonly Dictionary<int, string> turretIds = new()
    {
        { 0, "BasicTurret" },
        { 1, "LaserTurret" },
        { 2 , "RocketTurret" },
        { 3 , "AuraTurret" }
    };

    public Button PAGO_Resume;

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
    }

    private void Start()
    {
        statisticsManager = StatisticsManager.instance;
        gameManager = GameManager.instance;
    }

    public void ShowPauseAndGameOverMenu(bool gameOver = false)
    {
        Time.timeScale = 0.0f;
        blockGameInteraction = true;
        PauseButton.SetActive(false);
        BuildingMenu.SetActive(false);
        PAGO.SetActive(true);

        if(gameOver)
        {
            PAGO_Title.text = "Game over";
            PAGO_Resume.gameObject.SetActive(false);
        }
        else
        {
            PAGO_Title.text = "Pause";
            PAGO_Resume.gameObject.SetActive(true);
        }

        StringBuilder sb = new StringBuilder("Score: ").Append(gameManager.GetScore());
        PAGO_GameInfoSection_Score.text = sb.ToString();

        TimeSpan playedTimeSpan = TimeSpan.FromSeconds(statisticsManager.GetTimePlayed());
        sb.Clear().Append("Time played: ").Append(playedTimeSpan.ToString(@"mm\:ss"));
        PAGO_GameInfoSection_TimePlayed.text = sb.ToString();

        sb.Clear().Append("Killed blocks: ").Append(statisticsManager.GetKilledBlocksCount());
        PAGO_GameInfoSection_KilledBlocks.text = sb.ToString();

        sb.Clear().Append("Picked upgrades: ").Append(statisticsManager.GetPickedUpgradesCount());
        PAGO_GameInfoSection_PickedUpgrades.text = sb.ToString();

        sb.Clear().Append("Earned neon blocks: ").Append(statisticsManager.GetEarnedNeonBlocksCount());
        PAGO_GameInfoSection_EarnedNeonBlocks.text = sb.ToString();

        for(int i = 0; i < turretStatsInfos.Length; i++)
        {
            turretStatsInfos[i].SetBuildedTurretCount(statisticsManager.GetBuildedTurret(turretIds[i]));
            turretStatsInfos[i].SetKilledBlocks(statisticsManager.GetKilledBlocksTurret(turretIds[i]));
        }
    }

    public void HidePauseAndGameOverMenu()
    {
        Time.timeScale = 1.0f;
        blockGameInteraction = false;
        PauseButton.SetActive(true);
        BuildingMenu.SetActive(true);
        PAGO.SetActive(false);
    }

    public void TogglePauseMenu(InputAction.CallbackContext ctxt)
    {
        if(!PAGO.activeSelf)
        {
            ShowPauseAndGameOverMenu();           
        }
        else
        {
            HidePauseAndGameOverMenu();
        }
    }

    public void PlayAgain()
    {
        HidePauseAndGameOverMenu();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
