using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BehaviourState1 : EnemyBehaviorBase
{
    public float scale = 1;
    public float attackTime = 0;
    public float attackInterval = 0;
    float startPointSpeed = 0;
    float startAgentSpeed = 0;
    float intervalTimer = 0;
    float attackTimer = 0;

    bool inShoot = false;
    Boss1 boss = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.agent != null)
        {
            enemy.agent.enabled = true;
        }

        if (GameManager.Instance.gameSceneMgr != null)
        {
            GameManager.Instance.mono.StartCoroutine(AddBoss());
        }
        enemy.bodyCollider.enabled = true;


        boss = enemy as Boss1;

        startPointSpeed = boss.speed;
        startAgentSpeed = boss.agent.speed;
        boss.speed *= scale;
        boss.agent.speed *= scale;
        boss.agent.isStopped = false;

    }

    IEnumerator AddBoss()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.gameSceneMgr.enemyManager.AddEnemy(enemy);
        enemy.completeInStage = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.BossPointMove();
        //lookat
        Quaternion rotation = Quaternion.LookRotation(boss.agentTarget.transform.position - boss.transform.position);
        boss.transform.rotation = Quaternion.Lerp(boss.transform.rotation, rotation, boss.lookAtLerp * Time.deltaTime);
        //agent
        boss.agent.SetDestination(boss.bossPoint.transform.position);

        //attack
        intervalTimer += Time.deltaTime * 1000;
        if(intervalTimer >= attackInterval && !inShoot)
        {
            animator.Play("Shoot", 1);
            inShoot = true;
        }
        if(inShoot)
        {
            attackTimer += Time.deltaTime * 1000;
            if (attackTimer > attackTime)
            {
                animator.Play("Empty", 1);
                inShoot = false;
                intervalTimer = 0;
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play("Empty", 1);
        boss.speed = startPointSpeed;
        boss.agent.speed = startAgentSpeed;
        boss.agent.isStopped = true;
        boss.bodyCollider.enabled = false;
        boss = null;
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
