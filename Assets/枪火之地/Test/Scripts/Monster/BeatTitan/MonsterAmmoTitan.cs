using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAmmoTitan : AgentMonster
{

    bool m_Ready = false;
    
    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterAmmoTitan));
    }

    int m_idLoading = Animator.StringToHash("Loading");
    public override void UpdateMove()
    {
        base.UpdateMove();
        if (!m_Ready)
        {
            m_Ready = true;
            m_StateCompiler.m_Animator.SetTrigger(m_idLoading);
        }
    }

    protected override void JudgeAttack()
    {
        if (m_AttackAblity.RfreshTimer && m_Ready && m_TargetDisSqr < m_AttackAblity.m_AttackDistanceSqr && Method.InSight(transform, m_Target, 35))
        {
            m_StateCompiler.m_Animator.SetTrigger(m_idAttack);
            m_AttackAblity.Reset();
        }
    }

    public void ExitAttack()
    {
        m_Ready = false;
    }
}
