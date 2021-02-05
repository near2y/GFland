using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    private float shootTimer = 0;

    public override void InStage(Transform target, Transform spwanPoint)
    {
        Init(target, spwanPoint);
        completeInStage = false;
        //登场
        anim.Play(EnemyState.InStage);
        attackTimer = emitter.shootFrequency;
        shootTimer = 0;
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
            shootTimer += Time.deltaTime * 1000;
            if (completeInStage && shootTimer >= emitter.shootFrequency)
            {
                shootTimer = 0;
                emitter.SingleShot(emitter.bulletPos.position, emitter.bulletPos.position + emitter.bulletPos.forward);
            }
            BaseUpdate();
        }
    }



    new public void Dying()
    {
        emitter.Stop();
        base.Dying();
    }

    private void OnParticleCollision(GameObject other)
    {
        OnHit(other);
    }

}
