using UnityEngine;

public abstract class CameraControllerStrategy
{
    protected Transform cameraTransform;
    protected CameraController cameraController;

    public CameraControllerStrategy(Transform cameraTransform, CameraController cameraController)
    {
        this.cameraTransform = cameraTransform;
        this.cameraController = cameraController;
    }

    public abstract void Move();
    public abstract void Zoom();
}
