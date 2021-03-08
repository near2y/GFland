using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AgentMonster: Monster
{

    public AttackAbility m_AttackAblity;
    protected int m_idAttack = Animator.StringToHash("Attack");

    [Header("NavMeshAgent Role")]
    protected NavMeshAgent m_Agent;
    protected float m_TargetDisSqr = 0;



    new void Awake()
    {
        base.Awake();

        m_Agent = GetComponent<NavMeshAgent>();

    }

    public virtual void EnterMove()
    {
        m_Agent.isStopped = false;
    }

    public virtual void OutMove()
    {
        m_Agent.isStopped = true;
    }

    public virtual void UpdateMove()
    {
        AgentMove();
        JudgeAttack();
    }

    protected virtual void AgentMove()
    {
        m_Agent.SetDestination(m_Target.position);
        m_TargetDisSqr = Vector3.SqrMagnitude(transform.position - m_Target.position);
    }

    protected virtual void JudgeAttack()
    {
        if (m_AttackAblity.RfreshTimer && m_TargetDisSqr < m_AttackAblity.m_AttackDistanceSqr && Method.InSight(transform, m_Target, 35) )
        {
            m_StateCompiler.m_Animator.SetTrigger(m_idAttack);
            m_AttackAblity.Reset();
        }
    }

}



