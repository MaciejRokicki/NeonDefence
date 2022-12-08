using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    public static UpgradeManager instance { get { return _instance; } }

    //TODO: usunac
    [SerializeField]
    private int currentExperience = 0;
    [SerializeField]
    private int experienceToNextRoll = 2;

    public delegate void ExperienceChangeCallback(int experience, int experienceToNextRoll);
    public event ExperienceChangeCallback OnExperienceChange;

    public TierScriptableObject[] tiers;
    public float tierMaxChance = 0;
    [SerializeField]
    private InRunUpgrade[] inRunUpgrades;

    private Dictionary<string, List<InRunUpgrade>> inGameUpgradesToRand;

    //TODO: usunac
    [SerializeField]
    private InRunUpgrade[] inRunUpgradesInRoll;

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
        Debug.Log("ROLL");
        inRunUpgradesInRoll = new InRunUpgrade[3];

        for (int i = 0; i < 3; i ++)
        {
            //InRunUpgrade inRunUpgrade = RandomizeInRunUpgrade();
            //inRunUpgradesInRoll[i] = inRunUpgrade;

            //if(inRunUpgrade.Unique)
            //{
            //    inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
            //}
        }
    }

    public void PickInRunUpgrade(InputAction.CallbackContext ctxt)
    {
        //if (inRunUpgrade.Unique)
        //{
        //    inGameUpgradesToRand[inRunUpgrade.Tier.Name].Remove(inRunUpgrade);
        //}

        //Debug.Log($"{inRunUpgrade.Tier.name} {inRunUpgrade.name} {inGameUpgradesToRand[inRunUpgrade.Tier.Name].Count()}");
        //inRunUpgrade.Apply();
    }

    #nullable enable
    public InRunUpgrade? RandomizeInRunUpgrade()
    {
        InRunUpgrade? inRunUpgrade = null;
        float tierChance = Random.Range(0.0f, tierMaxChance);

        foreach(TierScriptableObject tier in tiers)
        {
            if(tierChance > tier.MinChance && tierChance <= tier.MaxChance)
            {
                inRunUpgrade = inGameUpgradesToRand[tier.Name][Random.Range(0, inGameUpgradesToRand[tier.Name].Count())];

                break;
            }
        }

        return inRunUpgrade;
    }
    #nullable disable

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
