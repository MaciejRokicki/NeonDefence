using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class CameraController : MonoBehaviour
{
    private InputManager playerInputManager;
    private GameManager gameManager;
    private TurretManager turretManager;

    [SerializeField]
    private Vector2 cameraViewSize;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector3 moveVectorVelocity = Vector3.zero;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float speed = 0.05f;
    [SerializeField]
    private Vector2Int spaceOffset = new Vector2Int(20, 10);

    [SerializeField]
    private float minZoom = 2.5f;
    [SerializeField]
    private float maxZoom = 10.0f;

    private float zoomVelocity = 0.0f;

    private Vector3? initialTouchPosition = null;
    private float previousTouchZoomDistance = 0.0f;
    private Vector3 zoomCenterPosition;

    private void Awake()
    {
        playerInputManager = InputManager.instance;
        gameManager = GameManager.instance;
        turretManager = TurretManager.instance;
    }

    private void Start()
    {
        CalculatePositionLimits();
    }

    private void Update()
    {
        Move();
        TouchMove();
        TouchZoom();
    }

    public void Move()
    {
        Vector2 mousePosition = playerInputManager.playerInput.actions["Look"].ReadValue<Vector2>();

        if (mousePosition.x <= spaceOffset.x || mousePosition.x >= Screen.width - spaceOffset.x || 
            mousePosition.y <= spaceOffset.y || mousePosition.y >= Screen.height - spaceOffset.y)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(mousePosition) - transform.position).normalized;
            Vector3 targetPosition = transform.position + direction;

            MoveHandler(targetPosition, speed);
        }
    }

    public void TouchMove()
    {
        if (Touch.activeFingers.Count == 1)
        {
            if (!turretManager.selectedVariant)
            {
                if (initialTouchPosition == null)
                {
                    initialTouchPosition = Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);
                }
                else
                {
                    switch(Touch.activeFingers[0].currentTouch.phase)
                    {
                        case TouchPhase.Moved:
                            Vector3 deltaPosition = (Vector3)initialTouchPosition - Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);

                            if (deltaPosition.sqrMagnitude > 0.4f)
                            {
                                Vector3 targetPosition = transform.position + deltaPosition;

                                MoveHandler(targetPosition, 0.075f);
                            }
                            break;

                        case TouchPhase.Ended:
                            initialTouchPosition = null;
                            break;
                    }
                }
            }
            else
            {
                Vector2 touchPosition = Touch.activeFingers[0].screenPosition;
                Vector2 test = new Vector2(Screen.width / 10.0f, Screen.height / 10.0f);

                if (touchPosition.x <= test.x || touchPosition.x >= Screen.width - test.x ||
                    touchPosition.y <= test.y || touchPosition.y >= Screen.height - test.y)
                {
                    Vector3 direction = (Camera.main.ScreenToWorldPoint(touchPosition) - transform.position).normalized;
                    Vector3 targetPosition = transform.position + direction;

                    MoveHandler(targetPosition, 0.15f);
                }
            }
        }
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();
        float scrollValue = Camera.main.orthographicSize;

        if (scroll > 0.0f)
        {
            scrollValue -= 1.0f;
        }
        else if (scroll < 0.0f)
        {
            scrollValue += 1.0f;
        }

        ZoomHandler(scrollValue);
    }

    public void TouchZoom()
    {
        if (Touch.activeFingers.Count == 2)
        {
            if (previousTouchZoomDistance == 0.0f)
            {
                previousTouchZoomDistance = (Touch.activeFingers[0].screenPosition - Touch.activeFingers[1].screenPosition).magnitude;
                zoomCenterPosition = (Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition) + Camera.main.ScreenToWorldPoint(Touch.activeFingers[1].screenPosition)) / 2.0f;
                zoomCenterPosition.z = -10.0f;
            }
            else
            {
                if (Touch.activeFingers[0].currentTouch.phase == TouchPhase.Moved && 
                    Touch.activeFingers[1].currentTouch.phase == TouchPhase.Moved)
                {
                    float currentDistance = (Touch.activeFingers[0].screenPosition - Touch.activeFingers[1].screenPosition).magnitude;
                    float deltaDistance = (previousTouchZoomDistance - currentDistance) / 10.0f;

                    if (Math.Abs(deltaDistance) > 0.4f)
                    {
                        float scrollValue = Camera.main.orthographicSize;

                        if (deltaDistance < 0.0f)
                        {
                            scrollValue -= 0.5f;
                        }
                        else if (deltaDistance > 0.0f)
                        {
                            scrollValue += 0.5f;
                        }

                        ZoomHandler(scrollValue);
                        MoveHandler(zoomCenterPosition, 0.075f);
                    }

                    previousTouchZoomDistance = currentDistance;
                }

                if (Touch.activeFingers[0].currentTouch.phase == TouchPhase.Ended || 
                    Touch.activeFingers[1].currentTouch.phase == TouchPhase.Ended)
                {
                    previousTouchZoomDistance = 0.0f;
                    initialTouchPosition = null;
                }
            }
        }
    }

    private void MoveHandler(Vector3 targetPosition, float smoothTime = 0.05f)
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVectorVelocity, smoothTime);

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    private void ZoomHandler(float zoomValue)
    {
        zoomValue = Mathf.SmoothDamp(Camera.main.orthographicSize, zoomValue, ref zoomVelocity, 0.01f);

        Camera.main.orthographicSize = Mathf.Clamp(zoomValue, minZoom, maxZoom);

        CalculatePositionLimits();

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }

    public void CalculatePositionLimits()
    {
        cameraViewSize.x = Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraViewSize.y = Camera.main.orthographicSize;

        minX = cameraViewSize.x - gameManager.mapSize.x / 2;
        maxX = gameManager.mapSize.x / 2 - cameraViewSize.x;
        minY = cameraViewSize.y - gameManager.mapSize.y / 2;
        maxY = gameManager.mapSize.y / 2 - cameraViewSize.y;
    }
}
