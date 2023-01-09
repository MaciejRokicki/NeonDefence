using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainMenuPressAnyKey;

    private void Start()
    {
        switch(SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                mainMenuPressAnyKey.text = "Touch to play";

                if(Screen.width > 720)
                {
                    Screen.SetResolution((int)(Screen.width * 0.5), (int)(Screen.height * 0.5), true, 60);
                }

                break;

            default:
                mainMenuPressAnyKey.text = "Press any key to play";
                break;
        }
    }

    public void LoadGame(InputAction.CallbackContext ctxt)
    {
        SceneManager.LoadScene(1);
    }
}
