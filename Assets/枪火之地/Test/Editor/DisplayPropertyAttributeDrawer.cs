using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomPropertyDrawer(typeof(DisplayPropertyAttribute))]
public class DisplayPropertyAttributeDrawer: PropertyDrawer
{

    float m_Height = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        DisplayPropertyAttribute display = (DisplayPropertyAttribute)attribute;
        string propertyName = display.m_PropertyName;
        if(property.serializedObject.FindProperty(propertyName).intValue == display.m_Equal)
        {
            EditorGUI.PropertyField(position, property);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

}

