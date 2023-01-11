using UnityEngine;

public class ResolutionManager : MonoBehaviour 
{
    private static ResolutionManager _instance;
    public static ResolutionManager instance { get { return _instance; } }

    private Resolution nativeResolution;
    private float aspectRatio;
    private int scaledWidth = 720;

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
        aspectRatio = (float)Screen.width / Screen.height;
        Debug.Log(aspectRatio);
        SetScaledResolution();
    }

    public void SetScaledResolution()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = 60;
            
            Screen.SetResolution((int)(scaledWidth * aspectRatio), scaledWidth, true);
        }
    }

    public void SetNativeResolution()
    {
        Screen.SetResolution(nativeResolution.width, nativeResolution.height, true);
    }
}
