using UnityEngine;

public class ResolutionManager : MonoBehaviour 
{
    private static ResolutionManager _instance;
    public static ResolutionManager instance { get { return _instance; } }

    [SerializeField]
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
    }

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            float aspectRatio = (float)Screen.width / Screen.height;

            Application.targetFrameRate = 60;
            Screen.SetResolution((int)(scaledWidth * aspectRatio), scaledWidth, true);
        }
    }
}
