using UnityEditor;

namespace Assets.Editor.Game.InRunUpgrade.ScriptableObjectEditor.Strategies
{
    public class InRunGameUpgradesScriptableObjectEditorStrategy : InRunUpgradesScriptableObjectEditorStrategy
    {
        private float health;
        private bool healthIsPercentage;
        private float maxHealth;
        private bool maxHealthIsPercentage;
        private bool increaseHealthToo;
        private int neonBlocks;

        public InRunGameUpgradesScriptableObjectEditorStrategy(SerializedObject serializedObject) : base(serializedObject) { }

        public override void OnEnable()
        {
            health = serializedObject.FindProperty("Health").floatValue;
            healthIsPercentage = serializedObject.FindProperty("HealthIsPercentage").boolValue;
            maxHealth = serializedObject.FindProperty("MaxHealth").floatValue;
            maxHealthIsPercentage = serializedObject.FindProperty("MaxHealthIsPercentage").boolValue;
            increaseHealthToo = serializedObject.FindProperty("IncreaseHealthToo").boolValue;
            neonBlocks = serializedObject.FindProperty("NeonBlocks").intValue;
        }

        public override void OnInspectorGUI()
        {
            InRunUpgradeScriptableObjectEditorGUI.GameUpgradeSection(
                ref health,
                ref healthIsPercentage,
                ref maxHealth,
                ref maxHealthIsPercentage,
                ref increaseHealthToo,
                ref neonBlocks);
        }

        public override void SaveProperties()
        {
            serializedObject.FindProperty("Health").floatValue = health;
            serializedObject.FindProperty("HealthIsPercentage").boolValue = healthIsPercentage;
            serializedObject.FindProperty("MaxHealth").floatValue = maxHealth;
            serializedObject.FindProperty("MaxHealthIsPercentage").boolValue = maxHealthIsPercentage;
            serializedObject.FindProperty("IncreaseHealthToo").boolValue = increaseHealthToo;
            serializedObject.FindProperty("NeonBlocks").intValue = neonBlocks;
        }
    }
}
