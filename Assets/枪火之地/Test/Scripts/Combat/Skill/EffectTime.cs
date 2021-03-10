using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTime
{
    public float nomalizeMoment;
    public int effectID;

    public EffectTime(string data)
    {
        var datas = data.Split('|');
        nomalizeMoment = float.Parse(datas[0]);
        effectID = int.Parse(datas[1]);
    }
}
