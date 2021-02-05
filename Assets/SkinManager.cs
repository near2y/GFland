using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{

    List<SkinPlayer> skinList = new List<SkinPlayer>();
    Dictionary<int, SkinPlayer> skinDic = new Dictionary<int, SkinPlayer>();
    int currentSkinIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in transform.GetComponentsInChildren<SkinPlayer>())
        {
            skinList.Add(item);
            skinDic.Add(item.skinID, item);
            if(skinList.Count!=1)item.gameObject.SetActive(false);
        }
    }


    public  void NextSkin()
    {
        int nextIndex = currentSkinIndex+1 >= skinList.Count ? 0 : currentSkinIndex+1;
        skinList[currentSkinIndex].HideSkin(() =>
        {
            skinList[nextIndex].DisplaySkin();
        });
        currentSkinIndex = nextIndex;
    }

}
