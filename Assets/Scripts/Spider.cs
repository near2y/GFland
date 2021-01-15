using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Enemy
{
    int id_Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        target = GameManager.Instance.player.gameObject.transform;
    }

    void Update()
    {
        if (!Transform.ReferenceEquals(target, null))
        {
            agent.destination = target.position;
            targetSqrDis = Vector3.SqrMagnitude(transform.position - target.position);
            isStop =  targetSqrDis <= agent.stoppingDistance * agent.stoppingDistance;
            anim.SetBool(id_Attack, isStop);
        }
    }
}
