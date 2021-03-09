using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDog : AgentMonster
{
    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterDog));
    }



}
