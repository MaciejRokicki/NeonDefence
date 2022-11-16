using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gameManager;

    private CameraControllerStrategy cameraControllerStrategy;

    [SerializeField]
    private Vector2 cameraViewSize;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector3 moveVectorVelocity = Vector3.zero;

    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float speed = 0.05f;
    [SerializeField]
    private Vector2Int spaceOffset = new Vector2Int(20, 10);

    [SerializeField]
    private float minZoom = 2.5f;
    [SerializeField]
    private float maxZoom = 10.0f;

    private float zoomVelocity = 0.0f;

    private void Awake()
    {
        gameManager = GameManager.instance;

        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            cameraControllerStrategy = new DesktopCameraControllerStrategy(transform, this, spaceOffset, speed);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            cameraControllerStrategy = new HandheldCameraControllerStrategy(transform, this);
        }
    }

    private void Start()
    {
        CalculatePositionLimits();
    }

    private void Update()
    {
        cameraControllerStrategy.Move();
        cameraControllerStrategy.Zoom();
    }

    public void MoveHandler(Vector3 targetPosition, float smoothTime = 0.05f)
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVectorVelocity, smoothTime);

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    public void ZoomHandler(float zoomValue)
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
