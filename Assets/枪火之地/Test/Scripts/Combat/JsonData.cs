using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData<T> where T : IJsonData,new()
{
    string m_url;
    Dictionary<int, T> m_dataDic;

    public JsonData(string url)
    {
        m_url = url;
        m_dataDic = new Dictionary<int, T>();
        var js = Method.GetJsonByUrl(m_url);
        for(int i = 0; i < js.Count; i++)
        {
            T data = new T();
            data.Init(js[i]);
            m_dataDic.Add(data.ID,data);
        }
    }

    public T GetDataByID(int id)
    {
        if (m_dataDic.ContainsKey(id))
        {
            return m_dataDic[id];
        }
        else
        {
            Debug.LogError(m_url + "：地址的Json中没有找到Id：" + id);
            return default(T);
        }
    }
}
