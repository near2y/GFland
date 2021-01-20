using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateTrajactory : TrajactoryBase
{
    public ParticleSystem parentSystem;
    void Start()
    {
        bulletProp.main.loop = false;
        var collision = system.collision;
        collision.enabled = false;
    }

    public void Penetrate(Vector3 position,Vector3 velocity)
    {
        transform.position = position;
        transform.rotation = parentSystem.transform.rotation;
        system.Play();
    }

}
