using UnityEngine;

public class EnemySpawnerPathMarker : MonoBehaviour
{
    private Vector3 basePosition;
    private bool visable;

    private void Start()
    {
        basePosition = transform.position;
    }

    private readonly Plane[] planes = new Plane[6];

    private void Update()
    {
        visable = Camera.main.IsPointInFrustum(basePosition);

        if (!visable)
        {
            Vector3 origin = Camera.main.transform.position;
            Vector3 direction = (basePosition - Camera.main.transform.position).normalized;

            Ray ray = new Ray(origin, direction);

            float currentMinDistance = float.MaxValue;
            Vector3 hitPoint = Vector3.zero;

            GeometryUtility.CalculateFrustumPlanes(Camera.main, planes);

            for (var i = 0; i < 4; i++)
            {
                if (planes[i].Raycast(ray, out var distance))
                {
                    if (distance < currentMinDistance)
                    {
                        hitPoint = ray.GetPoint(distance - 1.0f);
                        currentMinDistance = distance;
                    }
                }
            }

            transform.position = hitPoint;
        }
        else
        {
            transform.position = basePosition;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
}
