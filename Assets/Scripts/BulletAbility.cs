using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbility
{
    /// <summary>
    /// 正常模式，直接消除
    /// </summary>
    public static void Stop(ref ParticleSystem.Particle particle)
    {
        particle.remainingLifetime = 0;
    }

    /// <summary>
    /// 反弹
    /// </summary>
    /// <param name="particle"></param>
    public static void Bounce(ref ParticleSystem.Particle particle)
    {
        particle.velocity = -particle.velocity;
    }

    /// <summary>
    /// 直接穿透
    /// </summary>
    /// <param name="particle"></param>
    public static void Penetrate(ref ParticleSystem.Particle particle)
    {

    }

    /// <summary>
    /// 弹射
    /// </summary>
    /// <param name="particle"></param>
    /// <param name=""></param>
    public static void Diffraction(ref ParticleSystem.Particle particle,Transform target,float Speed)
    {

    }


}



public enum BulletAbilityType
{
    Bounce,
    Diffraction,
    Stop,
    PenetrateTrajatory,
}
