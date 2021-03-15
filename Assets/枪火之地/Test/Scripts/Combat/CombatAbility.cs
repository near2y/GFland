using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAbility 
{
    /// <summary>
    /// 血量上限
    /// </summary>
    public float hpBase = 0;
    /// <summary>
    /// 基础攻击力
    /// </summary>
    public float attBase = 0;
    /// <summary>
    /// 暴击率
    /// </summary>
    public float criProp = 0;
    /// <summary>
    /// 暴击伤害加成
    /// </summary>
    public float criHurAddProp = 0;
    /// <summary>
    /// 闪避率
    /// </summary>
    public float evaProp = 0;
    /// <summary>
    /// 免伤率
    /// </summary>
    public float harmReduceProp = 0;
    /// <summary>
    ///  生命回复加成
    /// </summary>
    public float cureAddProp = 0;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float spe = 0;
    /// <summary>
    /// 攻速
    /// </summary>
    public float attSpeProp = 0;
    /// <summary>
    /// 金币加成
    /// </summary>
    public float coinAddProp = 0;
    /// <summary>
    /// 能量获取加成
    /// </summary>
    public float energyAddProp = 0;
    /// <summary>
    /// 当前血量
    /// </summary>
    public float currentHp = 0;
    /// <summary>
    /// 当前所属 physics layer层
    /// </summary>
    public Int32 layer;
    /// <summary>
    /// 所属对象
    /// </summary>
    public GameObject gameObject;
    /// <summary>
    /// 阵营名字
    /// </summary>
    public string campName; 

    public void Reset()
    {
        hpBase = 0;
        attBase = 0;
        criProp = 0;
        criHurAddProp = 0;
        evaProp = 0;
        harmReduceProp = 0;
        cureAddProp = 0;
        spe = 0;
        attSpeProp = 0;
        coinAddProp = 0;
        energyAddProp = 0;
        currentHp = 0;
        layer = LayerMask.NameToLayer("Default");
    }
}
