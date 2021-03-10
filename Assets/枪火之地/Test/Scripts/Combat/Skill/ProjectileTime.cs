using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTime
{
    //动画中发射弹道的时刻
    public float nomalizeMoment;
    //弹道Id
    public int projectileID;

    public ProjectileTime(string data)
    {
        var datas = data.Split('|');
        nomalizeMoment = float.Parse(datas[0]);
    }
}
