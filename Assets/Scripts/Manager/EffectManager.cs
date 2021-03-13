using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    EffectData effectData = null;
    Transform effectParent = null;

    public EffectManager(EffectData data,Transform parent)
    {
        effectData = data;
        effectParent = parent;
    }

    public GameObject GetEffect(int id,Transform trans)
    {
        EffectDataBase data = GameManager.Instance.effectJson.GetDataByID(id);
        GameObject effect = ObjectManager.Instance.InstantiateObject(data.prePath);
        effect.SetActive(true);
        if (data.isFollow)
        {
            effect.transform.SetParent(trans);
            effect.transform.localPosition = data.locationOff;
        }
        else
        {
            effect.transform.SetParent(effectParent);
            effect.transform.position = trans.position + data.locationOff;
        }


        if (data.duration > 0)
        {
            GameManager.Instance.mono.StartCoroutine(AutoRelease(effect, data.duration));
        }

        effect.transform.localScale = Vector3.Scale(effect.transform.localScale, Vector3.one * data.scale);
        return effect;
    }

    IEnumerator AutoRemoveFollow(float time,GameObject effect)
    {
        yield return new WaitForSeconds(time);
        effect.transform.SetParent(effectParent);
    }

    public GameObject GetEffect(int id)
    {
        EffectDataBase data = GameManager.Instance.effectJson.GetDataByID(id);
        GameObject effect = ObjectManager.Instance.InstantiateObject(data.prePath);
        
        return effect;
    }

    IEnumerator AutoRelease(GameObject effect,float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseEffect(effect);
    }

    

    public void ReleaseEffect(GameObject effect)
    {


        //TODO 这里可以考虑优化   是否每次进行收回
        ObjectManager.Instance.ReleaseObject(effect,recycleParent:true);
    }
}
