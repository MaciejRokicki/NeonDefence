using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform background;

    public Vector2 MapSize = new Vector2(50.0f, 30.0f);

    private void Start()
    {
        background.localScale = new Vector3(MapSize.x, MapSize.y, 1.0f);
    }
}
