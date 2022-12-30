using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UpgradeTier", order = 4)]
public class TierScriptableObject : ScriptableObject
{
    public string Name;
    public Color color;
    public Material material;
    public float MinChance;
    public float MaxChance;
}