using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTime 
{
    float nomalizeMoment;
    int buffID;

    public BuffTime(string data)
    {
        var datas = data.Split('|');
        nomalizeMoment = float.Parse(datas[0]);
        buffID = int.Parse(datas[1]);
    }

}
