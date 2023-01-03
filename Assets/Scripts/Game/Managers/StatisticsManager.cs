using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    private static StatisticsManager _instance;
    public static StatisticsManager instance { get { return _instance; } }

    private TurretManager turretManager;

    private float timePlayed = 0.0f;
    private int killedBlocks = 0;
    private int pickedUpgrades = 0;
    private int earnedNeonBlocks = 0;

    private Dictionary<string, int[]> turretStats;

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
        turretManager = TurretManager.instance;

        turretStats = new();

        foreach(TurretScriptableObject turret in turretManager.turretVariants)
        {
            turretStats.Add(turret.name, new int[2] { 0, 0 });
        }
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
    }

    public float GetTimePlayed() => timePlayed;

    public void AddKilledBlocksCount() => killedBlocks++;

    public int GetKilledBlocksCount() => killedBlocks;

    public void AddPickedUpgradesCount() => pickedUpgrades++;

    public int GetPickedUpgradesCount() => pickedUpgrades;

    public void AddEarnedNeonBlocksCount(int count) => earnedNeonBlocks += count;

    public int GetEarnedNeonBlocksCount() => earnedNeonBlocks;

    public void AddBuildedTurret(string turret) => turretStats[turret][0]++;

    public int GetBuildedTurret(string turret) => turretStats[turret][0];

    public void AddKilledBlocksByTurret(string turret) => turretStats[turret][1]++;

    public int GetKilledBlocksTurret(string turret) => turretStats[turret][1];
}