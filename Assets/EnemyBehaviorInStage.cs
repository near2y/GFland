﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorInStage : EnemyBehaviorBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.bodyCollider.enabled = false;
        if(enemy.agent != null)
        {
            enemy.agent.enabled = false;
        }
        enemy.anim.speed = enemy.anim.speed * enemy.inStageSpeedRatio;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.anim.speed = enemy.anim.speed / enemy.inStageSpeedRatio;
        enemy.bodyCollider.enabled = true;
        if(SceneManager.Instance != null)
        {
            SceneManager.Instance.enemyManager.AddEnemy(enemy);
        }
        enemy.completeInStage = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
