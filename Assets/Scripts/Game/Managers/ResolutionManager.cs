using UnityEngine;

public class ResolutionManager : MonoBehaviour 
{
    private static ResolutionManager _instance;
    public static ResolutionManager instance { get { return _instance; } }

    private Resolution nativeResolution;

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

        nativeResolution = Screen.currentResolution;
    }

    private void Start()
    {
        SetScaledResolution();
    }

    public void SetScaledResolution()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = 60;

            if (Screen.width > 720)
            {
                Screen.SetResolution((int)(Screen.width * 0.5), (int)(Screen.height * 0.5), true, 60);
            }
        }
    }

    public void SetNativeResolution()
    {
        Screen.SetResolution(nativeResolution.width, nativeResolution.height, true);
    }
}
