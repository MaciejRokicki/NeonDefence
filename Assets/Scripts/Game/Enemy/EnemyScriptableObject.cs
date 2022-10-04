using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    [SerializeField]
    private float _health;
    public float health;
    [SerializeField]
    private float _movementSpeed;
    public float movementSpeed;
    [SerializeField]
    private float _damage;
    public float damage;

    [Header("Appearance")]
    public Sprite sprite;
    public Material material;

    [Header("Spawn options")]
    public int minWave;

    private void OnEnable()
    {
        health = _health;
        movementSpeed = _movementSpeed;
        damage = _damage;
    }
}