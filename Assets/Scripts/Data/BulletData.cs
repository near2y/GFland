using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[System.Serializable]
public class BulletData: ExcelBase
{
#if UNITY_EDITOR
    public override void Construction()
    {
        AllBulletList = new List<BulletBase>();
        for (int i = 0; i < 3; i++)
        {
            BulletBase bullet = new BulletBase();
            bullet.Id = i + 1;
            bullet.Name = "全BUFF" + i;
            bullet.PrefabPath = "near2y";
            AllBulletList.Add(bullet);
        }
    }
#endif

    public override void Init()
    {
        AllBulletDic.Clear();
        for (int i = 0; i < AllBulletList.Count; i++)
        {
            AllBulletDic.Add(AllBulletList[i].Id, AllBulletList[i]);
        }
    }



    /// <summary>
    /// 通过ID查找怪物数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BulletBase FindByID(int id)
    {
        return AllBulletDic[id];
    }

    [XmlIgnore]
    public Dictionary<int, BulletBase> AllBulletDic = new Dictionary<int, BulletBase>();

    [XmlElement("AllBulletList")]
    public List<BulletBase> AllBulletList { get; set; }

}

[System.Serializable]
public class BulletBase
{
    [XmlAttribute("Id")]
    public int Id { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlAttribute("PrefabPath")]
    public string PrefabPath { get; set; }

}

