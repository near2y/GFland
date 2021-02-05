using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    [Header("< 1号Boss相关设置 >")]
    public float maxRange = 30;
    public float minRange = 2;
    public float speed = 2;
    public float closeWaitTime = 3000;
    public ParticleSystem[] shootList = null;
    public float lookAtLerp = 3;
    public Transform bossPoint = null;

    [Header("< 1号Boss旋转子弹相关设置 >")]
    public ParticleSystem[] rotateBullet1List = null;
    public Transform rotateShoot1 = null;
    public int rotateCount = 3;
    public int rotateCircle = 50;
    public float rotateShootInterval = 200;
    public float rotateBullet1AttackInterval = 1500;
    public float rotateSpeed = 50;
    public float rotateBulletSpeed = 4;

    [Header("< 1号Boss旋转散射子弹相关设置 >")]
    public ParticleSystem[] rotateBullet2List = null;
    public Transform rotateShoot2 = null;
    public float rotateBullet2AttackInterval = 1000;


    [HideInInspector]
    public float duration1 = 0;


    float dir = 1;
    Vector3 targetPos = new Vector3();
    int id_DisSqr = Animator.StringToHash("DisSqr");
    int id_Hp = Animator.StringToHash("Hp");
    private void Start()
    {
        targetPos.y = transform.position.y;
        bossPoint.SetParent(SceneManager.Instance.transform);

        //死亡的时候不通过dying进入到播放死亡动画
        needPlayDying = false;

        //阶段二第一种子弹配置
        duration1 = rotateShootInterval * rotateCircle;
        foreach (ParticleSystem system in rotateBullet1List)
        {
            var main = system.main;
            main.startSpeed = rotateBulletSpeed;
            main.startLifetime = 2 * (8 / rotateBulletSpeed);
            main.duration = duration1 / 1000;
            var emission = system.emission;
            var burst = emission.GetBurst(0);
            burst.count = rotateCount;
            burst.cycleCount = rotateCircle;
            burst.repeatInterval = rotateShootInterval / 1000;
            emission.SetBurst(0, burst);
        }

        //阶段二第二种子弹配置

    }

    private void Update()
    {
        if (!died)
        {
            if (hp <= 0)
            {
                Dying();
                return;
            }
            BaseUpdate();
            anim.SetFloat(id_DisSqr, targetSqrDis);
        }
    }

    //protected new void Update()
    //{
    //    base.Update();
    //    anim.SetFloat(id_DisSqr, targetSqrDis);
    //}

    void Shoot()
    {
        for(int i = 0; i < shootList.Length; i++)
        {
            shootList[i].Play();
        }
    }

    public void BossPointMove()
    {
        if (agentTarget != null && bossPoint != null)
        {
            targetPos.x = agentTarget.position.x;
            targetPos.z = agentTarget.position.z;
            Vector3 off = targetPos - bossPoint.position;
            float disSqr = off.sqrMagnitude;
            if (disSqr >= maxRange * maxRange)
            {
                dir = 1;
            }
            else if (disSqr <= (minRange * minRange))
            {
                dir = -1;
            }
            bossPoint.Translate(off.normalized * Time.deltaTime * speed * dir);
        }
    }


    public override void InStage(Transform target, Transform spwanPoint)
    {
        Init(target, spwanPoint);


        if (meshRenderer != null)
        {
            anim.Play("ready");
            startColorRange = meshRenderer.material.GetFloat("_colorrange");
        }

        SceneManager.Instance.gameCamera.AddGroup(transform,0.6f,2f);
    }

    private void OnParticleCollision(GameObject other)
    {
        OnHit(other,4);
        anim.SetFloat(id_Hp, hp);
    }
}
