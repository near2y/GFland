using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    BulletData bulletData = null;

    private void Awake()
    {
        bulletData = ConfigerManager.Instance.FindData<BulletData>(CFG.TABLE_BULLET);

    }

    public GameObject GetEffect(int id)
    {
        BulletBase data = bulletData.FindByID(id);
        GameObject effect = ObjectManager.Instance.InstantiateObject(data.PrefabPath);
        return effect;
    }

    public void ReleaseEffect(GameObject effect)
    {
        ObjectManager.Instance.ReleaseObject(effect);
    }
}
