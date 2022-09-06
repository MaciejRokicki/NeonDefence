using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistics")]
    public float health;
    public float movementSpeed;
    public float damage;
    [Header("Appearance")]
    public Sprite sprite;
    public Material material;
    [Header("Light options")]
    public bool lightSource;
    public float lightSourceInnerRadius;
    public float lightSourceOuterRadius;
}
