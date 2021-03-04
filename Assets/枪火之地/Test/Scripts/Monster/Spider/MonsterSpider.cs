using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : AgentMonster
{

    public MonsterSpawnType m_SpawnType;
    public Vector2 m_SpawnRange;

    int m_idSpawnType = Animator.StringToHash("SpawnType");
   

    public override void SpawnMonster()
    {
        //播放出场方式
        m_StateCompiler.m_Animator.Play("ToStage", 0, Random.Range(m_SpawnRange.x, m_SpawnRange.y));
    }

    public override void SetSpawnType()
    {
        m_StateCompiler.m_Animator.SetFloat(m_idSpawnType, (float)m_SpawnType);
    }

    public virtual void EnterAttack()
    {
        m_Agent.isStopped = true;
    }
}
