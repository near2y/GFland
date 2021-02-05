using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BehaviourState2 : EnemyBehaviorBase
{


    Boss1 boss = null;
    float timer = 0;
    int dir = 1;
    float scale = 1;
    bool bullet1Shooted = false;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = enemy as Boss1;
        timer =boss.rotateBullet1AttackInterval - 1000;

        bullet1Shooted = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //第一种子弹
        boss.rotateShoot1.Rotate(Vector3.forward * boss.rotateSpeed * dir * scale * Time.deltaTime);
        boss.rotateShoot2.Rotate(Vector3.forward * boss.rotateSpeed * dir * scale * Time.deltaTime);
        timer += Time.deltaTime * 1000;
        if(timer >= (boss.rotateBullet1AttackInterval) && !bullet1Shooted)
        {
            foreach (ParticleSystem system in boss.rotateBullet1List)
            {
                system.Play();
            }
            dir = Random.Range(0, 1) > 0.5 ? 1 : -1;
            scale = 1;
            bullet1Shooted = true;
        }

        //第二种子弹
        if(timer >= (boss.rotateBullet1AttackInterval + boss.duration1+ boss.rotateBullet2AttackInterval))
        {
            foreach (ParticleSystem system in boss.rotateBullet2List)
            {
                system.Play();
            }
            dir = Random.Range(0, 1) > 0.5 ? 1 : -1;
            scale = 1f;
            timer = 0;
            bullet1Shooted = false;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (ParticleSystem system in boss.rotateBullet1List)
        {
            system.Stop();
        }

        foreach (ParticleSystem system in boss.rotateBullet2List)
        {
            system.Stop();
        }
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
