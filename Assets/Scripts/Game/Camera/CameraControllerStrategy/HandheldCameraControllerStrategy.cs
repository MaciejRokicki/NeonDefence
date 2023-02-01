using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class HandheldCameraControllerStrategy : CameraControllerStrategy
{
    private Vector3? initialTouchPosition = null;
    private TurretManager turretManager;

    private float previousTouchZoomDistance = 0.0f;
    private Vector3 zoomCenterPosition;

    public HandheldCameraControllerStrategy(Transform cameraTransform, CameraController cameraController) : base(cameraTransform, cameraController)
    {
        turretManager = TurretManager.instance;

        EnhancedTouchSupport.Enable();
    }

    public override void Move()
    {
        if (Touch.activeFingers.Count == 1)
        {
            Touch touch = Touch.activeFingers[0].currentTouch;

            if (initialTouchPosition == null)
            {
                initialTouchPosition = Camera.main.ScreenToWorldPoint(touch.startScreenPosition);
            }
            else
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!turretManager.selectedVariant)
                    {
                        Vector3 deltaPosition = initialTouchPosition.Value - Camera.main.ScreenToWorldPoint(touch.screenPosition);

                        if (deltaPosition.sqrMagnitude > 0.4f)
                        {
                            Vector3 targetPosition = cameraTransform.position + deltaPosition;

                            cameraController.MoveHandler(targetPosition, 0.09f);
                        }
                    }
                    else
                    {
                        Vector2 touchPosition = touch.screenPosition;
                        Vector2 screenOffset = new Vector2(Screen.width / 10.0f, Screen.height / 10.0f);

                        if (touchPosition.x <= screenOffset.x || touchPosition.x >= Screen.width - screenOffset.x ||
                            touchPosition.y <= screenOffset.y || touchPosition.y >= Screen.height - screenOffset.y)
                        {
                            Vector3 direction = (Camera.main.ScreenToWorldPoint(touchPosition) - cameraTransform.position).normalized;
                            Vector3 targetPosition = cameraTransform.position + direction;

                            cameraController.MoveHandler(targetPosition, 0.15f);
                        }
                    }
                }
            }
        }

        if (Touch.activeFingers.Count == 0)
        {
            previousTouchZoomDistance = 0.0f;
            initialTouchPosition = null;
        }
    }

    public override void Zoom()
    {
        if (Touch.activeFingers.Count == 2)
        {
            Touch touchA = Touch.activeFingers[0].currentTouch;
            Touch touchB = Touch.activeFingers[1].currentTouch;

            if (previousTouchZoomDistance == 0.0f)
            {
                previousTouchZoomDistance = (touchA.screenPosition - touchB.screenPosition).magnitude;
                zoomCenterPosition = (Camera.main.ScreenToWorldPoint(touchA.screenPosition) + Camera.main.ScreenToWorldPoint(touchB.screenPosition)) / 2.0f;
                zoomCenterPosition.z = -10.0f;
            }
            else
            {
                float currentDistance = (touchA.screenPosition - touchB.screenPosition).magnitude;
                float deltaDistance = (previousTouchZoomDistance - currentDistance) / 10.0f;

                if (Math.Abs(deltaDistance) > 0.4f)
                {
                    float scrollValue = Camera.main.orthographicSize;

                    if (deltaDistance < 0.0f)
                    {
                        scrollValue -= 0.4f;
                    }
                    else if (deltaDistance > 0.0f)
                    {
                        scrollValue += 0.4f;
                    }

                    cameraController.ZoomHandler(scrollValue);
                    cameraController.MoveHandler(zoomCenterPosition, 0.2f);
                }

                previousTouchZoomDistance = currentDistance;
            }
        }
    }
}
