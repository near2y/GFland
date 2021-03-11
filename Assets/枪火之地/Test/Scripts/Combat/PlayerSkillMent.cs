using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




[Serializable]
public class PlayerSkillMent 
{
    public KeyCode m_SkillKey;
    public int m_SkillID;
    
    Transform m_Parent;
    bool inited = false;

    public void Init(Transform transform)
    {
        inited = true;
        m_Parent = transform;
    }

    void ReleaseSkill()
    {
        string url = GameManager.Instance.effectJson.GetDataByID(m_SkillID).prePath;
        GameObject effect = GameManager.Instance.gameSceneMgr.effectManager.GetEffect(url);
        effect.transform.position = m_Parent.position;
    }

    public void Update()
    {
        if (!inited) return;
        if (Input.GetKeyDown(m_SkillKey))
        {
            ReleaseSkill();
        }
    }
}
