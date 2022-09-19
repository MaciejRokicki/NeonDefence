using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager instance { get { return _instance; } }

    [HideInInspector]
    public PlayerInput playerInput;

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

        playerInput = GetComponent<PlayerInput>();
    }
}
