using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTurret : Monster
{
    public AttackAbility m_AttackAblity;

    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterTurret));
    }

    public void JudgeAttack()
    {
        if (m_AttackAblity.RfreshTimer)
        {
            //TODO
            m_AttackAblity.Reset();
        }
    }

}
