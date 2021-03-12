using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerAttackSystem 
{

    public InitiativeSkillData m_InitiantiveSkillData;
    Skill m_Skill;
    GFLandPlayer m_Player;

    public void Init(GFLandPlayer player)
    {
        m_Player = player;
        m_Skill = GameManager.Instance.m_SkillClassPool.Spawn(true);
        m_Skill.Init(GameManager.Instance.skillJson.GetDataByID(m_InitiantiveSkillData.skillID),m_Player.m_AnimateStateCompiler.m_Animator,m_Player,
            GameManager.Instance.gameSceneMgr.effectManager.GetEffect);
    }

    void EffectTest()
    {
        Debug.Log("释放了特效22222");
    }

    public void OnMoveUpdate()
    {
        if (Input.GetKeyDown(m_InitiantiveSkillData.skillKey))
        {
            m_Skill.Release();
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
