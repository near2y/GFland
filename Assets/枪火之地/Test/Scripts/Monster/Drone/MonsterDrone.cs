using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrone : AgentMonster
{


    public MonsterSpawnType m_SpawnType;
    public Vector2 m_SpawnRange;


    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterDrone));

    }


    int m_idSpawnType = Animator.StringToHash("SpawnType");


    public void SetSpawnType()
    {
        m_StateCompiler.m_Animator.SetFloat(m_idSpawnType, (float)m_SpawnType);
    }



}
