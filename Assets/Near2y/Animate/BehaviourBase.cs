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
    public string[] m_EnterCallBackName;
    [SerializeField]
    public string[] m_UpdateCallBackName;
    [SerializeField]
    public string[] m_ExitCallBackName;

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
        if (!inited) Debug.LogError("没有初始化角色状态行为：" + animator.gameObject.name+",情调用   animateStateCompiler.Init()");
#endif
        m_EnterDelegate?.Invoke();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_UpdateDelegate?.Invoke();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_ExitDelegate?.Invoke();
    }


    void AddDelegate(ref BehaviourCallBack callBack,string[] names,System.Type type)
    {
        foreach (var name in names)
        {
            AddDelegate(ref callBack, name, type);
        }
    }
    void AddDelegate(ref BehaviourCallBack callBack, string name, System.Type type)
    {
        var m = type.GetMethod(name);
        if (m != null)
        {
            callBack += (BehaviourCallBack)m.CreateDelegate(typeof(BehaviourCallBack), m_Bind);
        }
    }
}



