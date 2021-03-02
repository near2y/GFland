using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMonster : MonoBehaviour
{
    
    public float m_Hp;
    public Transform m_Target;

    public AnimateStateCompiler m_StateCompiler;


    NavMeshAgent m_Agent;
    BehaviourCallBack m_callback;


    private void Awake()
    {

        m_Agent = GetComponent<NavMeshAgent>();



        m_StateCompiler.Init(typeof(AgentMonster));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_StateCompiler.m_Animator.Play("ToStage");
        }
    }


    public void SpwanMonster()
    {
        //登场

        //
    }


    public void OnMove()
    {
        if(m_Target != null)
        {
            m_Agent.SetDestination(m_Target.position);
        }
    }



    //添加到场景
    void AddInStage()
    {
        //TODO
        Debug.Log("增加到可攻击的怪物列表中");
    }


}

