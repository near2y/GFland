using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Reflection;

public delegate void BehaviourCallBack();

public class BehaviourBase : StateMachineBehaviour 
{
    [HideInInspector]
    public MonoBehaviour m_Bind = null;

    [SerializeField]
    public string m_EnterCallBackName;
    [SerializeField]
    public string m_UpdateCallBackName;
    [SerializeField]
    public string m_ExitCallBackName;

    BehaviourCallBack m_EnterDelegate;
    BehaviourCallBack m_UpdateDelegate;
    BehaviourCallBack m_ExitDelegate;

    bool inited = false;
    public void Init(MonoBehaviour bind,System.Type type)
    {
        inited = true;
        m_Bind = bind;

        AddDelegate(ref m_EnterDelegate, m_EnterCallBackName, type);
        AddDelegate(ref m_UpdateDelegate, m_UpdateCallBackName, type);
        AddDelegate(ref m_ExitDelegate, m_ExitCallBackName, type);
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

#if UNITY_EDITOR
        if (!inited) Debug.LogError("没有初始化角色状态行为：" + animator.gameObject.name);
#endif

        //m_Bind.SendMessage(m_EnterCallBackName,SendMessageOptions.DontRequireReceiver);
        m_EnterDelegate?.Invoke();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_Bind.SendMessage(m_UpdateCallBackName, SendMessageOptions.DontRequireReceiver);
        m_UpdateDelegate?.Invoke();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_Bind.SendMessage(m_ExitCallBackName, SendMessageOptions.DontRequireReceiver);
        m_ExitDelegate?.Invoke();
    }


    void AddDelegate(ref BehaviourCallBack callBack,string name,System.Type type)
    {
        var m = type.GetMethod(name);
        if (m != null)
        {
            callBack += (BehaviourCallBack)m.CreateDelegate(typeof(BehaviourCallBack), m_Bind);
        }
    }
}



