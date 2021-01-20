using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffractionTrajactory : TrajactoryBase
{
    public ParticleSystem parentSystem;

    void Start()
    {
        bulletProp.main.loop = false;
        var collision = system.collision;
        collision.enabled = false;
        system.trigger.SetCollider(0, transform);
    }

    public void Diffraction(Vector3 position,Transform target)
    {
        transform.position = position;
        transform.LookAt(target);
        //system.trigger.SetCollider(0, target);
        system.Play();
    }

    private void OnParticleTrigger()
    {
        int numExit = system.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, enterParticles);
        for (int i = 0; i < numExit; i++)
        {
            Debug.LogError("near2y");
        }
        //system.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, enterParticles);

    }
}
