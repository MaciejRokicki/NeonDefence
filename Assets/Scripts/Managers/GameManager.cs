using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    [SerializeField]
    private SpriteRenderer background;
    public Vector2 mapSize = new Vector2(50.0f, 30.0f);

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        background.size = mapSize;
    }
}
