using System;
using UnityEngine;
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
    }

    public override void Move()
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
                    switch (Touch.activeFingers[0].currentTouch.phase)
                    {
                        case TouchPhase.Moved:
                            Vector3 deltaPosition = (Vector3)initialTouchPosition - Camera.main.ScreenToWorldPoint(Touch.activeFingers[0].screenPosition);

                            if (deltaPosition.sqrMagnitude > 0.4f)
                            {
                                Vector3 targetPosition = cameraTransform.position + deltaPosition;

                                cameraController.MoveHandler(targetPosition, 0.075f);
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
                    Vector3 direction = (Camera.main.ScreenToWorldPoint(touchPosition) - cameraTransform.position).normalized;
                    Vector3 targetPosition = cameraTransform.position + direction;

                    cameraController.MoveHandler(targetPosition, 0.15f);
                }
            }
        }
    }

    public override void Zoom()
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

                        cameraController.ZoomHandler(scrollValue);
                        cameraController.MoveHandler(zoomCenterPosition, 0.075f);
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
}
