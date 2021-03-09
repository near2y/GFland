using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.Events;

[CustomPropertyDrawer(typeof(AnimateStateCompiler))]
public class AnimateStateCompilerDrawer : PropertyDrawer
{
    readonly Color pinkColor = new Color(1, 0.27f, 0.516f);
    readonly Color greenColor = new Color(0.27f, 1f, 0.516f);
    float m_position = 0;

    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        var nativeBgColor = GUI.backgroundColor;
        var showProp = property.FindPropertyRelative("m_showProperty");

        if (!showProp.boolValue)
        {
            var displayBtnRect = new Rect(position.x, position.y , position.width, 16);
            GUI.backgroundColor = greenColor;
            if (GUI.Button(displayBtnRect, "展示动画状态编辑器"))
            {
                showProp.boolValue = true;
            }
            m_position = 16;
        }
        else
        {
            GUI.backgroundColor = pinkColor;
            var component = property.serializedObject.targetObject as Component;
            var hpRect = new Rect(15, position.y + 20, position.xMax, 16);
            var btnRect = new Rect(15, hpRect.y + 20+20, position.xMax, 25);

            Animator anim = component.GetComponent<Animator>();
            var aniProp = property.FindPropertyRelative("m_Animator");
            aniProp.objectReferenceValue = aniProp.objectReferenceValue!=null ? aniProp.objectReferenceValue : anim;
            EditorGUI.ObjectField(hpRect, aniProp, new GUIContent("动画控制器："));

            GUI.enabled = false;
            MonoBehaviour mono = component as MonoBehaviour;
            var bind = property.FindPropertyRelative("m_Mono");
            bind.objectReferenceValue = mono;
            EditorGUI.ObjectField(new Rect(15, hpRect.y+20,position.xMax,16), bind, new GUIContent("所属脚本对象："));
            GUI.enabled = true;
            if (aniProp.objectReferenceValue == null)
            {
                if (GUI.Button(btnRect, "缺少动画控制器，点击添加"))
                {
                    aniProp.objectReferenceValue = component.gameObject.AddComponent<Animator>();
                }
            }
            else
            {
                if (GUI.Button(btnRect, "编辑动画状态的回调函数"))
                {
                    AnimateStateCompilerWindow.Open((Animator)aniProp.objectReferenceValue, component);
                }
            }




            var hideBtnRect = new Rect(15, btnRect.y + 30, position.xMax, 25);
            if (GUI.Button(hideBtnRect, "隐藏动画属性"))
            {
                showProp.boolValue = false;
            }

            m_position = 20 + 20 + 30 + 30+20;
        }

       
        EditorGUI.EndProperty();
        GUI.backgroundColor = nativeBgColor;
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return m_position;
    }
}
