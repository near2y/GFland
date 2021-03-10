using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能作用形状
/// </summary>
public class SkillForm 
{
    SkillFormType m_type;
    float m_radiusSqr;
    float m_lengthSqr;
    float m_angle;

    public SkillForm(string data)
    {
        string[] datas = data.Split('|');
        int typeIndex = int.Parse(datas[0]);
        m_type = (SkillFormType)typeIndex;
        switch (m_type)
        {
            case SkillFormType.Circle:
                m_radiusSqr = float.Parse(datas[1])* float.Parse(datas[1]);
                break;
            case SkillFormType.Pie:
                m_angle = float.Parse(datas[1]) * 0.5f;
                break;
            case SkillFormType.Rect:
                m_lengthSqr = float.Parse(datas[1])* float.Parse(datas[1]);
                break;
        }
    }

    public bool InSkillForm(Transform src,Transform target)
    {
        bool res = false;
        if (target == null) return false;
        switch (m_type)
        {
            case SkillFormType.Target:
                res = true;
                break;
            case SkillFormType.Circle:
                res = Vector3.SqrMagnitude(target.position - src.position)<=m_radiusSqr;
                break;
            case SkillFormType.Pie:
                res = Method.InSight(src, target, m_angle);
                break;
            case SkillFormType.Rect:
                res = Vector3.SqrMagnitude(target.position - src.position) <= m_lengthSqr;
                break;
            default:
                break;
        }
        return res;
    }
}

public enum SkillFormType
{
    Target = 0,
    Circle,
    Pie,
    Rect,
}
