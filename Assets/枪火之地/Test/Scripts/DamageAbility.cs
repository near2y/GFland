using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class DamageAbility
{
    public float m_Hp;
    Action m_CompleteCallBack;
    bool m_InHit;
    Dictionary<Material, float> m_MaterialDic = new Dictionary<Material, float>();

    const string str_colorrange = "_colorrange";

    public void Update()
    {
        if (!m_InHit) return;
        var completed = true;
        foreach (var material in m_MaterialDic.Keys)
        {
            float value = material.GetFloat(str_colorrange);
            float target = m_MaterialDic[material];
            if (value != target)
            {
                float res = Mathf.Lerp(value, target, 15 * Time.deltaTime);
                if (Mathf.Abs(res - target) < 0.3f)
                {
                    res = target;
                }
                material.SetFloat(str_colorrange, res);
                completed = false;
            }
        }
        m_InHit = !completed;
    }

    public void Init(float hp)
    {
        m_Hp = hp;
    }

    public void Init(float hp,Transform obj)
    {
        Init(hp);
        m_MaterialDic.Clear();
        Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
        foreach (var render in renders)
        {
            if (render.material.HasProperty(str_colorrange))
            {
                m_MaterialDic.Add(render.material, render.material.GetFloat(str_colorrange));
            }  
        }
    }

    public void Init(float hp,Transform obj,params Action[] complete)
    {
        Init(hp, obj);
        for (int i = 0; i < complete.Length; i++)
        {
            m_CompleteCallBack += complete[i];
        }
    }

    public void SubHp(float sub,float colorRange = 2)
    {
        m_InHit = true;
        m_Hp -= sub;
        if (m_Hp <= 0)
        {
            m_CompleteCallBack?.Invoke();
        }
        else
        {
            foreach (var material in m_MaterialDic.Keys)
            {
                material.SetFloat(str_colorrange, colorRange);
            }
        }
    }


}
