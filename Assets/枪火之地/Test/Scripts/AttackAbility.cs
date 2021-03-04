using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackAbility 
{
    [Tooltip("攻击间隔，单位毫秒")]
    public float m_AttackInterval;
    [Tooltip("伤害")]
    public float m_AttackDamge;
    [Tooltip("攻击距离")]
    public float m_AttackDistance;
    [HideInInspector]
    public float m_AttackDistanceSqr
    {
        get { return m_AttackDistance * m_AttackDistance; }
    }

    float timer = 0;


    public void Reset()
    {
        timer = 0;
    }

    public bool RfreshTimer
    {
        get
        {
            timer += Time.deltaTime * 1000;
            return timer >= m_AttackInterval;
        }
    }
}
