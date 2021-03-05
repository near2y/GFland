using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [Header("< 怪物参数相关 >")]
    //攻击射程
    public float attackRange;
    //攻击间隔
    public float attackInterval;
    //血量
    public float hp;
    //移动速度比例
    public float walkSpeedRatio = 1;
    //攻击速度比例
    public float attackSpeedRatio = 1;
    //登场速度比例
    public float inStageSpeedRatio = 1;
    //怪物材质
    public Renderer meshRenderer = null;
    //怪物爆炸特效大小
    public float boomEffectSize = 1;

    [Header("< 调试相关 >")]
    public Transform startPos;
    public Transform agentTarget;
    public Emitter emitter = null;
    public bool invalid = false;


    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Collider bodyCollider;
    [HideInInspector]
    public float targetSqrDis;
    [HideInInspector]
    public bool completeInStage= false;
    [HideInInspector]
    public float attackTimer = 0;
    [HideInInspector]
    public bool addedInGame = false;

    [HideInInspector]
    public int id_Attack = Animator.StringToHash("Attack");

    [HideInInspector]
    public bool died = false;
    [HideInInspector]
    public bool lookPlayerInAttack = false;
    protected float startColorRange = 1;
    protected float startAniSpeed = 0;

    protected int otherGameID = 0;
    protected ParticleSystem otherParticleSystem = null;
    protected List<ParticleCollisionEvent> collisionEvents;
    protected bool needPlayDying = true;


    protected void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        bodyCollider = GetComponent<Collider>();
        emitter = GetComponent<Emitter>();
        startColorRange = Method.GetColorrangeInRender(meshRenderer);
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    //把敌人放入场景中
    public abstract void InStage(Transform target, Transform spwanPoint);

    //初始化
    protected void Init(Transform target, Transform start)
    {
        //由于没有放回到隐藏对象的子物体，需要考虑是否隐藏的情况
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        //初始化该对象的动作行为
        EnemyBehaviorBase[] behaviours = anim.GetBehaviours<EnemyBehaviorBase>();
        foreach (EnemyBehaviorBase behaviour in behaviours)
        {
            behaviour.enemy = this;
            if(behaviour.GetType() == typeof(EnemyBehaviorDying))
            {
                behaviour.EnterCallBack = () =>
                {
                      
                };
            }
        }
        //攻击范围
        //agent.stoppingDistance = attackRange;
        startPos = start;
        //初始位置
        transform.position = start.position;
        transform.rotation = start.rotation;
        //攻击对象
        agentTarget = target;
        //不再处于死亡状态
        died = false;
        //恢复render
        if (meshRenderer != null)
        {
            meshRenderer.material.SetFloat("_DissvoleRange", 0);
            Method.SetRenderColorRange(meshRenderer, startColorRange);
        }
        //开启emitter
        if(emitter != null)emitter.SetActive(true);
        startAniSpeed = anim.speed;

        //attackTimer
        attackTimer = attackInterval;

        //added
        addedInGame = false;
    }

    protected void FullGround(float size)
    {
        GameObject effect = GameManager.Instance.gameSceneMgr.effectManager.GetEffect(4007);
        effect.transform.position = transform.position;
        effect.transform.localScale = Vector3.one * size;
    }

    public void Release()
    {
        anim.speed = startAniSpeed;
        attackTimer = attackInterval;
        ObjectManager.Instance.ReleaseObject(gameObject,recycleParent:false);
    }

    public void Dying()
    {
        if(emitter!=null)emitter.SetActive(false);
        died = true;
        if(needPlayDying)anim.Play(EnemyState.Dying);
        //手机震动
        //Handheld.Vibrate();
        ////相机震动
        GameManager.Instance.gameSceneMgr.gameCamera.ShakeCamera(Random.Range(0.5f, 4f), Random.Range(0.5f, 1f));
        //变黑
        if (meshRenderer != null)
        {
            Method.SetRenderColorRange(meshRenderer, 0.3f);
        }
        //爆炸特效
        GameObject boomEffect = GameManager.Instance.gameSceneMgr.effectManager.GetEffect(4005);
        boomEffect.transform.localScale = Vector3.one * boomEffectSize;
        boomEffect.transform.position = transform.position;

    }

    protected void BaseUpdate()
    {
        targetSqrDis = Vector3.SqrMagnitude(transform.position - agentTarget.position);
        if (hitting)
        {
            hitting = Method.LerpRenderColorRange(meshRenderer, startColorRange);
        }
    }

    protected bool hitting = false;
    protected void OnHit(GameObject other,float colorrange = 2f)
    {
        if (other.GetInstanceID() != otherGameID)
        {
            otherParticleSystem = other.GetComponent<ParticleSystem>();
        }
        int count = otherParticleSystem.GetCollisionEvents(gameObject, collisionEvents);
        float sub = GameManager.Instance.gameSceneMgr.player.ATK * count;
        HitSubHp(sub, colorrange);
    }

    public void HitSubHp(float subHp,float colorrange)
    {
        hp -= subHp; 
        if (meshRenderer == null) return;
        hitting = Method.SetRenderColorRange(meshRenderer, colorrange);
    }
}


public class EnemyState
{
    public static string InStage = "InStage";
    public static string Walk = "Walk";
    public static string Dying = "Dying";
    public static string Attack = "Attack";
}
