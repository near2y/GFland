using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerAttackSystem 
{
    GFLandPlayer m_Player;

    //主动技能
    public InitiativeSkillData m_InitiantiveSkillData;
    Skill m_Skill;

    //武器

    //被动技能

    public void Init(GFLandPlayer player)
    {
        m_Player = player;
        m_Skill = GameManager.Instance.m_SkillClassPool.Spawn(true);
        m_Skill.Init(GameManager.Instance.skillJson.GetDataByID(m_InitiantiveSkillData.skillID),m_Player.m_CombatAbility,m_Player.m_AnimateStateCompiler.m_Animator,m_Player,
            GameManager.Instance.gameSceneMgr.effectManager.GetEffect);

    }


    public void OnMoveUpdate()
    {
        if (Input.GetKeyDown(m_InitiantiveSkillData.skillKey))
        {
            m_Skill.Release(m_Player.m_CombatAbility);
        }
    }


    public void OnDestroy()
    {
        GameManager.Instance.m_SkillClassPool.Recycle(m_Skill);
    }


}

[Serializable]
public class InitiativeSkillData
{
    public KeyCode skillKey;
    public int skillID;
}
