using UnityEditor;

[CustomEditor(typeof(Turret))]
public class TurretEditor : Editor
{
    SerializedProperty _data;
    SerializedProperty missilePrefab;

    SerializedProperty cannon;
    SerializedProperty aura;

    SerializedProperty turretSpriteRenderer;
    SerializedProperty cannonSpriteRenderer;
    SerializedProperty auraSpriteRenderer;

    private void OnEnable()
    {
        _data = serializedObject.FindProperty("data");
        missilePrefab = serializedObject.FindProperty("missilePrefab");

        cannon = serializedObject.FindProperty("cannon");
        aura = serializedObject.FindProperty("aura");

        turretSpriteRenderer = serializedObject.FindProperty("turretSpriteRenderer");
        cannonSpriteRenderer = serializedObject.FindProperty("cannonSpriteRenderer");
        auraSpriteRenderer = serializedObject.FindProperty("auraSpriteRenderer");
    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();

        EditorGUILayout.PropertyField(_data);

        if (_data != null)
        {
            SerializedObject data = new SerializedObject(_data.objectReferenceValue);

            if (data.FindProperty("_needTarget").boolValue)
            {
                EditorGUILayout.PropertyField(missilePrefab);
                EditorGUILayout.PropertyField(cannon); //TODO: usunac
            }

            if (data.FindProperty("_aura").boolValue)
            {
                EditorGUILayout.PropertyField(aura); //TODO: usunac
            }

            EditorGUILayout.PropertyField(turretSpriteRenderer); //TODO: usunac

            if (data.FindProperty("_needTarget").boolValue)
            {
                EditorGUILayout.PropertyField(cannonSpriteRenderer);  //TODO: usunac
            }

            if (data.FindProperty("_aura").boolValue)
            {
                EditorGUILayout.PropertyField(auraSpriteRenderer);  //TODO: usunac
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}