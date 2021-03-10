using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamageTime 
{
    public float nomalizeMoment;
    public float damageProp;


    public HitDamageTime(string data)
    {
        var datas = data.Split('|');
        nomalizeMoment = float.Parse(datas[0]);
        damageProp = float.Parse(datas[1]);
    }

    public bool OnHit()
    {
        bool res = false;
        //TODO
        return res;
    }

}
