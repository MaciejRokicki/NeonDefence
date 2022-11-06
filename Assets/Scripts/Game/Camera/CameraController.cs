using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CameraController : MonoBehaviour
{
    private InputManager playerInputManager;
    private GameManager gameManager;

    [SerializeField]
    private Vector2 cameraViewSize;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;

    [SerializeField]
    private float minZoom = 2.5f;
    [SerializeField]
    private float maxZoom = 10.0f;

    private float zoomVelocity = 0.0f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float speed = 0.05f;
    [SerializeField]
    private Vector2Int spaceOffset = new Vector2Int(20, 10);

    private Vector3? initialTouchPosition = null;

    private void Awake()
    {
        playerInputManager = InputManager.instance;
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        CalculatePositionLimits();
    }

    private void Update()
    {
        Move();
        TouchMove();
    }

    public void Move()
    {
        Vector2 mousePosition = playerInputManager.playerInput.actions["Look"].ReadValue<Vector2>();

        if (mousePosition.x <= spaceOffset.x || mousePosition.x >= Screen.width - spaceOffset.x || mousePosition.y <= spaceOffset.y || mousePosition.y >= Screen.height - spaceOffset.y)
        {
            Vector3 pos = (Camera.main.ScreenToWorldPoint(mousePosition) - transform.position).normalized;
            MoveHandler(pos);
        }
    }

    public void TouchMove()
    {
        if (Touch.activeFingers.Count != 0)
        {
            if (initialTouchPosition == null)
            {
                initialTouchPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
            }
            else
            {
                if (Touch.activeFingers[0].currentTouch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    Vector3 deltaPosition = (Vector3)initialTouchPosition - Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
                    if (deltaPosition.magnitude > 0.1f)
                    {
                        MoveHandler(deltaPosition, 0.8f);
                    }
                }

                if (Touch.activeFingers[0].currentTouch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    initialTouchPosition = null;
                }
            }
        }
    }

    private void MoveHandler(Vector2 direction, float velocity = 1.0f)
    {
        velocity = Mathf.Clamp(velocity, 1.0f, 3.0f);
        direction.x *= velocity;
        direction.y *= velocity;

        Vector2 test = transform.position + (Vector3)direction;
        Vector3 newPosition;

        newPosition.x = Mathf.SmoothDamp(transform.position.x, test.x, ref xVelocity, speed);
        newPosition.y = Mathf.SmoothDamp(transform.position.y, test.y, ref yVelocity, speed);
        newPosition.z = -10.0f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    public void ZoomHandler(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();
        float scrollValue = Camera.main.orthographicSize;

        if(scroll > 0.0f)
        {
            scrollValue -= 1.0f;
        }
        else if (scroll < 0.0f)
        {
            scrollValue += 1.0f;
        }

        scrollValue = Mathf.SmoothDamp(Camera.main.orthographicSize, scrollValue, ref zoomVelocity, 0.01f);

        Camera.main.orthographicSize = Mathf.Clamp(scrollValue, minZoom, maxZoom);

        CalculatePositionLimits();
    }

    public void CalculatePositionLimits()
    {
        //float worldMenuWidth = Camera.main.ScreenToWorldPoint(new Vector3(uiManager.menuWidth, 0.0f)).x - Camera.main.ScreenToWorldPoint(Vector3.zero).x;

        cameraViewSize.x = Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraViewSize.y = Camera.main.orthographicSize;

        minX = cameraViewSize.x - gameManager.mapSize.x / 2;
        maxX = gameManager.mapSize.x / 2 - cameraViewSize.x/* + worldMenuWidth*/;
        minY = cameraViewSize.y - gameManager.mapSize.y / 2;
        maxY = gameManager.mapSize.y / 2 - cameraViewSize.y;
    }
}
