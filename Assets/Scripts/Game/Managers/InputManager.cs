using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager instance { get { return _instance; } }

    [HideInInspector]
    public PlayerInput playerInput;

    [SerializeField]
    private GraphicRaycaster graphicRaycaster;

    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;

    //[HideInInspector]
    public GameObject pressedUiButton;

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

        pointerEventData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    public void ClickHandler(InputAction.CallbackContext ctxt)
    {
        //TODO: Mobile
        //if(ctxt.started)
        //{
            //pointerEventData.position = Mouse.current.position.ReadValue();
            pointerEventData.position = Touchscreen.current.position.ReadValue();
            raycastResults.Clear();

            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<Button>())
                {
                    pressedUiButton = result.gameObject;

                    break;
                }
            }
        //}

        if (ctxt.canceled)
        {
            pressedUiButton = null;
        }
    }
}
