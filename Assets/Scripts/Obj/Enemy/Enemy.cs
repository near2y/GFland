﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class  Enemy : MonoBehaviour
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

    [Header("< 调试相关 >")]
    public Transform startPos;
    public Transform agentTarget;
    public Emitter emitter = null;


    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Collider bodyCollider;
    [HideInInspector]
    public float targetSqrDis;

    [HideInInspector]
    public int id_Attack = Animator.StringToHash("Attack");

    protected bool died = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        bodyCollider = GetComponent<Collider>();
        emitter = GetComponent<Emitter>();
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
        }
        //攻击范围
        //agent.stoppingDistance = attackRange;
        //初始位置
        transform.position = start.position;
        transform.rotation = start.rotation;
        //攻击对象
        agentTarget = target;
        //不再处于死亡状态
        died = false;
        //玩家与敌人的距离平方
        targetSqrDis = Vector3.SqrMagnitude(transform.position - agentTarget.position);
    }

    public void Release()
    {
        ObjectManager.Instance.ReleaseObject(gameObject,recycleParent:false);

    }

    public void Dying()
    {
        died = true;
        anim.Play(EnemyState.Dying);
        SceneManager.Instance.enemyManager.ClearEnemy(this);
    }
}


public class EnemyState
{
    public static string InStage = "InStage";
    public static string Walk = "Walk";
    public static string Dying = "Dying";
    public static string Attack = "Attack";
}