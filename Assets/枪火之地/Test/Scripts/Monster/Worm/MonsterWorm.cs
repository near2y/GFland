using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWorm : AgentMonster
{
    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterWorm));
    }

    //TODO
    //登场有待调整
}
