using UnityEngine;

public class EnemyBehaviorInStage : EnemyBehaviorBase
{
    float startAniSpeed = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.bodyCollider != null) enemy.bodyCollider.enabled = false;
        if (enemy.agent != null) enemy.agent.enabled = false;
        startAniSpeed = enemy.anim.speed;
        enemy.anim.speed = startAniSpeed * enemy.inStageSpeedRatio;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (!enemy.completeInStage && stateInfo.normalizedTime>0.9f)
        //{
        //    if (SceneManager.Instance != null)
        //    {
        //        SceneManager.Instance.enemyManager.AddEnemy(enemy);
        //    }
        //    enemy.bodyCollider.enabled = true;
        //    enemy.completeInStage = true;
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.anim.speed =  startAniSpeed;
        enemy.completeInStage = true;
        if (SceneManager.Instance.bossGame) SceneManager.Instance.player.StartGame();

    }
}
