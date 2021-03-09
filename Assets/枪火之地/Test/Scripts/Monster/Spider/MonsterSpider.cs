using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpider : AgentMonster
{

    public MonsterSpawnType m_SpawnType;
    public Vector2 m_SpawnRange;

    int m_idSpawnType = Animator.StringToHash("SpawnType");

    public override void ToStage(Transform target)
    {
        m_Target = target;
        if(m_SpawnType == MonsterSpawnType.Climb)
        {
            m_StateCompiler.m_Animator.Play("ToStage", 0, Random.Range(m_SpawnRange.x, m_SpawnRange.y));
        }
        else
        {
            m_StateCompiler.m_Animator.Play("ToStage", 0);
        }
    }


    public void SetSpawnType()
    {
        m_StateCompiler.m_Animator.SetFloat(m_idSpawnType, (float)m_SpawnType);
    }


    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterSpider));
    }
}
