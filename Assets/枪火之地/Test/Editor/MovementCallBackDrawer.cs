using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer (typeof(MovementCallBack))]
public class MovementCallBackDrawer : PropertyDrawer
{
    private MovementCallBack m_MovementCallBack;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 50;
            position.height = EditorGUIUtility.singleLineHeight;
            var halfWidth = position.width * 0.5f;
            var scriptRect = new Rect(position)
            {
                width = 120,
            };

            var nameRect = new Rect(position)
            {
                width = position.width - 120,
                x = position.x + 120
            };
            var scriptProperty = property.FindPropertyRelative("script");
            var nameProperty = property.FindPropertyRelative("scriptName");

            scriptProperty.objectReferenceValue =
            EditorGUI.ObjectField(scriptRect,
            scriptProperty.objectReferenceValue, typeof(Component));

            nameProperty.stringValue =
              EditorGUI.TextField(nameRect,
                "name", nameProperty.stringValue);
        }
    }
}
