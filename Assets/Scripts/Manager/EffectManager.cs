using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    EffectData effectData = null;

    private void Awake()
    {
        effectData = ConfigerManager.Instance.FindData<EffectData>(CFG.TABLE_BULLET);

    }

    public GameObject GetEffect(int id)
    {
        EffectBase data = effectData.FindByID(id);
        GameObject effect = ObjectManager.Instance.InstantiateObject(data.PrefabPath);
        return effect;
    }

    public void ReleaseEffect(GameObject effect)
    {
        ObjectManager.Instance.ReleaseObject(effect);
    }
}
