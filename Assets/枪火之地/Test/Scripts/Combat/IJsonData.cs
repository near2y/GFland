using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public interface IJsonData
{
    int ID { get; }
    
    void Init(JSONNode jsData);

}
