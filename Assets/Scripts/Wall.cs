using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //private void OnParticleCollision(GameObject other)
    //{
    //    Bullet bullet = other.GetComponent<Bullet>();
    //    //bullet.Penetrate();
    //}

    private void OnParticleTrigger()
    {
        Debug.Log("near2y");
    }


}
