using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMonster : MonoBehaviour
{
    public float m_Hp;
    public AnimateStateCompiler m_StateCompiler;

    [Header("NavMeshAgent Role")]
    public Transform m_Target;
    NavMeshAgent m_Agent;



    private void Awake()
    {
        m_StateCompiler.Init(typeof(AgentMonster));
    }

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
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
        Debug.Log("near2y spwan monster");
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

