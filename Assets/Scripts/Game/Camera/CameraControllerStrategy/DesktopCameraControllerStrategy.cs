using UnityEngine;

public class DesktopCameraControllerStrategy : CameraControllerStrategy
{
    private InputManager playerInputManager;
    private Vector2Int spaceOffset;
    private float speed;

    public DesktopCameraControllerStrategy(Transform cameraTransform, CameraController cameraController, Vector2Int spaceOffset, float speed) : base(cameraTransform, cameraController)
    {
        playerInputManager = InputManager.instance;
        this.spaceOffset = spaceOffset;
        this.speed = speed;
    }

    public override void Move()
    {
        Vector2 mousePosition = playerInputManager.playerInput.actions["Look"].ReadValue<Vector2>();

        if (mousePosition.x <= spaceOffset.x || mousePosition.x >= Screen.width - spaceOffset.x ||
            mousePosition.y <= spaceOffset.y || mousePosition.y >= Screen.height - spaceOffset.y)
        {
            Vector3 direction = (Camera.main.ScreenToWorldPoint(mousePosition) - cameraTransform.position).normalized;
            Vector3 targetPosition = cameraTransform.position + direction;

            cameraController.MoveHandler(targetPosition, speed);
        }
    }

    public override void Zoom()
    {
        float scroll = playerInputManager.playerInput.actions["Zoom"].ReadValue<float>();
        float scrollValue = Camera.main.orthographicSize;

        if (scroll > 0.0f)
        {
            scrollValue -= 1.0f;
        }
        else if (scroll < 0.0f)
        {
            scrollValue += 1.0f;
        }

        cameraController.ZoomHandler(scrollValue);
    }
}

