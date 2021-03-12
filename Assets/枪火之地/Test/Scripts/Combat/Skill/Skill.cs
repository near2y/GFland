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

    public void Init(SkillDataBase data, Animator ani, MonoBehaviour mono, SkillShowEffect showEffect)
    {
        m_Data = data;
        m_Mono = mono;
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
    }
    public void Release()
    {
        if (!m_inited) return;
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
        
    }

    IEnumerator PlayEffect(float ratio,int id)
    {
        var time = ratio * m_aniTimeLength;
        yield return new WaitForSeconds(time);
        m_showEffect?.Invoke(id,m_Transform);
    }
}

public delegate GameObject SkillShowEffect(int i,Transform trans);


