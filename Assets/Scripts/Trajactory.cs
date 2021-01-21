using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajactory : MonoBehaviour
{
    public ParticleSystem system;
    public BulletProp bulletProp = null;
    //发射器
    public Emitter test;

    //Trajactory subTrajactory = null;
    BulletAbilityType abilityType = BulletAbilityType.Stop;

    public Trajactory pTrajactory= null;
    public Trajactory dTrajactory = null;

    List<ParticleCollisionEvent> collisionEvents;
    

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
#if UNITY_EDITOR
        if (system == null)
        {
            Debug.LogError("对象：" + gameObject.name + "  没有粒子系统，检查！");
        }
#endif
        bulletProp = new BulletProp(system);
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if (bulletProp == null)
        {
            Debug.LogError("对象：" + gameObject.name + "  没有初始化bulletProp,检查！");
        }
#endif
    }

    public void Init(Emitter emitter, 
        float bulletSpeed,int shootCount,float bulletFrequency,float shootFrequency)
    {
        test = emitter;
        bulletProp.BulletSpeed = bulletSpeed;
        bulletProp.ShootCount = shootCount;
        bulletProp.BulletFrequency = bulletFrequency;
        bulletProp.ShootFrequency = shootFrequency;
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

    public void Spwan(Vector3 position,Vector3 targetPos,int count = 1)
    {
        system.Stop();
        bulletProp.ShootCount = count;
        bulletProp.Loop = false;
        transform.position = position;
        transform.LookAt(targetPos);
        system.Play();
    }

    private void OnParticleCollision(GameObject other)
    {

        Diffraction(other);
        Penetrate(other);
    }

    //弹射
    void Diffraction(GameObject other)
    {
        if (dTrajactory == null) return;
        Collider collider = other.GetComponent<Collider>();
        int t = Random.Range(0, test.targets.Length);
        float dis = Mathf.Max(collider.bounds.size.x, collider.bounds.size.z) * 0.6f;
        Vector3 point = other.transform.position + (test.targets[t].position - other.transform.position).normalized * dis;
        dTrajactory.Spwan(point, test.targets[t].position);
    }

    //穿透
    void Penetrate(GameObject other)
    {
        if (pTrajactory == null ) return;
        int numCollisionEvents = system.GetCollisionEvents(other, collisionEvents);
        Collider collider = other.GetComponent<Collider>();
        float dis = Mathf.Max(collider.bounds.size.x, collider.bounds.size.z) * 0.6f;
        for(int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 point = other.transform.position + collisionEvents[i].velocity.normalized * dis;
            Vector3 penetratePos = point + collisionEvents[i].velocity.normalized * dis;
            pTrajactory.Spwan(point, penetratePos);
        }
    }
}
