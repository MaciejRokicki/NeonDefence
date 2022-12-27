using UnityEditor;

namespace Assets.Extensions
{
    public static class EditorGUILayoutExtension
    {
        public static float FloatField(string label, float value, float min)
        {
            value = value < min ? min : value;

            return UnityEditor.EditorGUILayout.FloatField(label, value);
        }

        public static float FloatField(string label, float value, float min, float max)
        {
            value = value < min ? min : value;
            value = value > max ? max : value;

            return UnityEditor.EditorGUILayout.FloatField(label, value);
        }
    }
}
