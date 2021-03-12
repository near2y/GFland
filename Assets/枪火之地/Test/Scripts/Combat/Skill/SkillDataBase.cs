using System.Collections.Generic;
using SimpleJSON;


public class SkillDataBase:IJsonData
{
    int id;
    public string name;
    public string icon;
    public string describe;
    public int skillType;
    public string passiveType;
    public float range;
    public float coolTime;
    public int attackType;
    public int centerPoint;
    public SkillForm form;
    public string animateName;
    public List<HitDamageTime> hitDamageTimes;
    public List<ProjectileTime> projectileTimes;
    public List<EffectTime> effectTimes;
    public List<BuffTime> buffTimes;
    public float addEnergy;
    public float chantTime;

    public int ID { get { return id; } }

    public void Init(JSONNode jsData)
    {
        id = jsData["技能id"];
        name = jsData["技能名"];
        icon = jsData["技能图标"];
        describe = jsData["技能描述"];
        skillType = jsData["技能类别"];
        passiveType = jsData["被动技能类型"];
        range = jsData["施法距离"];
        coolTime = jsData["冷却时间"];
        attackType = jsData["攻击方式"];
        centerPoint = jsData["作用中心"];
        form = new SkillForm(jsData["作用形状"].ToString());
        animateName = jsData["施法动作"];
        SetListData<HitDamageTime>(jsData, "伤害帧", ref hitDamageTimes, (string data) => { return new HitDamageTime(data); });
        SetListData<ProjectileTime>(jsData, "弹道帧", ref projectileTimes, (string data) => { return new ProjectileTime(data); });
        SetListData<EffectTime>(jsData, "特效帧", ref effectTimes, (string data) => { return new EffectTime(data); });
        SetListData<BuffTime>(jsData, "功能帧", ref buffTimes, (string data) => { return new BuffTime(data); });
        addEnergy = jsData["增加能量"];
        chantTime = jsData["吟唱时间"];
    }

    void SetListData<T>(JSONNode jsNode, string name, ref List<T> list,NewItem<T> newItem)
    {
        string data = jsNode[name];
        string[] dataList = data.Split('&');
        list = new List<T>();
        for (int i = 0; i < dataList.Length; i++)
        {
            if (dataList[i] != "0")
            {
                list.Add(newItem.Invoke(dataList[i]));
            }
        }
    }
    delegate T NewItem<T>(string data);
}



