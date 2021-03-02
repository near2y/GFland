using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Events;

[Serializable]
public class AnimateStateCompiler 
{
    public bool m_showProperty = false;
    public Animator m_Animator = null;
    public UnityEvent m_UnityEvent;
    public MonoBehaviour m_Mono;

    public AnimateStateCompiler()
    {

    }

    public void Init(System.Type type)
    {
        var behaviours = m_Animator.GetBehaviours<BehaviourBase>();
        foreach (var behaviour in behaviours)
        {
            behaviour.Init(m_Mono,type);
        }
    }

}
