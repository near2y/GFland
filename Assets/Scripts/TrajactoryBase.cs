using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajactoryBase : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem system;
    [HideInInspector]
    public BulletProp bulletProp = null;
    [HideInInspector]
    public Test test;
    protected List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();

    public PenetrateTrajactory pTrajatory = null;
    public DiffractionTrajactory dTrajatory = null;


    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        bulletProp = new BulletProp(system);
    }


}
