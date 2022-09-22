using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private InputManager playerInputManager;
    private GameManager gameManager;
    private UIManager uiManager;

    private bool isLeftSideMenu = true;

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

    private void Awake()
    {
        playerInputManager = InputManager.instance;
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    private void Start()
    {
        CalculatePositionLimits();
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (!isLeftSideMenu && transform.position.x >= 0.0f)
        {
            uiManager.ToggleMenuSide();
            isLeftSideMenu = true;
        }
        else if(isLeftSideMenu && transform.position.x < 0.0f)
        {
            uiManager.ToggleMenuSide();
            isLeftSideMenu = false; 
        }
    }

    public void Move()
    {
        Vector2 mousePosition = playerInputManager.playerInput.actions["Look"].ReadValue<Vector2>();
        Vector3 pos = transform.position;

        if (mousePosition.x <= spaceOffset.x)
        {
            pos.x -= 1.0f;
        }

        if (mousePosition.x >= Screen.width - spaceOffset.x)
        {

            pos.x += 1.0f;
        }

        if (mousePosition.y <= spaceOffset.y)
        {
            pos.y -= 1.0f;
        }

        if (mousePosition.y >= Screen.height - spaceOffset.y)
        {
            pos.y += 1.0f;
        }

        Vector3 newPosition;

        newPosition.x = Mathf.SmoothDamp(transform.position.x, pos.x, ref xVelocity, speed);
        newPosition.y = Mathf.SmoothDamp(transform.position.y, pos.y, ref yVelocity, speed);
        newPosition.z = -10.0f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }

    public void Zoom(InputAction.CallbackContext context)
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

    private void CalculatePositionLimits()
    {
        cameraViewSize.x = Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraViewSize.y = Camera.main.orthographicSize;

        minX = cameraViewSize.x - gameManager.mapSize.x / 2;
        maxX = gameManager.mapSize.x / 2 - cameraViewSize.x;
        minY = cameraViewSize.y - gameManager.mapSize.y / 2;
        maxY = gameManager.mapSize.y / 2 - cameraViewSize.y;
    }
}
