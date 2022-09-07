using UnityEditor;

[CustomEditor(typeof(Turret))]
public class TurretEditor : Editor
{
    SerializedProperty _data;

    SerializedProperty cannon;
    SerializedProperty missile;
    SerializedProperty aura;

    SerializedProperty turretSpriteRenderer;
    SerializedProperty cannonSpriteRenderer;
    SerializedProperty missileSpriteRenderer;
    SerializedProperty auraSpriteRenderer;

    private void OnEnable()
    {
        _data = serializedObject.FindProperty("data");

        cannon = serializedObject.FindProperty("cannon");
        missile = serializedObject.FindProperty("missile");
        aura = serializedObject.FindProperty("aura");

        turretSpriteRenderer = serializedObject.FindProperty("turretSpriteRenderer");
        cannonSpriteRenderer = serializedObject.FindProperty("cannonSpriteRenderer");
        missileSpriteRenderer = serializedObject.FindProperty("missileSpriteRenderer");
        auraSpriteRenderer = serializedObject.FindProperty("auraSpriteRenderer");
    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();

        EditorGUILayout.PropertyField(_data);

        if(_data != null)
        {
            SerializedObject data = new SerializedObject(_data.objectReferenceValue);

            if (data.FindProperty("_needTarget").boolValue)
            {
                EditorGUILayout.PropertyField(cannon); //TODO: usunac
                EditorGUILayout.PropertyField(missile); //TODO: usunac
            }

            if (data.FindProperty("_aura").boolValue)
            {
                EditorGUILayout.PropertyField(aura); //TODO: usunac
            }

            EditorGUILayout.PropertyField(turretSpriteRenderer); //TODO: usunac

            if (data.FindProperty("_needTarget").boolValue)
            {
                EditorGUILayout.PropertyField(cannonSpriteRenderer);  //TODO: usunac
                EditorGUILayout.PropertyField(missileSpriteRenderer);  //TODO: usunac
            }

            if (data.FindProperty("_aura").boolValue)
            {
                EditorGUILayout.PropertyField(auraSpriteRenderer);  //TODO: usunac
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}