using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Monster : MonoBehaviour
{
    [HideInInspector]
    public DamageAbility m_DamageAbility;
    [HideInInspector]
    public Transform m_Target;

    public AnimateStateCompiler m_StateCompiler;



    protected void Awake()
    {
        m_DamageAbility.Init(100, transform, OutStage);
        InitStateCompiler();
    }

    protected abstract void InitStateCompiler();


    protected int m_idToStage = Animator.StringToHash("ToStage");
    public virtual void ToStage(Transform target)
    {
        m_Target = target;
        m_StateCompiler.m_Animator.SetTrigger(m_idToStage);
    }

    protected int m_idOutStage = Animator.StringToHash("OutStage");
    public virtual void OutStage()
    {
        m_StateCompiler.m_Animator.SetTrigger(m_idOutStage);
    }


    protected void Update()
    {
        m_DamageAbility.Update();
    }

}

public enum MonsterSpawnType
{
    Full = 0,
    Climb,
}

