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
            Debug.Log("炮台进行了一次攻击！");
            m_AttackAblity.Reset();
        }
    }

}
