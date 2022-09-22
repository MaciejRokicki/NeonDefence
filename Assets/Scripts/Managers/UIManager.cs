using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }

    [SerializeField]
    private GameUI gameUI;

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

    public void ToggleMenuSide()
    {
        gameUI.ToggleMenuSide();
    }

    public void ShowTurretInfo(Turret turret)
    {
        gameUI.ShowTurretInfo(turret);
    }

    public void HideTurretInfo()
    {
        gameUI.HideTurretInfo();
    }
}
