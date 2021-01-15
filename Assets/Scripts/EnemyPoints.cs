using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoints : MonoBehaviour
{

    Dictionary<int, Transform> allPointsDic = new Dictionary<int, Transform>();

    private void Awake()
    {
        Transform[] transList = transform.GetComponentsInChildren<Transform>();
        for(int i = 1; i < transList.Length; i++)
        {
            int id = int.Parse(transList[i].name);
#if UNITY_EDITOR
            if (allPointsDic.ContainsKey(id))
            {
                Debug.LogError("有相同名字的怪物出生点，请检查！");
            }
#endif
            allPointsDic[id] = transList[i];
        }

        Debug.Log(allPointsDic);
    }


    public Transform FindPointByID(int id)
    {
//#if UNITY_EDITOR
        if (!allPointsDic.ContainsKey(id))
        {
            Debug.LogError("没有找到ID:" + id + "的怪物生成点");
        }
//#endif
        return allPointsDic[id];
    }

}
