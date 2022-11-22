using Assets.Scripts.Game.Upgrades.InGameUpgrades;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    public static UpgradeManager instance { get { return _instance; } }

    private TurretManager turretManager;

    [SerializeField]
    private InRunUpgrade[] inGameUpgrades;

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
    }

    public void Test(InputAction.CallbackContext ctxt)
    {
        if(ctxt.performed)
        {
            float tierChance = Random.Range(0.0f, 100.0f);

            if(tierChance > 0.0f && tierChance <= 60.0f)
            {
                Debug.Log("Basic");
            }
            else if (tierChance > 60.0f && tierChance <= 80.0f)
            {
                Debug.Log("Fine");
            }
            else if(tierChance > 80.0f && tierChance <= 90.0f)
            {
                Debug.Log("Superior");
            }
            else if(tierChance > 90.0f && tierChance <= 98.0f)
            {
                Debug.Log("Epic");
            }
            else if(tierChance > 98.0f && tierChance <= 100.0f)
            {
                Debug.Log("Legendary");
            }

            InRunUpgrade iru = inGameUpgrades[Random.Range(0, inGameUpgrades.Length)];
            Debug.Log(iru.name);
            iru.Apply();
        }
    }
}
