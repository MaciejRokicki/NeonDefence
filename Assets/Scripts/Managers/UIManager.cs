using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }

    [SerializeField]
    private TurretInfoUI turretInfo;

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

    public void ShowTurretInfo(Turret turret)
    {
        turretInfo.Show(turret);
    }

    public void HideTurretInfo()
    {
        turretInfo.Hide();
    }
}
