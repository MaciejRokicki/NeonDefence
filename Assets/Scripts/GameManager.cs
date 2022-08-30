using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer background;

    public Vector2 MapSize = new Vector2(50.0f, 30.0f);

    private void Start()
    {
        background.size = MapSize;
    }
}
