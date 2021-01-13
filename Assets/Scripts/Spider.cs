using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    

    public Animator anim;
    public NavMeshAgent agent;
    public Transform target;
    public bool isStop;

    int id_Attack = Animator.StringToHash("Attack");


    private void Start()
    {
        if(anim == null)
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }
        target = Player.Instance.gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        isStop = Vector3.SqrMagnitude(transform.position - target.position) <= agent.stoppingDistance * agent.stoppingDistance;
        anim.SetBool(id_Attack, isStop);
    }
}
