using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阵营管理器
/// </summary>
public class CampManager : Singleton<CampManager>
{
    public Dictionary<string, List<GameObject>> m_CampDic = new Dictionary<string, List<GameObject>>();


    public void Reset()
    {
        
    }


    /// <summary>
    /// 注册阵营
    /// </summary>
    public void RegisterCamp(string tag)
    {
        if (!m_CampDic.ContainsKey(tag))
        {
            m_CampDic.Add(tag, new List<GameObject>());
        }
    }

    /// <summary>
    /// 移除阵营
    /// </summary>
    public void ClearCamp(string tag)
    {
        m_CampDic.Remove(tag);
    }

    /// <summary>
    /// 增加至对应的阵营
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="obj"></param>
    public void AddToCamp(string tag,GameObject obj)
    {

    }
}
