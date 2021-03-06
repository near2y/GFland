using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class WaveJson 
{
    public List<WaveBase> AllWaveList { get; set; }

    const string str_id = "ID";
    const string str_WaveID = "波次的ID";
    const string str_DelayTime = "延迟激活时间";
    const string str_PointID = "落地点ID";
    const string str_EnemyID = "生成怪物ID";
    const string str_EnemyName = "生成怪物名字";
    const string str_SpwanCount = "当前点生成怪物的总量";
    const string str_SpwanInterval = "当前点生成怪物的间隔";
    const string str_NextWaveActiveNum = "下个波次激活条件-剩余数量";
    const string str_MaxTime = "下个波次激活条件-最长时间";
    const string str_SceneName = "场景名称";

    public WaveJson(string url)
    {
        AllWaveList = new List<WaveBase>();
        var js = JSON.Parse(ResourceManager.Instance.LoadResource<Object>(url).ToString());
        for(int i = 0; i < js.Count; i++)
        {
            WaveBase wave = new WaveBase();
            wave.ID = js[i][str_id];
            wave.WaveID = js[i][str_WaveID];
            wave.DelayTime = js[i][str_DelayTime];
            wave.PointID = js[i][str_PointID];
            wave.EnemyID = js[i][str_EnemyID];
            wave.EnemyName = js[i][str_EnemyName];
            wave.SpwanCount = js[i][str_SpwanCount];
            wave.SpwanInterval = js[i][str_SpwanInterval];
            wave.NextWaveActiveNum = js[i][str_NextWaveActiveNum];
            wave.MaxTime = js[i][str_MaxTime];
            wave.SceneName = js[i][str_SceneName];
            AllWaveList.Add(wave);
        }
    }
}

public class WaveBase
{
    /// <summary>
    /// 唯一ID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// 归属于波次的ID
    /// </summary>
    public int WaveID { get; set; }

    /// <summary>
    /// 当前波次开始，延迟多少激活该点
    /// </summary>
    public float DelayTime { get; set; }

    /// <summary>
    /// 生成点ID
    /// </summary>
    public int PointID { get; set; }

    /// <summary>
    /// 生成怪物的ID
    /// </summary>
    public int EnemyID { get; set; }

    /// <summary>
    /// 生成怪物的名字
    /// </summary>
    public string EnemyName { get; set; }

    /// <summary>
    /// 同一个点同一个波次中总共会生成的怪物数量
    /// </summary>
    public int SpwanCount { get; set; }

    /// <summary>
    /// 同一个点同一波次中生成怪物间隔
    /// </summary>
    public float SpwanInterval { get; set; }

    /// <summary>
    /// 剩余怪物数量小于该值激活下一波
    /// </summary>
    public int NextWaveActiveNum { get; set; }

    /// <summary>
    /// 持续时间大于该值激活下一波
    /// </summary>
    public float MaxTime { get; set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string SceneName { get; set; }
}




