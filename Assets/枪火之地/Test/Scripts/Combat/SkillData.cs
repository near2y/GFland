using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class SkillData 
{
    Dictionary<int, SkillBase> m_skillDic;


    public SkillData()
    {
        m_skillDic = new Dictionary<int, SkillBase>();

        var js = JSON.Parse(File.ReadAllText(ConstGameSet.skillJsonUrl).ToString());
        for (int i = 0; i < js.Count; i++)
        {
            SkillBase skill = new SkillBase(js[i]);
            m_skillDic.Add(skill.id, skill);
        }
    }

    public SkillBase GetSkillByID(int id)
    {
        if (m_skillDic.ContainsKey(id))
        {
            return m_skillDic[id];
        }
        else
        {
            Debug.LogError("技能id:" + id + "没有找到，检查！");
            return null;
        }
    }
}
