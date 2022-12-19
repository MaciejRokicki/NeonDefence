using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance { get { return _instance; } }

    public bool blockGameInteraction = false; 

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
    }
}
