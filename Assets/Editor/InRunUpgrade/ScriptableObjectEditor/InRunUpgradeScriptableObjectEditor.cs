using UnityEditor;
using UnityEngine;
using Assets.Scripts.Game.Upgrades.InRunUpgrades;
using Assets.Editor.InRunUpgrade.ScriptableObjectEditor.Strategies;

namespace Assets.Editor.InRunUpgrade.ScriptableObjectEditor
{
    [CustomEditor(typeof(InRunUpgradeScriptableObject), true)]
    public class InRunUpgradeScriptableObjectEditor : UnityEditor.Editor
    {
        private InRunUpgradesScriptableObjectEditorStrategy inRunUpgradesScriptableObjectEditorStrategy;

        private TierScriptableObject tier;
        private bool unique;
        private string description;

        private void OnEnable()
        {
            tier = serializedObject.FindProperty("Tier").objectReferenceValue as TierScriptableObject;
            unique = serializedObject.FindProperty("Unique").boolValue;
            description = serializedObject.FindProperty("Description").stringValue;

            if(serializedObject.targetObject is InRunGameUpgradeScriptableObject)
            {
                inRunUpgradesScriptableObjectEditorStrategy = new InRunGameUpgradesScriptableObjectEditorStrategy(serializedObject);
            }
            else if (serializedObject.targetObject is InRunTurretUpgradeScriptableObject)
            {
                inRunUpgradesScriptableObjectEditorStrategy = new InRunTurretUpgradesScriptableObjectEditorStrategy(serializedObject);
            }

            inRunUpgradesScriptableObjectEditorStrategy.OnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            InRunUpgradeScriptableObjectEditorGUI.InRunUpgradeSection(
                ref tier,
                ref unique,
                ref description);

            inRunUpgradesScriptableObjectEditorStrategy.OnInspectorGUI();

            SaveProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void SaveProperties()
        {
            serializedObject.FindProperty("Tier").objectReferenceValue = tier;
            serializedObject.FindProperty("Unique").boolValue = unique;
            serializedObject.FindProperty("Description").stringValue = description;

            inRunUpgradesScriptableObjectEditorStrategy.SaveProperties();
        }
    }
}
