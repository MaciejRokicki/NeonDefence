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

    private UIManager uiManager;
    private TurretManager turretManager;
    private TurretPlaceholder turretPlaceholder;

    //TODO: usunac
    [SerializeField]
    private int currentExperience = 0;
    [SerializeField]
    private int experienceToNextRoll = 2;

    public delegate void ExperienceChangeCallback(int experience, int experienceToNextRoll);
    public event ExperienceChangeCallback OnExperienceChange;

    private int rollUpgradesCount = 3;
    public TierScriptableObject[] tiers;
    public float tierMaxChance = 0.0f;
    [SerializeField]
    private List<InRunUpgradeScriptableObject> inRunUpgrades;

    private Dictionary<string, List<InRunUpgradeScriptableObject>> inGameUpgradesToRand;

    //TODO: usunac
    [SerializeField]
    private InRunUpgradeScriptableObject[] rollUpgradesCollection;

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

        inGameUpgradesToRand = new Dictionary<string, List<InRunUpgradeScriptableObject>>();

        GetTiers();
        GetUpgrades();
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        turretManager = TurretManager.instance;
        turretPlaceholder = TurretPlaceholder.instance;
    }

    public void IncreaseExperience(int experience = 1)
    {
        currentExperience += experience;

        if(currentExperience >= experienceToNextRoll)
        {
            currentExperience = currentExperience % experienceToNextRoll;
            experienceToNextRoll = Mathf.CeilToInt(experienceToNextRoll * 1.5f);

            InRunUpgradeRoll();
        }

        OnExperienceChange(currentExperience, experienceToNextRoll);
    }

    private void InRunUpgradeRoll()
    {
        turretManager.UnselectVariant();
        rollUpgradesCollection = new InRunUpgradeScriptableObject[rollUpgradesCount];

        InRunUpgradeScriptableObject[] inRunUpgrades = new InRunUpgradeScriptableObject[rollUpgradesCount];

        for (int i = 0; i < rollUpgradesCount; i ++)
        {
            InRunUpgradeScriptableObject inRunUpgrade = RandomizeInRunUpgrade();
            rollUpgradesCollection[i] = inRunUpgrade;

            if (inRunUpgrade.Unique)
            {
                inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
            }

            inRunUpgrades[i] = inRunUpgrade;
        }

        uiManager.ShowUpgradeRoll(inRunUpgrades);

        //PickInRunUpgrade(Random.Range(0, 3));
    }

    public void PickInRunUpgrade(int rollUpgradeId)
    {
        InRunUpgradeScriptableObject inRunUpgrade = rollUpgradesCollection[rollUpgradeId];

        if (inRunUpgrade.Unique)
        {
            inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
        }

        inRunUpgrade.Apply();

        uiManager.HideUpgradeRoll();
    }

    #nullable enable
    public InRunUpgradeScriptableObject RandomizeInRunUpgrade()
    {
        InRunUpgradeScriptableObject? inRunUpgrade = null;

        InRunUpgradeScriptableObject randomizeInTier(TierScriptableObject tier)
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

    private void GetTiers()
    {
        foreach(TierScriptableObject tier in tiers)
        {
            tierMaxChance = tier.MaxChance > tierMaxChance ? tier.MaxChance : tierMaxChance;
            inGameUpgradesToRand[tier.Name] = new List<InRunUpgradeScriptableObject>();
        }
    }

    private void GetUpgrades()
    {
        foreach (TierScriptableObject tier in tiers)
        {
            inGameUpgradesToRand[tier.Name] = new List<InRunUpgradeScriptableObject>();
        }

        foreach (InRunUpgradeScriptableObject upgrade in inRunUpgrades)
        {
            inGameUpgradesToRand[upgrade.Tier.Name].Add(upgrade);
        }
    }

    public void AddUpgrade(InRunUpgradeScriptableObject inRunUpgrade)
    {
        inRunUpgrades.Add(inRunUpgrade);
    }
}
