﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorWalk : EnemyBehaviorBase
{
    float startAniSpeed = 0;
    float startAgentSpeed = 0;

    bool hadAgent = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //加入到enemyList
        SceneManager.Instance.enemyManager.AddEnemy(enemy);

        hadAgent = enemy.agent != null;
        startAniSpeed = enemy.anim.speed;
        enemy.anim.speed = enemy.walkSpeedRatio;


        if (hadAgent)
        {
            enemy.agent.enabled = true;
            startAgentSpeed = enemy.agent.speed;
            enemy.agent.speed = startAgentSpeed * enemy.walkSpeedRatio;
        }
        if (enemy.bodyCollider != null) enemy.bodyCollider.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hadAgent)
        {
            enemy.agent.SetDestination(enemy.agentTarget.position);
        }
        enemy.attackTimer += Time.deltaTime * 1000;
        if(enemy.attackTimer >= enemy.attackInterval)
        {
            //距离够，切看下目标
            if(enemy.targetSqrDis <= enemy.attackRange * enemy.attackRange && Method.InSight(enemy.transform,enemy.agentTarget.transform,20))
            {
                enemy.anim.SetBool(enemy.id_Attack, true);
                enemy.attackTimer = 0;
                enemy.anim.speed = startAniSpeed;
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.anim.speed = startAniSpeed;
        if (hadAgent)
        {
            enemy.agent.speed = startAgentSpeed;
            hadAgent = false;
        }
    }

}