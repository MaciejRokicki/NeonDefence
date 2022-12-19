using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    public static UpgradeManager instance { get { return _instance; } }

    private UIManager uIManager;

    //TODO: usunac
    [SerializeField]
    private int currentExperience = 0;
    [SerializeField]
    private int experienceToNextRoll = 2;

    public delegate void ExperienceChangeCallback(int experience, int experienceToNextRoll);
    public event ExperienceChangeCallback OnExperienceChange;

    [SerializeField]
    private GameObject rollUpgrades;
    [SerializeField]
    private RollUpgradeUI[] rollUpgradeUI;
    private int rollUpgradesCount = 3;
    public TierScriptableObject[] tiers;
    public float tierMaxChance = 0.0f;
    [SerializeField]
    private InRunUpgrade[] inRunUpgrades;

    private Dictionary<string, List<InRunUpgrade>> inGameUpgradesToRand;

    //TODO: usunac
    [SerializeField]
    private InRunUpgrade[] rollUpgradesCollection;

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

        inGameUpgradesToRand = new Dictionary<string, List<InRunUpgrade>>();

        GetTiers();
        GetUpgrades();
    }

    private void Start()
    {
        uIManager = UIManager.instance;
    }

    public void IncreaseExperience(int experience = 1)
    {
        currentExperience += experience;

        if(currentExperience >= experienceToNextRoll)
        {
            currentExperience = currentExperience % experienceToNextRoll;
            experienceToNextRoll += 1;

            InRunUpgradeRoll();
        }

        OnExperienceChange(currentExperience, experienceToNextRoll);
    }

    private void InRunUpgradeRoll()
    {
        uIManager.blockGameInteraction = true;

        rollUpgradesCollection = new InRunUpgrade[rollUpgradesCount];

        for (int i = 0; i < rollUpgradesCount; i ++)
        {
            InRunUpgrade inRunUpgrade = RandomizeInRunUpgrade();
            rollUpgradesCollection[i] = inRunUpgrade;

            if (inRunUpgrade.Unique)
            {
                inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
            }

            rollUpgradeUI[i].inRunUpgrade = inRunUpgrade;
        }

        rollUpgrades.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PickInRunUpgrade(int rollUpgradeId)
    {
        InRunUpgrade inRunUpgrade = rollUpgradesCollection[rollUpgradeId];

        if (inRunUpgrade.Unique)
        {
            inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
        }

        inRunUpgrade.Apply();

        rollUpgrades.SetActive(false);
        Time.timeScale = 1.0f;
        uIManager.blockGameInteraction = false;
    }

    #nullable enable
    public InRunUpgrade RandomizeInRunUpgrade()
    {
        InRunUpgrade? inRunUpgrade = null;

        InRunUpgrade randomizeInTier(TierScriptableObject tier)
        {
            int upgradeId = Random.Range(0, inGameUpgradesToRand[tier.Name].Count());
            inRunUpgrade = inGameUpgradesToRand[tier.Name][upgradeId];

            return inRunUpgrade;
        }

        while(inRunUpgrade == null || rollUpgradesCollection.Contains(inRunUpgrade))
        {
            float tierChance = Random.Range(0.0f, tierMaxChance);

            foreach (TierScriptableObject tier in tiers)
            {
                if(inGameUpgradesToRand[tier.Name].Count() == 0)
                {
                    tierChance = Random.Range(0.0f, tierMaxChance);

                    break;
                }

                if (tierChance > tier.MinChance && tierChance <= tier.MaxChance)
                {
                    inRunUpgrade = randomizeInTier(tier);

                    break;
                }
            }
        }

        return inRunUpgrade;
    }
    #nullable disable

    //TODO: AssetDatabase nie dziala na deploy'u (android)
    private void GetTiers()
    {
        string[] files = Directory.GetFiles("Assets/ScriptableObjects/Upgrades/Tiers", "*.asset");
        tiers = new TierScriptableObject[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            TierScriptableObject tier = AssetDatabase.LoadAssetAtPath(files[i], typeof(TierScriptableObject)) as TierScriptableObject;
            tiers[i] = tier;

            tierMaxChance = tier.MaxChance > tierMaxChance ? tier.MaxChance : tierMaxChance;
        }

        foreach (TierScriptableObject tier in tiers)
        {
            inGameUpgradesToRand[tier.Name] = new List<InRunUpgrade>();
        }
    }

    private void GetUpgrades()
    {
        string[] files = Directory.GetFiles("Assets/ScriptableObjects/Upgrades/InRunUpgrades", "*.asset");
        inRunUpgrades = new InRunUpgrade[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            InRunUpgrade inRunUpgrade = AssetDatabase.LoadAssetAtPath(files[i], typeof(InRunUpgrade)) as InRunUpgrade;
            inRunUpgrades[i] = inRunUpgrade;
        }

        foreach (TierScriptableObject tier in tiers)
        {
            inGameUpgradesToRand[tier.Name] = new List<InRunUpgrade>();
        }

        foreach (InRunUpgrade upgrade in inRunUpgrades)
        {
            inGameUpgradesToRand[upgrade.Tier.Name].Add(upgrade);
        }
    }
}
