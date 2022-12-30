using UnityEditor;

namespace Assets.Editor.Game.InRunUpgrade.ScriptableObjectEditor.Strategies
{
    public abstract class InRunUpgradesScriptableObjectEditorStrategy
    {
        protected SerializedObject serializedObject;

        public InRunUpgradesScriptableObjectEditorStrategy(SerializedObject serializedObject)
        {
            this.serializedObject = serializedObject;
        }

        public abstract void OnEnable();
        public abstract void OnInspectorGUI();
        public abstract void SaveProperties();
    }
}
