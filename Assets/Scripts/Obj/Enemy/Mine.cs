﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Enemy
{
    [Header("< 爆炸弹的相关属性 >")]
    public float traceSpeed = 5;
    public int effectID= 4003;

    Rigidbody rig;
    //在等待发射
    Transform followPoint = null;

    new void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody>();
    }


    public override void InStage(Transform target, Transform spwanPoint)
    {
        Init(target, spwanPoint);
        followPoint = spwanPoint;
        //钢体处于运动学状态
        rig.isKinematic = true;
        //关闭碰撞，发射前无法被打爆
        bodyCollider.enabled = false;
    }

    private void Update()
    {
        if (!died)
        {
            if (followPoint == null) return;
            if (hp <= 0)
            {
                Dying();
                return;
            }
            if (rig.isKinematic)
            {
                //跟随目标
                transform.position = followPoint.position;
                transform.rotation = followPoint.rotation;
            }

            if (!rig.isKinematic)
            {
                Vector3 dir = agentTarget.position - transform.position;
                rig.AddForce(dir.normalized * Time.deltaTime * traceSpeed, ForceMode.Impulse);
            }

            BaseUpdate();
        }
    }

    public void BeShoot()
    {
        //不再处于等待发射状态
        rig.isKinematic = false;
        bodyCollider.enabled = true;
        anim.Play(EnemyState.InStage);
        rig.AddForce(-(transform.right).normalized * 10, ForceMode.Impulse);
        GameManager.Instance.gameSceneMgr.enemyManager.AddEnemy(this);
    }


    IEnumerator Boom()
    {
        died = true;
        rig.isKinematic = true;
        //变黑
        float startColorRange = meshRenderer.material.GetFloat("_colorrange");
        meshRenderer.material.SetFloat("_colorrange", 0.3f);
        //显示爆炸特效
        GameObject effect = GameManager.Instance.gameSceneMgr.effectManager.GetEffect(effectID);
        effect.transform.position = transform.position;
        //变回原来的样子
        anim.Play(EnemyState.Dying);
        //不会再被打到
        bodyCollider.enabled = false;
        //回收
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.SetFloat("_colorrange", startColorRange);
        GameManager.Instance.gameSceneMgr.enemyManager.ClearEnemy(this,true);
    }

    new public void Dying()
    {
        StartCoroutine(Boom());
    }


    private void OnParticleCollision(GameObject other)
    {
        OnHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && !died)
        {
            StartCoroutine(Boom());
        }
    }
}

