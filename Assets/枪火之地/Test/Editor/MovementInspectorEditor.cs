using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEditorInternal;

[CustomEditor(typeof(Movement))]
public class MovementInspectorEditor : Editor
{
    Movement m_Movement = null;


    private void OnEnable()
    {
        m_Movement = (Movement)target;
    }

    public override void OnInspectorGUI()
    {
        m_Movement.m_type = (MovementMoveType)EditorGUILayout.EnumPopup(m_Movement.m_type);
        GUILayout.Space(5);
        switch (m_Movement.m_type)
        {
            case MovementMoveType.CharactorCtrol:
                SetCharactorCtrolModel();
                break;
            case MovementMoveType.Navmesh:
                SetNavmeshModel();
                break;
            case MovementMoveType.Rigidbody:
                GUILayout.Label("当前为刚体移动");
                m_Movement.moveSpeed = EditorGUILayout.FloatField("移动速度", m_Movement.moveSpeed);
                break;
        }
        GUILayout.Space(5);

        serializedObject.Update();
        GUIContent callBackLabel = new GUIContent();
        callBackLabel.text = "移动完成的回调函数";
        EditorGUILayout.PropertyField(serializedObject.FindProperty("completeMovement"),callBackLabel,true);
        serializedObject.ApplyModifiedProperties();


        if (GUILayout.Button("移除移动模块", GUILayout.Height(25)))
        {
            RemoveCCR();
            RemoveAgent();
            DestroyImmediate(m_Movement);
        }
    }



    private void SetNavmeshModel()
    {
        RemoveCCR();
        m_Movement.m_agent = m_Movement.gameObject.GetComponent<NavMeshAgent>();
        if (m_Movement.m_agent == null)
        {
            m_Movement.m_AddNavmeshAgent= true;
            m_Movement.m_agent= m_Movement.gameObject.AddComponent<NavMeshAgent>();
        }
        GUILayout.Label("当前为导航移动");
        GUILayout.BeginVertical();
        m_Movement.moveSpeed = EditorGUILayout.FloatField("移动速度", m_Movement.moveSpeed);
        m_Movement.angelSpeed = EditorGUILayout.FloatField("转向速度", m_Movement.angelSpeed);
        GUILayout.EndVertical();
    }


    private void SetCharactorCtrolModel()
    {
        RemoveAgent();
        m_Movement.m_ccr = m_Movement.gameObject.GetComponent<CharacterController>();
        if (m_Movement.m_ccr == null)
        {
            m_Movement.m_AddCharatorControl = true;
            m_Movement.m_ccr = m_Movement.gameObject.AddComponent<CharacterController>();
        }
        GUILayout.Label("当前为角色控制器移动");
        GUILayout.BeginVertical();
        m_Movement.m_ctrlType = (MovementCtrlType)EditorGUILayout.EnumPopup(m_Movement.m_ctrlType);
        switch (m_Movement.m_ctrlType)
        {
            case MovementCtrlType.Joystick:
                GUILayout.BeginVertical();
                GUILayout.Label("摇杆控制：");
                EditorGUILayout.ObjectField("摇杆UI：",m_Movement.m_Joystick,typeof(Joystick),true);
                m_Movement.m_lookType = (MovementLookType)EditorGUILayout.EnumFlagsField("看向方式：", m_Movement.m_lookType);
                m_Movement.m_LookLerp = EditorGUILayout.Slider("看向目标的插值率",m_Movement.m_LookLerp,0,1);
                m_Movement.m_lookAxisType = (MovementLookAxisType)EditorGUILayout.EnumFlagsField("可以转动的轴：", m_Movement.m_lookAxisType);
                if (m_Movement.m_lookType.HasFlag(MovementLookType.Input))
                {
                    m_Movement.m_inputLayer = EditorGUILayout.LayerField("射线检测层", m_Movement.m_inputLayer);
                    m_Movement.m_inputLength = EditorGUILayout.FloatField("射线检测长度", m_Movement.m_inputLength);
                }
                m_Movement.moveSpeed = EditorGUILayout.FloatField("移动速度", m_Movement.moveSpeed);
                
                GUILayout.EndVertical();
                break;
            case MovementCtrlType.Keyboard:
                
                break;
        }


        GUILayout.EndVertical();
    }

    private void RemoveCCR()
    {
        if (m_Movement.m_AddCharatorControl)
        {
            m_Movement.m_AddCharatorControl = false;
            DestroyImmediate(m_Movement.m_ccr);
        }
    }

    private void RemoveAgent()
    {
        if (m_Movement.m_AddNavmeshAgent)
        {
            m_Movement.m_AddNavmeshAgent = false;
            DestroyImmediate(m_Movement.m_agent);
        }
    }
}
