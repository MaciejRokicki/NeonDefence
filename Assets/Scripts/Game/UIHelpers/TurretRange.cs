using UnityEngine;

public class TurretRange : MonoBehaviour
{
    private static TurretRange _instance;
    public static TurretRange instance { get { return _instance; } }

    [SerializeField]
    private GameObject cannonRange;
    [SerializeField]
    private GameObject auraRange;

    private LineRenderer cannonLineRenderer;
    private SpriteRenderer cannonSpriteRenderer;
    private LineRenderer auraLineRenderer;
    private SpriteRenderer auraSpriteRenderer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        cannonLineRenderer = cannonRange.GetComponent<LineRenderer>();
        cannonSpriteRenderer = cannonRange.GetComponent<SpriteRenderer>();
        auraLineRenderer = auraRange.GetComponent<LineRenderer>();
        auraSpriteRenderer = auraRange.GetComponent<SpriteRenderer>();
    }

    public void ShowCannonRange(Vector2 position, float range)
    {
        cannonRange.SetActive(true);

        transform.position = position;
        DrawCircle(cannonLineRenderer, position, range + 0.5f);
        cannonSpriteRenderer.size = new Vector2(range + 0.5f, range + 0.5f) * 2;
    }

    public void ShowAuraRange(Vector2 position, float range)
    {
        auraRange.SetActive(true);

        transform.position = position;
        DrawCircle(auraLineRenderer, position, range / 2);
        auraSpriteRenderer.size = new Vector2(range, range);
    }

    public void HideTurretRange()
    {
        cannonRange.SetActive(false);
        auraRange.SetActive(false);
    }

    private void DrawCircle(LineRenderer lineRenderer, Vector3 origin, float radius)
    {
        int iterations = 100;
        lineRenderer.positionCount = iterations;

        for (int i = 0; i < iterations; i++)
        {
            float progress = (float)i / (iterations - 2);
            float rad = progress * 2.0f * Mathf.PI;

            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            Vector3 pos = origin + new Vector3(x, y, 0.0f);

            lineRenderer.SetPosition(i, pos);
        }
    }
}
