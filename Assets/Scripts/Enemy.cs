﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    [Header("< 怪物参数 >")]
    public int id;
    public string enemyName;
    public float hp;
    public float attackDis;
    public int effectId;
    public float size;

    [Header("< 游戏中变量展示 >")]
    public Transform target = null;
    public bool isStop = false;
    public float targetSqrDis = 0;

    protected Animator anim;
    protected NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Init(EnemyBase data)
    {
        id = data.Id;
        enemyName = data.Name;
        attackDis = data.AttackDis;
        effectId = data.EffectId;
        hp = data.Hp;
        size = data.Size;
        
        //set
        gameObject.transform.localScale = Vector3.one * size;
        agent.stoppingDistance = attackDis;
    }


}
