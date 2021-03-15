using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : AttackRole
{
    [HideInInspector]
    public DamageAbility m_DamageAbility;
    [HideInInspector]
    public Transform m_Target;
    [HideInInspector]
    public bool m_InMonsterList;
    public AnimateStateCompiler m_StateCompiler;


    protected void Awake()
    {
        m_DamageAbility.Init(100, transform, OutStage);
        InitStateCompiler();
    }

    private void Start()
    {
        //TODO
        //配置CombatAbility
        m_CombatAbility = GameManager.Instance.m_CombatAbilityClassPool.Spawn(true);
        m_CombatAbility.currentHp = 100;
        m_CombatAbility.gameObject = gameObject;
        m_CombatAbility.campName = ConstString.MonsterCampName;
    }

    protected abstract void InitStateCompiler();


    protected int m_idToStage = Animator.StringToHash("ToStage");
    public virtual void ToStage(Transform target)
    {
        m_Target = target;
        m_StateCompiler.m_Animator.SetTrigger(m_idToStage);
        m_InMonsterList = false;
    }

    protected int m_idOutStage = Animator.StringToHash("OutStage");


    public virtual void OutStage()
    {
        m_StateCompiler.m_Animator.SetTrigger(m_idOutStage);
        GameManager.Instance.gameSceneMgr.enemyManager.RemoveMonster(this);
    }


    protected void Update()
    {
        m_DamageAbility.Update();
    }

    /// <summary>
    /// 完成生成
    /// </summary>
    public virtual void CompleteSpawn()
    {
        GameManager.Instance.gameSceneMgr.enemyManager.AddMonster(this);
    }
}

public enum MonsterSpawnType
{
    Full = 0,
    Climb,
}

