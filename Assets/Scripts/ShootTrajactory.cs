using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrajactory : TrajactoryBase
{

    void Start()
    {
        bulletProp.main.loop = true;
    }

    public bool InShoot
    {
        set
        {
            if (value)
            {
                system.Play();
            }
            else
            {
                system.Stop();
            }
        }
    }

    private void OnParticleTrigger()
    {
        int numExit = system.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
        for(int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = enterParticles[i];

            if (pTrajatory != null)
            {
                pTrajatory.Penetrate(p.position,p.velocity);
            }

            if(dTrajatory != null)
            {
                int t = Random.Range(0, test.targets.Length);
                dTrajatory.Diffraction(p.position,test.targets[t]);
            }

            p.remainingLifetime = 0;
            enterParticles[i] = p;

        }
        system.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
    }

    private void OnParticleCollision(GameObject other)
    {
        //给粒子系统设置当前碰撞对象为trigger对象
        system.trigger.SetCollider(0, other.transform);
    }

}
