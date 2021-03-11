using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class EquipmentDataBase:IJsonData
{
    int id;
    public string name;
    /// <summary>
    /// 所属部位
    /// </summary>
    public EquipmentPart part;
    /// <summary>
    /// 品质
    /// </summary>
    public int quality;
    /// <summary>
    /// 等级上限
    /// </summary>
    public int maxLevel;
    /// <summary>
    /// 合成需求装备数量
    /// </summary>
    public int compoundNum;
    /// <summary>
    /// 合成后装备id
    /// </summary>
    public int compoundNextID;
    /// <summary>
    /// 枪支技能id
    /// </summary>
    public int gunSkill;
    /// <summary>
    /// 附加技能
    /// </summary>
    public int addSkill1, addSkill2, addSkill3, addSkill4, addSkill5;
    /// <summary>
    /// 升级需求材料id
    /// </summary>
    public int levelUpNeedRes;
    /// <summary>
    /// 升级需求材料数量
    /// </summary>
    public List<int> levelUpNeedResNum;
    /// <summary>
    /// 升级需要金币的基数
    /// </summary>
    public float levelUpNeedCoinBase;
    /// <summary>
    /// 升级需要金币的系数
    /// </summary>
    public float levelUpNeedCoinRatio;
    /// <summary>
    /// 1级该装备分解返回金币数
    /// </summary>
    public int sale;
    /// <summary>
    /// 常规属性
    /// </summary>
    public CombatAbility baseAbility;

    public int ID { get { return id; } }

    void SetProp(string prop)
    {
        string[] datas = prop.Split('|');
    }



    void SetAbility(string propName,object value)
    {
        baseAbility.GetType().GetField(propName).SetValue(baseAbility, value);    
    }

    public float GetAbility(string propName)
    {
        return (float)baseAbility.GetType().GetField(propName).GetValue(baseAbility);
    }

    public void Init(JSONNode jsData)
    {
        id = jsData["装备id"];
        name = jsData["装备名"];
        int partIndex = jsData["所属部位"];
        part = (EquipmentPart)partIndex;
        quality = jsData["阶级"];
        maxLevel = jsData["等级上限"];
        compoundNum = jsData["合成需求装备数量"];
        compoundNextID = jsData["合成后装备"];
        gunSkill = jsData["枪支技能"];
        addSkill1 = jsData["附加技能1"];
        addSkill2 = jsData["附加技能2"];
        addSkill3 = jsData["附加技能3"];
        addSkill4 = jsData["附加技能4"];
        addSkill5 = jsData["附加技能5"];
        levelUpNeedRes = jsData["升级需求材料id"];
        levelUpNeedResNum = new List<int>();
        string needNumStr = jsData["升级需求材料数量"];
        string[] needNums = needNumStr.Split('|');
        for (int i = 0; i < maxLevel - 1; i++)
        {
            int index = Mathf.Min(i, needNums.Length - 1);
            int need = int.Parse(needNums[index]);
            levelUpNeedResNum.Add(need);
        }
        string needCoinStr = jsData["升级需求金币数量"];
        string[] needCoinStrs = needCoinStr.Split('|');
        levelUpNeedCoinBase = float.Parse(needCoinStrs[0]);
        levelUpNeedCoinRatio = float.Parse(needCoinStrs[1]);

        baseAbility = new CombatAbility();
        //string prop1 = jsData["常规属性1"];
        //SetProp(prop1);
        //string prop2 = jsData["常规属性2"];
        //SetProp(prop2);
        sale = jsData["1级该装备分解返回金币数"];
    }
}

public enum EquipmentPart
{
    Weapon = 1,
    Helmet,
    Cuirass,
    Ornament,
}
