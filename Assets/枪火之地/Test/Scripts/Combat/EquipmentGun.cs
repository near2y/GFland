using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGun : MonoBehaviour
{

    public GameObject test;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(test.GetComponent<AttackRole>().m_CombatAbility.currentHp);
        }        
    }
}
