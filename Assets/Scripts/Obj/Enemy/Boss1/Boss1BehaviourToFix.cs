using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1BehaviourToFix : EnemyBehaviorBase
{
    public Vector3 fixPos = new Vector3();
    public float moveSpeed = 8;
    public Boss1 boss = null;

    int id_FixSqr = Animator.StringToHash("FixSqr");
    float startAgentSpeed = 1;
    float startAgentRotSpeed = 1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = enemy as Boss1;
        fixPos = Vector3.zero;
        //startAgentSpeed = boss.agent.speed;
        //startAgentRotSpeed = boss.agent.angularSpeed;
        //boss.agent.speed = moveSpeed;
        //boss.agent.angularSpeed = 200;
        //boss.agent.isStopped = false;

        boss.agent.isStopped = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.transform.position = Vector3.Lerp(boss.transform.position, fixPos, 2 * Time.deltaTime);
        //Quaternion lookRot = boss.transform.rotation;
        //lookRot.eulerAngles = fixPos - boss.transform.position;
        //boss.transform.rotation = Quaternion.Lerp(boss.transform.rotation, lookRot, 1);
        boss.transform.LookAt(fixPos);
        animator.SetFloat(id_FixSqr, (fixPos - boss.transform.position).sqrMagnitude);
        //boss.agent.SetDestination(fixPos);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.agent.speed = startAgentSpeed;
        //boss.agent.angularSpeed= startAgentRotSpeed;

        //boss.agent.isStopped = true;
        boss.bodyCollider.enabled = true;
        boss.invalid = false;
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
