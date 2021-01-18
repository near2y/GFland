using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Enemy
{
    int id_Attack = Animator.StringToHash("Attack");

    [Header("< Spider参数 >")]
    public SkinnedMeshRenderer render;

    private void Start()
    {
#if UNITY_EDITOR
        if(GameManager.Instance == null)
        {
            Debug.LogError("GameManager不存在！请通过GameStart.Scene启动！");
            return;
        }
        else
        {
            if (GameManager.Instance.player == null)
            {
                Debug.LogError("player没有赋值");
                return;
            }
        }
#endif

        target = GameManager.Instance.player.gameObject.transform;
    }

    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
            targetSqrDis = Vector3.SqrMagnitude(transform.position - target.position);
            isStop =  targetSqrDis <= agent.stoppingDistance * agent.stoppingDistance;
            anim.SetBool(id_Attack, isStop);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }
    }

    /// <summary>
    /// 被攻击闪白
    /// </summary>
    void Hit()
    {
        render.material.color = Color.red;
    }
}
