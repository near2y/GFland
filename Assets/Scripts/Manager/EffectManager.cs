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
            effect.transform.position = trans.position + data.locationOff;
        }

        if (data.duration > 0)
        {
            GameManager.Instance.mono.StartCoroutine(AutoRelease(effect, data.duration));
        }
        return effect;
    }

    public GameObject GetEffect(int id)
    {
        EffectDataBase data = GameManager.Instance.effectJson.GetDataByID(id);
        GameObject effect = ObjectManager.Instance.InstantiateObject(data.prePath);
        return effect;
    }

    public GameObject GetEffect(string path,float duration)
    {
        GameObject effect = ObjectManager.Instance.InstantiateObject(path);
        effect.transform.SetParent(effectParent);
        effect.SetActive(true);
        if (duration > 0)
        {
            GameManager.Instance.mono.StartCoroutine(AutoRelease(effect, duration));
        }
        return effect;
    }

    public GameObject GetEffect(string path,float duration,Transform parent)
    {
        GameObject effect = GetEffect(path, duration);
        effect.transform.position = parent.position;
        effect.transform.SetParent(parent);
        return effect;
    }

    public GameObject GetEffect(string path,float duration,Transform parent,Vector3 off)
    {
        GameObject effect = GetEffect(path, duration);
        effect.transform.SetParent(parent);
        effect.transform.localPosition = off;
        return effect;
    }

    IEnumerator AutoRelease(GameObject effect,float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseEffect(effect);
    }

    

    public void ReleaseEffect(GameObject effect)
    {
        ObjectManager.Instance.ReleaseObject(effect,recycleParent:false);
    }
}
