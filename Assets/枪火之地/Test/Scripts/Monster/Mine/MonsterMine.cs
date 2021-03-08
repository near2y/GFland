using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody),typeof(SphereCollider))]
public class MonsterMine : Monster
{
    protected Rigidbody m_Rigbody;
    protected SphereCollider m_Collider;
    Vector3 m_TargetForce;
    
    [Header("炮弹相关设置")]
    [Tooltip("朝向目标移动强度")]
    public float m_Power;
    public LayerMask m_CouldMoveLayer;

    private void Start()
    {
        m_TargetForce = new Vector3();
        m_Rigbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<SphereCollider>();
        IsSleep = true;
    }

    protected override void InitStateCompiler()
    {
        m_StateCompiler.Init(typeof(MonsterMine));
    }

    //休眠
    bool IsSleep
    {
        set
        {
            if (value)
            {
                m_Rigbody.isKinematic = true;
                m_Collider.enabled = false;
            }
            else
            {
                m_Rigbody.isKinematic = false;
                m_Collider.enabled = true;
            }
        }
    }

    public override void ToStage(Transform target)
    {
        base.ToStage(target);
        //TEST
        Shoot();
    }

    int m_idShoot = Animator.StringToHash("Shoot");
    public void Shoot()
    {
        IsSleep = false;
        m_Animator.SetTrigger(m_idShoot);
    }

    public void MoveToTarget()
    {
        Vector3 dir = m_Target.position - transform.position;
        m_TargetForce = dir.normalized;
        m_Rigbody.velocity = dir.normalized * m_Power;

    }

    public override void OutStage()
    {
        base.OutStage();
        IsSleep = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            MoveToTarget();
        }
    }


}
