using UnityEditor;
using UnityEngine;

namespace Systems.Attributes {
#if UNITY_EDITOR
    public class ShowOnlyAttribute : PropertyAttribute{ }
    [CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ShowOnlyDrawer : PropertyDrawer{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }
    }
#endif
}