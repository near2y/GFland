using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    [Header("< 怪物参数相关 >")]
    //是否为近战
    public bool melee;
    //攻击射程
    public float attackRange;
    //攻击间隔
    public float attackInterval;
    //血量
    public float hp;


    [Header("< 调试相关 >")]
    public Transform startPos;
    public Transform agentTarget;
    public Vector2 startRange = new Vector2(0.6f,0.8f);

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Collider bodyCollider;
    [HideInInspector]
    public float targetSqrDis;

    //================Animator ID
    [HideInInspector]
    public int id_Melee = Animator.StringToHash("Melee");
    [HideInInspector]
    public int id_Attack = Animator.StringToHash("Attack");
    //================Animator ID

    bool died = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        bodyCollider = GetComponent<Collider>();
    }

    //把敌人放入场景中
    public void InStage(Transform target,Transform start)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        EnemyBehaviorBase[] behaviours = anim.GetBehaviours<EnemyBehaviorBase>();
        foreach (EnemyBehaviorBase behaviour in behaviours)
        {
            behaviour.enemy = this;
        }
        //配置该角色相关设置
        //攻击类型
        float attcakType = melee? 1 : 0;
        anim.SetFloat(id_Melee, attcakType);
        //攻击范围
        agent.stoppingDistance = attackRange;
        //初始位置
        transform.position = start.position;
        transform.rotation = start.rotation;
        //攻击对象
        agentTarget = target;
        //从哪一帧开始播放登场动画
        anim.Play(EnemyState.InStage.ToString(), 0, Random.Range(startRange.x,startRange.y));
        //血量
        hp = 100;
        died = false;
        targetSqrDis = Vector3.SqrMagnitude(transform.position - agentTarget.position);
    }

    public void Release()
    {
        ObjectManager.Instance.ReleaseObject(gameObject,recycleParent:false);
    }

    public void Dying()
    {
        died = true;
        anim.Play(EnemyState.Dying.ToString());
        SceneManager.Instance.enemyManager.ClearEnemy(this);
    }

    private void OnParticleCollision(GameObject other)
    {
        hp -= SceneManager.Instance.player.ATK;
        if (hp <= 0&& !died)
        {
            Dying();
        }
    }
}


public enum EnemyState
{
    InStage,
    Walk,
    Dying,
    Attack
}
