using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System;
using System.Reflection;

[Serializable]
public class AnimateStateCompilerWindow : EditorWindow
{
    Animator m_animator;
    int m_animatorLayerIndex = 0;
    int m_animatorStateIndex = -1;
    Vector2 m_begin;
    Component m_Component;
    Vector2 m_FunctionNameBegin;
    bool m_showFunctionName = false;


    public static void Open(Animator ani,Component component)
    {
        AnimateStateCompilerWindow window = GetWindow<AnimateStateCompilerWindow>(true,"动画状态回调编辑器",true);
        window.minSize = new Vector2(800f, 600f);
        window.m_animator = ani;
        window.m_begin = Vector2.zero;
        window.m_FunctionNameBegin = Vector2.zero;
        window.m_animatorLayerIndex = 0;
        window.m_animatorLayerIndex = -1;
        window.m_Component = component;
        window.m_showFunctionName = false;
    }

    private void OnGUI()
    {
        m_animator.Rebind();

        EditorGUILayout.BeginHorizontal();

        ChooseState();

        SetState();

        EditorGUILayout.EndHorizontal();

    }


    void ChooseState()
    {
        m_begin = EditorGUILayout.BeginScrollView(m_begin, GUILayout.Width(position.width * 0.35f), GUILayout.Height(position.height - 15));
        {

            GUILayout.Label("所有层级");
            for (int i = 0; i < m_animator.layerCount; i++)
            {
                GUI.enabled = m_animatorLayerIndex != i;
                if (GUILayout.Button(m_animator.GetLayerName(i)))
                {
                    m_animatorLayerIndex = i;
                }
                GUI.enabled = true;
            }
            GUILayout.Space(15);
            if (m_animatorLayerIndex >= 0)
            {
                GUILayout.Label(m_animator.GetLayerName(m_animatorLayerIndex) + " 层级所有状态");
                ChildAnimatorState[] ams =
                    ((AnimatorController)m_animator.runtimeAnimatorController).layers[m_animatorLayerIndex].stateMachine.states;

                for (int i = 0; i < ams.Length; i++)
                {
                    if (GUILayout.Button(ams[i].state.name))
                    {
                        m_animatorStateIndex = i;
                    }
                }
            }
        }
        EditorGUILayout.EndScrollView();

    }

    void SetState()
    {
        if (m_animatorStateIndex >= 0)
        {
            EditorGUILayout.BeginVertical();
            ChildAnimatorState[] ams =
                 ((AnimatorController)m_animator.runtimeAnimatorController).layers[m_animatorLayerIndex].stateMachine.states;
            var state = ams[m_animatorStateIndex].state;
            GUILayout.Label(state.name);

            if (state.behaviours.Length == 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("当前状态缺少行为脚本");
                if (GUILayout.Button("添加行为脚本"))
                {
                    BehaviourBase b = state.AddStateMachineBehaviour<BehaviourBase>();
                    b.m_EnterCallBackName = new string[0];
                    b.m_UpdateCallBackName = new string[0];
                    b.m_ExitCallBackName = new string[0];
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("设置当前状态行为脚本");
                GUI.enabled = false;
                if (GUILayout.Button("移除当前状态行为脚本TODO"))
                {
                    //TODO
                }
                GUI.enabled = true;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();


                var behaviour = state.behaviours[0] as BehaviourBase;
                SetCallBacksName("进入状态调用函数数量", ref behaviour.m_EnterCallBackName);
                SetCallBacksName("状态的每帧回调函数名称", ref behaviour.m_UpdateCallBackName);
                SetCallBacksName("状态的离开回调函数名称", ref behaviour.m_ExitCallBackName);

                SetDisplayAllMethodsName();

            }
            EditorGUILayout.EndVertical();
        }
    }

    void SetDisplayAllMethodsName()
    {
        if (m_Component != null)
        {
            if (m_showFunctionName)
            {
                if (GUILayout.Button("隐藏当前脚本所有函数名称"))
                {
                    m_showFunctionName = false;
                }
                MethodInfo[] methods = m_Component.GetType().GetMethods();
                int row = Mathf.FloorToInt(methods.Length / 3);
                int index = 0;
                m_FunctionNameBegin = EditorGUILayout.BeginScrollView(m_FunctionNameBegin);
                for (int i = 0; i < row; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < 3; j++)
                    {
                        EditorGUILayout.BeginVertical();
                        GUILayout.TextField(methods[index].Name);
                        EditorGUILayout.EndVertical();
                        index++;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }
            else
            {
                if (GUILayout.Button("显示当前脚本所有函数名称"))
                {
                    m_showFunctionName = true;
                }
            }
        }
    }

    void SetCallBacksName(string title, ref string[] names)
    {
        if (names == null) return;
        EditorGUILayout.BeginHorizontal();
        int length = EditorGUILayout.IntField(title, names.Length);
        if (length != names.Length)
        {
            names = new string[length];
        }
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = GUILayout.TextField(names[i]);

        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }


}

