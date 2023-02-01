using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainMenuPressAnyKey;
    [SerializeField]
    private GameObject pressEscapeToExitObject;

    private void Start()
    {
        switch(SystemInfo.deviceType)
        {
            case DeviceType.Handheld:
                mainMenuPressAnyKey.text = "Touch to play";
                break;

            default:
                mainMenuPressAnyKey.text = "Press any key to play";
                pressEscapeToExitObject.SetActive(true);
                break;
        }
    }

    public void LoadGame(InputAction.CallbackContext ctxt)
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame(InputAction.CallbackContext ctxt)
    {
        Application.Quit();
    }
}
