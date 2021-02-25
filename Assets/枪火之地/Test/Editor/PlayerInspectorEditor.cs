using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityScript.Lang;
using UnityEngine.AI;

[CustomEditor(typeof(PlayerTest))]
public class PlayerInspectorEditor :Editor
{
    public PlayerTest PlayerTest { get { return target as PlayerTest; } }


    public override void OnInspectorGUI()
    {
        GUILayout.Label("玩家相关设置", EditorStyles.boldLabel);
        GUILayout.Space(7);
        //=================movement==================
        if (PlayerTest.m_movement==null)
        {
            if(GUILayout.Button("添加移动模块", GUILayout.Height(25)))
            {
                PlayerTest.m_movement = PlayerTest.gameObject.AddComponent<Movement>();
            }
        }
        else
        {
            EditorGUILayout.ObjectField("移动组件",PlayerTest.m_movement, typeof(Movement), true);
        }

    }

}


