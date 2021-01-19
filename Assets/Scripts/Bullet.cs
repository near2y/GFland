using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    public int penetrateCount = 5;
    ParticleSystem particle = null;
    public Transform[] targets = null;
    public float speed = 1;
    Vector3 direction;

    Dictionary<Particle, Transform> particleTargetDic = new Dictionary<Particle, Transform>();
    Dictionary<Particle, int> particleLaunchNumDic = new Dictionary<Particle, int>();
    private void Start()
    {
        particle = this.GetComponentInChildren<ParticleSystem>();
        direction = new Vector3();
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Particle[] ps = new Particle[particle.particleCount];
            //int pCount = particle.GetParticles(ps);
            //for (int i = 0; i < pCount; i++)
            //{
            //    if (!particleTargetDic.ContainsKey(ps[i]))
            //    {
            //        particleTargetDic.Add(ps[i], targets[0]);
            //    }
            //}
            //particle.SetParticles(ps, pCount);
            particle.Play();

            //dir

            //speed

            //
        }
        Debug.Log(particle.particleCount);
        //foreach (Particle item in particleTargetDic.Keys)
        //{
        //    //找到目标
        //    Vector3 off = particleTargetDic[item].position - item.position;
        //    direction = off.normalized;
        //    float distance = off.magnitude;
        //    item.velocity = speed * direction;
        //    ps[i].remainingLifetime = distance / speed;
        //}
    }

}
