using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [Header("< 蜘蛛相关设置 >")]
    //是否为近战
    public bool melee;
    public Vector2 startRange = new Vector2(0.6f, 0.8f);


    [HideInInspector]
    public int id_Melee = Animator.StringToHash("Melee");
    [HideInInspector]
    public int id_SpwanType = Animator.StringToHash("SpwanType");



    public override void InStage(Transform target, Transform spwanPoint)
    {
        Init(target, spwanPoint);
        //攻击类型
        float attcakType = melee ? 1 : 0;
        anim.SetFloat(id_Melee, attcakType);
        //登场方式
        if (spwanPoint.tag == SpawnPointTag.Climb)
        {
            //爬上来的
            anim.SetFloat(id_SpwanType, 1);
            //从哪一帧开始播放登场动画
            anim.Play(EnemyState.InStage, 0, Random.Range(startRange.x, startRange.y));
        }
        else if(spwanPoint.tag == SpawnPointTag.Full)
        {
            //落下来的
            anim.SetFloat(id_SpwanType, 0.5f);
            anim.Play(EnemyState.InStage);
        }
        attackTimer = attackInterval;
        if (meshRenderer != null) 
        {
            startColorRange = meshRenderer.material.GetFloat("_colorrange");
        }
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
        } 
    }


    private void OnParticleCollision(GameObject other)
    {
        OnHit(other);
    }
}
