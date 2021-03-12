using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDataBase : IJsonData
{
    public int id;
    /// <summary>
    /// 预制体完整路径
    /// </summary>
    public string prePath;
    /// <summary>
    /// 角度目标中心
    /// </summary>
    public int rotCenter;
    /// <summary>
    /// 坐标目标中心
    /// </summary>
    public int posCenter;
    /// <summary>
    /// 挂点位置
    /// </summary>
    public string location;
    /// <summary>
    /// 挂点偏移坐标
    /// </summary>
    public Vector3 locationOff;
    /// <summary>
    /// 特效是否跟随
    /// </summary>
    public bool isFollow;
    /// <summary>
    /// 特效大小
    /// </summary>
    public float scale;
    /// <summary>
    /// 特效持续时间
    /// </summary>
    public float duration;
    /// <summary>
    /// 播放规则
    /// </summary>
    public EffectPlayType playType;


    public int ID { get { return id; } }

    public void Init(JSONNode jsData)
    {
        id = jsData["技能id"];
        prePath = jsData["特效预制体位置"];
        posCenter = jsData["坐标目标中心"];
        rotCenter = jsData["角度目标中心"];
        location = jsData["挂点位置"];
        string offData = jsData["挂点偏移坐标"];
        string[] off = offData.Split('|');

        locationOff = new Vector3(float.Parse(off[0]), float.Parse(off[1]), float.Parse(off[2]));
        isFollow = jsData["特效是否跟随"] == "1";
        scale = jsData["大小"];
        playType = (EffectPlayType)int.Parse(jsData["播放规则"]);
        duration = jsData["特效持续时间"];
    }
}

public enum EffectPlayType
{
    Once = -1,
    Loop = 0
}
