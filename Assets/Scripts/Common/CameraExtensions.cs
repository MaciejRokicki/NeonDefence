using UnityEngine;

public static class CameraExtensions
{
    public static bool IsPointInFrustum(this Camera camera, Vector3 worldPoint)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(worldPoint);

        return screenPos.z >= camera.nearClipPlane
               && screenPos.z <= camera.farClipPlane
               && screenPos.x >= 0
               && screenPos.x <= Screen.width
               && screenPos.y >= 0
               && screenPos.y <= Screen.height;
    }
}
