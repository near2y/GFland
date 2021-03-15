using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阵营管理器
/// </summary>
public class CampManager : Singleton<CampManager>
{
    public Dictionary<string, List<CombatAbility>> m_CampDic = new Dictionary<string, List<CombatAbility>>();


    public void Reset()
    {
        m_CampDic.Clear();
    }

    /// <summary>
    /// 通过阵营类型找到对应的对象
    /// </summary>
    /// <param name="type">所需阵营类型，无限制，敌方，友军，友军除自己，自己</param>
    /// <param name="selfAbility">自己的战斗属性对象</param>
    /// <param name="abilityList">返回所需类型的所有战斗属性对象</param>
    public void FindCampByType(CampType type,CombatAbility selfAbility,ref List<CombatAbility> abilityList)
    {
        abilityList.Clear();
        switch (type)
        {
            case CampType.None:
                //无限制
                foreach (var listName in m_CampDic.Keys)
                {
                    for(int i = 0; i < m_CampDic[listName].Count; i++)
                    {
                        abilityList.Add(m_CampDic[listName][i]);
                    }
                }
                break;
            case CampType.Opposite:
                //对立面
                foreach (var listName in m_CampDic.Keys)
                {
                    if (listName != selfAbility.campName)
                    {
                        for (int i = 0; i < m_CampDic[listName].Count; i++)
                        {
                            abilityList.Add(m_CampDic[listName][i]);
                        }
                    }
                }
                break;
            case CampType.Friendly:
                //友军包括自己
                foreach (var listName in m_CampDic.Keys)
                {
                    if (listName == selfAbility.campName)
                    {
                        for (int i = 0; i < m_CampDic[listName].Count; i++)
                        {
                            abilityList.Add(m_CampDic[listName][i]);
                        }
                    }
                }
                break;
            case CampType.FriendlyExcludeSelf:
                //友军不包括自己
                bool findedSelf = false;
                foreach (var listName in m_CampDic.Keys)
                {
                    if (listName == selfAbility.campName)
                    {
                        for (int i = 0; i < m_CampDic[listName].Count; i++)
                        {
                            if(!findedSelf  && m_CampDic[listName][i] != selfAbility)
                            {
                                abilityList.Add(m_CampDic[listName][i]);
                            }
                        }
                    }
                }
                break;
            case CampType.Self:
                abilityList.Add(selfAbility);
                break;
        }
    }

    public void FindCampByType(CampType type, string campName, ref List<CombatAbility> abilityList)
    {
        abilityList.Clear();
        switch (type)
        {
            case CampType.None:
                //无限制
                foreach (var listName in m_CampDic.Keys)
                {
                    for (int i = 0; i < m_CampDic[listName].Count; i++)
                    {
                        abilityList.Add(m_CampDic[listName][i]);
                    }
                }
                break;
            case CampType.Opposite:
                //对立面
                foreach (var listName in m_CampDic.Keys)
                {
                    if (listName != campName)
                    {
                        for (int i = 0; i < m_CampDic[listName].Count; i++)
                        {
                            abilityList.Add(m_CampDic[listName][i]);
                        }
                    }
                }
                break;
            case CampType.Friendly:
                //友军包括自己
                foreach (var listName in m_CampDic.Keys)
                {
                    if (listName == campName)
                    {
                        for (int i = 0; i < m_CampDic[listName].Count; i++)
                        {
                            abilityList.Add(m_CampDic[listName][i]);
                        }
                    }
                }
                break;
            }
        }


    /// <summary>
    /// 注册阵营
    /// </summary>
    public void RegisterCamp(string camp)
    {
        if (!m_CampDic.ContainsKey(camp))
        {
            m_CampDic.Add(camp, new List<CombatAbility>());
        }
    }

    /// <summary>
    /// 移除阵营
    /// </summary>
    public void ClearCamp(string camp)
    {
        m_CampDic.Remove(camp);
    }

    /// <summary>
    /// 增加至对应的阵营
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="obj"></param>
    public void AddToCamp(string camp,CombatAbility ability)
    {
#if UNITY_EDITOR
        if (!m_CampDic.ContainsKey(camp))
        {
            //没有对应的阵容
            Debug.LogError("没有注册" + camp + "阵营，请假！");
        }
#endif
        m_CampDic[camp].Add(ability);
    }

    /// <summary>
    /// 从阵营中移除对象
    /// </summary>
    /// <param name="camp">阵营名字</param>
    /// <param name="obj">对象</param>
    public void RemoveOutCamp(string camp,CombatAbility ability)
    {
        m_CampDic[camp].Remove(ability);
    }
}

public enum CampType
{
    None = 0,
    Opposite,
    Friendly,
    FriendlyExcludeSelf,
    Self
}

