using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MissileScriptableObject", order = 2)]
public class MissileScriptableObject : ScriptableObject
{
    [SerializeField]
    private Vector2 _size;
    [HideInInspector]
    public Vector2 size;
    [SerializeField]
    private Sprite _sprite;
    [HideInInspector]
    public Sprite sprite;
    [SerializeField]
    private Material _material;
    [HideInInspector]
    public Material material;

    private void OnEnable()
    {
        size = _size;
        sprite = _sprite;
        material = _material;
    }
}
