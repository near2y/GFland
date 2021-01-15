using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    bool inited = false;
    WaveData waveData = null;
    Dictionary<int, List<WaveBase>> waveBaseListDic = new Dictionary<int, List<WaveBase>>();
    Dictionary<int, WaveBehavior> waveBehaviorDic = new Dictionary<int, WaveBehavior>();
    int waveIndex = 0;

    public WaveBehavior CurrentWave { get { return waveBehaviorDic[waveIndex]; } }


    public void Init(WaveData data)
    {
        waveData = data;
        foreach (WaveBase waveBase in data.AllWaveList)
        {
            List<WaveBase> list = null;
            if(!waveBaseListDic.TryGetValue(waveBase.WaveID,out list) || list == null)
            {
                list = new List<WaveBase>();    
                waveBaseListDic[waveBase.WaveID] = list;
            }
            list.Add(waveBase);
        }
        Debug.Log(waveBaseListDic);
        foreach (int waveID in waveBaseListDic.Keys)
        {
            WaveBehavior wb = new WaveBehavior();
            waveBehaviorDic[waveID] = wb;
            wb.Init(waveBaseListDic[waveID], this);
        }
        inited = true;

        waveIndex = 0;
        waveBehaviorDic[waveIndex].Active();
    }


    private void Update()
    {
        if (!inited) return;

        //判断下个波次是否达到激活条件
        if(waveIndex < waveBehaviorDic.Count - 1)
        {
            bool over = waveBehaviorDic[waveIndex].Update();
            if (over && waveIndex < waveBehaviorDic.Count)
            {
                waveIndex++;
                if (waveBehaviorDic.ContainsKey(waveIndex))
                {
                    waveBehaviorDic[waveIndex].Active();
                }
                //TODO最后一波的问题

            }
        }

    }



}