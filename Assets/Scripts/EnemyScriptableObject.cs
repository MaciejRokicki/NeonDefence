using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public float health;
    public float movementSpeed;
    public float dmg;
    public Sprite sprite;
    public Material material;
    public bool lightSource;
    public float lightSourceInnerRadius;
    public float lightSourceOuterRadius;
}
