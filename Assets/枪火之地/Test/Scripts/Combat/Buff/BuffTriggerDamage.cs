using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerDamage:MonoBehaviour
{
    bool couldDamage = false;

    public void Init()
    {
        couldDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
