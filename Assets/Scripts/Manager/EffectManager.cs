using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    BulletData bulletData = null;

    // Start is called before the first frame update
    void Start()
    {
        bulletData = ConfigerManager.Instance.FindData<BulletData>(CFG.TABLE_BULLET);
        BulletBase data1 = bulletData.FindByID(4001);
        Debug.Log("near2y" + data1.Name);
    }


}
