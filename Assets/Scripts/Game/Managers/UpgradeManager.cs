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

    private TurretManager turretManager;

    public TierScriptableObject[] tiers;
    public float tierMaxChance = 0;
    [SerializeField]
    private InRunUpgrade[] inRunUpgrades;

    private Dictionary<string, List<InRunUpgrade>> inGameUpgradesToRand;

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

        turretManager = TurretManager.instance;
        inGameUpgradesToRand = new Dictionary<string, List<InRunUpgrade>>();

        GetTiers();
        GetUpgrades();
    }

    private void Start()
    {

    }

    public void RandomizeInRunUpgrade(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            float tierChance = Random.Range(0.0f, tierMaxChance);

            foreach(TierScriptableObject tier in tiers)
            {
                if(tierChance > tier.MinChance && tierChance <= tier.MaxChance)
                {
                    //TODO: po uzupelnieniu upgrade'ow usunac
                    if (inGameUpgradesToRand[tier.Name].Count() == 0)
                        continue;

                    InRunUpgrade inRunUpgrade = inGameUpgradesToRand[tier.Name][Random.Range(0, inGameUpgradesToRand[tier.Name].Count())];

                    if(inRunUpgrade.Unique)
                    {
                        inGameUpgradesToRand[tier.Name].Remove(inRunUpgrade);
                    }

                    Debug.Log($"{inRunUpgrade.Tier.name} {inRunUpgrade.name} {inGameUpgradesToRand[tier.Name].Count()}");
                    inRunUpgrade.Apply();

                    break;
                }
            }
        }
    }

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
