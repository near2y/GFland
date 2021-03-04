using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AgentMonster : MonoBehaviour
{
    public float m_Hp;
    public AnimateStateCompiler m_StateCompiler;
    public DamageAbility m_DamageAbility;
    public AttackAbility m_AttackAblity;
    protected int m_idAttack = Animator.StringToHash("Attack");
    protected int m_idDie = Animator.StringToHash("Dying");
    protected bool m_HasTarget
    {
        get { return m_Target != null; }
    }

    [Header("NavMeshAgent Role")]
    public Transform m_Target;
    protected NavMeshAgent m_Agent;
    protected float m_TargetDisSqr = 0;


    private void Awake()
    {
        m_StateCompiler.Init(typeof(AgentMonster));
        m_DamageAbility.Init(100, transform,ToDying);
        m_Agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
        m_DamageAbility.Update();
    }


    #region Spawn
    public virtual void SpawnMonster()
    {
        m_StateCompiler.m_Animator.Play("ToStage");
    }
    
    public virtual void SetSpawnType()
    {

    }

    #endregion

    #region Move
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
        if (!m_HasTarget) return;
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
        if (m_AttackAblity.RfreshTimer && m_TargetDisSqr < m_AttackAblity.m_AttackDistanceSqr && Method.InSight(transform, m_Target, 20) )
        {
            m_StateCompiler.m_Animator.SetTrigger(m_idAttack);
            m_AttackAblity.Reset();
        }
    }
    #endregion

    protected virtual void ToDying()
    {
        //播放动画
        m_StateCompiler.m_Animator.SetTrigger(m_idDie);
        //TODO从队列中移除
        //
    }
}


public enum MonsterSpawnType
{
    Full = 0,
    Climb,
}

