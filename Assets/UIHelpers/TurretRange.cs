using UnityEngine;

public class TurretRange : MonoBehaviour
{
    private static TurretRange _instance;
    public static TurretRange instance { get { return _instance; } }

    [SerializeField]
    private GameObject cannonRangeObject;
    [SerializeField]
    private GameObject auraRangeObject;

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

        cannonLineRenderer = cannonRangeObject.GetComponent<LineRenderer>();
        cannonSpriteRenderer = cannonRangeObject.GetComponent<SpriteRenderer>();
        auraLineRenderer = auraRangeObject.GetComponent<LineRenderer>();
        auraSpriteRenderer = auraRangeObject.GetComponent<SpriteRenderer>();
    }

    public void ShowTurretRange(Vector2 position, TurretScriptableObject variant)
    {
        if(variant.needTarget)
        {
            cannonRangeObject.SetActive(true);

            transform.position = position;
            DrawCircle(cannonLineRenderer, position, variant.range + 0.5f);
            cannonSpriteRenderer.size = new Vector2(variant.range + 0.5f, variant.range + 0.5f) * 2;
        }

        if(variant.aura)
        {
            auraRangeObject.SetActive(true);

            transform.position = position;
            DrawCircle(auraLineRenderer, position, variant.auraRange / 2);
            auraSpriteRenderer.size = new Vector2(variant.auraRange, variant.auraRange);
        }
    }

    public void HideTurretRange()
    {
        cannonRangeObject.SetActive(false);
        auraRangeObject.SetActive(false);
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
