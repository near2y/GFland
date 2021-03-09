using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBeatTitan : AgentMonster
{
    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterBeatTitan));
    }


}
