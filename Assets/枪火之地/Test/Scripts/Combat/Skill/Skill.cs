using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    SkillDataBase m_Data;
    bool m_inited = false;
    Animator m_Ani = null;
    bool m_hadAni = false;
    float m_aniTimeLength = 0;
    MonoBehaviour m_Mono;
    Transform m_Transform;
    SkillShowEffect m_showEffect;
    CombatAbility m_ability;

    /// <summary>
    /// 初始化技能
    /// </summary>
    /// <param name="data">技能基础属性</param>
    /// <param name="ability">角色战斗属性</param>
    /// <param name="ani">角色动画控制器</param>
    /// <param name="mono">携带该技能的对象的mono脚本</param>
    /// <param name="showEffect">如何表现特效的回调函数</param>
    public void Init(SkillDataBase data, CombatAbility ability,Animator ani, MonoBehaviour mono, SkillShowEffect showEffect)
    {
        m_Data = data;
        m_Mono = mono;
        m_ability = ability;
        m_Transform = m_Mono ? m_Mono.transform : null;
        m_inited = true;
        m_Ani = null;
        m_showEffect += showEffect;
        m_hadAni = (m_Ani != null && m_Data.animateName != "0");
        if (m_hadAni)
        {
            var clips = m_Ani.runtimeAnimatorController.animationClips;
            for (int i = 0; i < clips.Length; i++)
            {
                if(clips[i].name == m_Data.animateName)
                {
                    m_aniTimeLength = clips[i].length;
                    break;
                }
            }
        }
    }

    public void Reset()
    {
        m_Data = null;
        m_inited = false;
        m_Ani = null;
        m_hadAni = false;
        m_aniTimeLength = 0;
        m_showEffect = null;
        m_Transform = null;
        m_ability = null;
    }
    public void Release(CombatAbility ability)
    {
        if (!m_inited) return;
        //判断攻击方式
        
        if(m_Data.attackType == 1)
        {
            //直接攻击  ------   逻辑判断


        }
        else if(m_Data.attackType == 2)
        {
            //弹道攻击  ------    triiger触发     
            for(int i = 0; i < m_Data.projectileTimes.Count; i++)
            {
                m_Mono.StartCoroutine(PlayTriggerEffect(m_Data.projectileTimes[i].nomalizeMoment, m_Data.projectileTimes[i].projectileID,ability));
            }

        }


        //播放动画
        if( m_hadAni)
        {
            m_Ani.Play(m_Data.animateName);
        }
        //播放弹道


        //播放特效
        for(int i = 0; i < m_Data.effectTimes.Count; i++)
        {
            var effectTime = m_Data.effectTimes[i];
            m_Mono.StartCoroutine(PlayEffect(effectTime.nomalizeMoment, effectTime.effectID));
        }
        Debug.Log("释放 " + m_Data.name + " 技能");
        
        //调用功能
    }

    IEnumerator PlayEffect(float ratio,int id)
    {
        var time = ratio * m_aniTimeLength;
        yield return new WaitForSeconds(time);
        m_showEffect?.Invoke(id,m_Transform);
    }

    IEnumerator PlayTriggerEffect(float ratio,int id,CombatAbility ability)
    {
        var time = ratio * m_aniTimeLength;
        yield return new WaitForSeconds(time);
        GameObject effect = m_showEffect?.Invoke(id, m_Transform);
        TriggerEffect triggerEffect = effect.GetComponent<TriggerEffect>();
        if(triggerEffect != null)
        {
            triggerEffect.Init(ability,this);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("特效 ："+id+"没有携带triggerEffect,请检查");
#endif
            triggerEffect = effect.AddComponent<TriggerEffect>();
            triggerEffect.Init(ability,this);
        }
    }



    public void DamageTiming(ref CombatAbility targetAbility)
    {
        if (targetAbility == null) return;
        //伤害帧
        for (int i = 0; i < m_Data.hitDamageTimes.Count; i++)
        {
            HitDamageTime hitDamageTime = m_Data.hitDamageTimes[i];
            BuffBrige.DamageTiming(hitDamageTime.damageProp, m_ability,ref targetAbility);
        }
    }



}

public delegate GameObject SkillShowEffect(int i,Transform trans);


