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
    public DamageAbility m_DamageAbility;

    [Header("NavMeshAgent Role")]
    public Transform m_Target;
    NavMeshAgent m_Agent;

    public MonsterSpawnType m_SpawnType;
    public Vector2 m_SpawnRange;

    int m_idSpawnType = Animator.StringToHash("SpawnType");


    private void Awake()
    {
        m_StateCompiler.Init(typeof(AgentMonster));
        m_DamageAbility.Init(100, transform);
    }

    private void Start()
    {
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


    public virtual void SpawnMonster()
    {
        //播放出场方式
        m_StateCompiler.m_Animator.Play("ToStage",0,Random.Range(m_SpawnRange.x,m_SpawnRange.y));
    }

    public virtual void SetSpawnType()
    {
        m_StateCompiler.m_Animator.SetFloat(m_idSpawnType, (float)m_SpawnType);
    }


    public virtual void UpdateMove()
    {
        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.position);
        }
    }

    public virtual void EnterMove()
    {
        m_Agent.isStopped = false;
    }

    public virtual void OutMove()
    {
        m_Agent.isStopped = true;
    }

}


public class SpawnType
{

}

public enum MonsterSpawnType
{
    Full = 0,
    Climb,

}

