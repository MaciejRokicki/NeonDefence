using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatRangeProperty))]
public class FloatRangePropertyDrawer : PropertyDrawer
{
    private readonly float labelWidth = 30.0f;
    private readonly float fieldWidth = 75.0f;
    private readonly float marginWidth = 5.0f;

    private readonly float minFieldPosition;
    private readonly float maxLabelPosition;
    private readonly float maxFieldPosition;

    public FloatRangePropertyDrawer()
    {
        minFieldPosition = labelWidth;
        maxLabelPosition = minFieldPosition + fieldWidth + marginWidth;
        maxFieldPosition = maxLabelPosition + labelWidth + marginWidth;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect minLabel = new Rect(position.x, position.y, labelWidth, position.height);
        Rect minField = new Rect(position.x + minFieldPosition, position.y, fieldWidth, position.height);
        Rect maxLabel = new Rect(position.x + maxLabelPosition, position.y, labelWidth, position.height);
        Rect maxField = new Rect(position.x + maxFieldPosition, position.y, fieldWidth, position.height);

        EditorGUI.LabelField(minLabel, "Min");
        EditorGUI.PropertyField(minField, property.FindPropertyRelative("Min"), GUIContent.none);
        EditorGUI.LabelField(maxLabel, "Max");
        EditorGUI.PropertyField(maxField, property.FindPropertyRelative("Max"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}