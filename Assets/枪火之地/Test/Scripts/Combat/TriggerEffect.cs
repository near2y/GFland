using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffect : MonoBehaviour
{
    public float m_TriggerEnabledTime;
    float timer;
    Collider m_Collider;
    bool m_HadCollider;
    Skill m_Skill;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_HadCollider = m_Collider != null;
    }

    public void Init(CombatAbility ability,Skill skill)
    {
        m_Skill = skill;
        timer = 0;
        if(m_HadCollider)m_Collider.enabled = false;
        //触碰层级
        gameObject.layer = ability.layer;
            
        //

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(m_HadCollider && timer >= m_TriggerEnabledTime && !m_Collider.enabled)
        {
            m_Collider.enabled = true;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        CombatAbility ability = other.GetComponent<AttackRole>()?.m_CombatAbility;
        if (ability != null)
        {
            Debug.Log("当前怪物血量：" + ability.currentHp);
            m_Skill.DamageTiming(ref ability);
            Debug.Log("被攻击后：" + ability.currentHp);
        }
    }

    private void OnParticleCollision(GameObject other)
    {

    }



}
