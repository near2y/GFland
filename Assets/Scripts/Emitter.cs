using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [Header("< 基础对象 >")]
    public ParticleSystem particleSystem;

    [Header("< 射击相关参数 >")]
    public float speed = 5;
    public float shootFrequency = 200;

    [Header("< 游戏中相关参数动态展示 >")]
    public  bool inShoot = false;
    public Vector3 shootDir = new Vector3(0, 0, 1);
    public Vector3 shootPos = new Vector3(0, 0, 0);

    float shootFrequencyTimer = 0;
    ParticleSystem.Particle[] particles;
    ParticleSystem.MainModule main;
    ParticleSystem.VelocityOverLifetimeModule velocity;
    List<ParticleCollisionEvent> collisionEvents;

    private void Awake()
    {
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        main = particleSystem.main;
        velocity = particleSystem.velocityOverLifetime;
        collisionEvents = new List<ParticleCollisionEvent>();


    }

    private void Update()
    {
        RefreshTimer();
        GetInput();
        Behavior();
    }

    void RefreshTimer()
    {
        shootFrequencyTimer += Time.deltaTime*1000;
    }

    void Behavior()
    {
        if (inShoot)
        {
            Shoot();
        }
    }


    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inShoot = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            inShoot = false;
        }
    }

    void Shoot()
    {
        if (shootFrequencyTimer >= shootFrequency)
        {
            shootFrequencyTimer = 0;
            StartCoroutine(SetBullet());
        }
    }

    IEnumerator SetBullet()
    {
        particleSystem.Play();
        yield return new WaitForEndOfFrame();
        int count = particleSystem.GetParticles(particles);
        particles[count - 1].velocity = shootDir.normalized * speed;
        particleSystem.SetParticles(particles, count);
    }

    IEnumerator SetBullet(Vector3 pos)
    {
        yield return new WaitForEndOfFrame();
        particleSystem.Play();
        yield return new WaitForEndOfFrame();
        int count = particleSystem.GetParticles(particles);
        particles[count - 1].velocity = shootDir.normalized * speed;
        particles[count - 1].position = pos;
        particleSystem.SetParticles(particles, count);

    }


    Vector3 collisionPos = new Vector3();
    public Vector3 outPos;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(particleSystem.GetSafeCollisionEventSize());
        particleSystem.trigger.SetCollider(0, other.GetComponent<Collider>());
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        //outPos = collisionEvents[numCollisionEvents - 1].intersection+ collisionEvents[numCollisionEvents - 1].velocity.normalized*0.6f;
        StartCoroutine(SetBullet(other.transform.position));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("near2y");

    }
}
