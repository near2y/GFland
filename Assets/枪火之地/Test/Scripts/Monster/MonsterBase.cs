using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterBase : MonoBehaviour
{
    public Movement m_movement;
    public Animator m_animator;
    public NavMeshAgent m_agent;
    public Transform m_agentTarget;
    public AnimateStateCompiler m_animationMent;
    bool hadAgent = false;

    private void Awake()
    {
        //m_animationMent.Init(typeof(MonsterBase));
    }


    private void Start()
    {

        hadAgent = m_agentTarget != null;
    }

    //How to move
    public void InMove()
    {
        m_agent.SetDestination(m_agentTarget.position);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToStage();
        }
    }
    //How to attack


    //How to die


    //Hot to born
    void ToStage()
    {
        m_animator.Play("ToStage");
    }


    
    void MoveByAgent()
    {
        Debug.Log("Move By Agent");
    }

}


