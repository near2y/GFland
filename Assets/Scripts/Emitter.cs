using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [Header("< 基础对象 >")]
    public ParticleSystem particleSystem = null;
    public ParticleSystem.MainModule particleMain;
    public Transform[] targets;

    [Header("< 射击相关参数 >")]
    public float speed = 5;
    public float shootFrequency = 200;
    public BulletAbilityType prop = BulletAbilityType.Stop;
    public short trajectoryCount = 1;

    [Header("< 游戏中相关参数动态展示 >")]
    public  bool inShoot = false;
    public Vector3 shootDir = new Vector3(0, 0, 1);
    public Vector3 shootPos = new Vector3(0, 0, 0);

    float shootFrequencyTimer = 0;
    ParticleSystem.Particle[] particles;
    List<ParticleSystem.Particle> exitParticles = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        particleMain = particleSystem.main;
        for(int i = 0; i < targets.Length; i++)
        {
            particleSystem.trigger.SetCollider(i, targets[i]);
        }
    }

    private void Start()
    {
        SetTrajectory(trajectoryCount);
    }

    private void Update()
    {
        //int count = particleSystem.GetParticles(particles);
        //Debug.Log(count);
        particleMain.startSpeed = speed;
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

    void SetTrajectory(short count)
    {
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
        emission.GetBursts(bursts);
        bursts[0].minCount = bursts[0].maxCount = count;
        emission.SetBursts(bursts);
    }

    void Shoot()
    {
        if (shootFrequencyTimer >= shootFrequency)
        {
            shootFrequencyTimer = 0;
            particleSystem.Play();
        }
    }

    private void OnParticleTrigger()
    {
        // 获取与此帧的触发条件匹配的粒子
        int numExit = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, exitParticles);
        if (numExit > 0)
        {
            switch (prop)
            {
                case BulletAbilityType.Bounce:
                    // 反弹
                    for (int i = 0; i < numExit; i++)
                    {
                        ParticleSystem.Particle p = exitParticles[i];
                        //p.velocity = -p.velocity;
                        //exitParticles[i] = p;

                        BulletAbility.Bounce(ref p);
                        exitParticles[i] = p;
                    }
                    break;
                case BulletAbilityType.Diffraction:
                    //衍射
                    for (int i = 0; i < numExit; i++)
                    {
                        ParticleSystem.Particle p = exitParticles[i];
                        p.velocity = (targets[1].position - p.position).normalized * speed;
                        exitParticles[i] = p;
                    }
                    break;
                case BulletAbilityType.Stop:
                    //抹掉
                    for (int i = 0; i < numExit; i++)
                    {
                        ParticleSystem.Particle p = exitParticles[i];
                        p.remainingLifetime = 0;
                        exitParticles[i] = p;
                    }
                    break;
            }
            // 将修改后的粒子重新分配回粒子系统
            particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, exitParticles);
            exitParticles.Clear();
        }
    }
}

