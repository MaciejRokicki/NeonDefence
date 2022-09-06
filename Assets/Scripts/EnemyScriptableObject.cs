using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _damage;
    [Header("Appearance")]
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private Material _material;
    [Header("Spawn options")]
    [SerializeField]
    private int _minWave;
    [Header("Light options")]
    [SerializeField]
    private bool _lightSource;
    [SerializeField]
    private float _lightSourceInnerRadius;
    [SerializeField]
    private float _lightSourceOuterRadius;

    // Internal values
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float movementSpeed;
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public Sprite sprite;
    [HideInInspector]
    public Material material;

    [HideInInspector]
    public int minWave;

    [HideInInspector]
    public bool lightSource;
    [HideInInspector]
    public float lightSourceInnerRadius;
    [HideInInspector]
    public float lightSourceOuterRadius;

    private void OnEnable()
    {
        health = _health;
        movementSpeed = _movementSpeed;
        damage = _damage;
        sprite = _sprite;
        material = _material;
        minWave = _minWave;
        lightSource = _lightSource;
        lightSourceInnerRadius = _lightSourceInnerRadius;
        lightSourceOuterRadius = _lightSourceOuterRadius;
    }
}
