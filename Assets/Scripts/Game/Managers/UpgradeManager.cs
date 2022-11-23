using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.Scripts.Game.Upgrades.InGameUpgrades;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    public static UpgradeManager instance { get { return _instance; } }

    private TurretManager turretManager;

    public TierScriptableObject[] tiers;
    [SerializeField]
    private InRunUpgrade[] inGameUpgrades;

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

        string[] files = Directory.GetFiles("Assets/ScriptableObjects/Upgrades/Tiers", "*.asset");
        tiers = new TierScriptableObject[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            TierScriptableObject tier = AssetDatabase.LoadAssetAtPath(files[i], typeof(TierScriptableObject)) as TierScriptableObject;
            tiers[i] = tier;
        }

        foreach (TierScriptableObject tier in tiers)
        {
            inGameUpgradesToRand[tier.Name] = new List<InRunUpgrade>();
        }
    }

    private void Start()
    {
        foreach (InRunUpgrade upgrade in inGameUpgrades)
        {
            inGameUpgradesToRand[upgrade.tier.Name].Add(upgrade);
        }
    }

    public void Test(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            float tierChance = Random.Range(0.0f, 100.0f);

            foreach(TierScriptableObject tier in tiers)
            {
                if(tierChance > tier.MinChance && tierChance <= tier.MaxChance)
                {
                    InRunUpgrade iru = inGameUpgradesToRand[tier.Name][Random.Range(0, inGameUpgradesToRand[tier.Name].Count())];
                    Debug.Log(iru.name);
                    iru.Apply();

                    break;
                }
            }
        }
    }
}
