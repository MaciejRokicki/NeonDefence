using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }

    [SerializeField]
    private CameraController cameraController;


    public Vector2 currentResolution;
    public int menuWidth;

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
        //OnScreenResize();
    }

    private void Update()
    {
        if(currentResolution.x != Screen.width || currentResolution.y != Screen.height)
        {
            //OnScreenResize();
            cameraController.CalculatePositionLimits();
        }
    }

    //public void ShowTurretInfo(Turret turret)
    //{
    //    sideMenu.ShowTurretInfo(turret);
    //}

    //public void HideTurretInfo()
    //{
    //    sideMenu.HideTurretInfo();
    //}

    //private void OnScreenResize()
    //{
    //    currentResolution = new Vector2(Screen.width, Screen.height);
    //    menuWidth = (int)(0.25f * currentResolution.x);
    //}
}
