using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 波次的行为
/// </summary>
public class WaveBehavior
{
    List<WaveBase> waveBaseList = null;
    MonoBehaviour mono = null;

    #region 完成此次波次条件
    float timer = 0f;
    float maxTimer = 0f;
    int nextWaveActiveNum = 0;
    bool overSpwan = false;
    public int aliveEnemyNum = 0;
    #endregion



    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="wb"></param>
    /// <param name="mb"></param>
    public void Init(List<WaveBase> wb, MonoBehaviour mb)
    {
        waveBaseList = wb;
        mono = mb;  
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        waveBaseList.Clear();
        waveBaseList = null;
        mono = null;
        timer = 0f;
    }

    /// <summary>
    /// 激活波次
    /// </summary>
    public void Active()
    {
        for(int i = 0; i < waveBaseList.Count; i++)
        {
            WaveBase wave = waveBaseList[i];
            for(int j = 0; j < wave.SpwanCount; j++)
            {
                //有可能一个点可以生成多个怪物，采用延迟生成
                mono.StartCoroutine(ISpwanEnemy(i,j, wave));
            }
            maxTimer = Mathf.Max(maxTimer, wave.MaxTime);
            nextWaveActiveNum = Mathf.Max(nextWaveActiveNum, wave.NextWaveActiveNum);
        }
    }

    /// <summary>
    /// 更新波次行为
    ///  
    /// </summary>
    public bool Update()
    {
        if (overSpwan)
        {
            timer += Time.deltaTime;
            if ((timer >= maxTimer || aliveEnemyNum <= nextWaveActiveNum))
                return true;
        }
        return false;
    }

    
    IEnumerator ISpwanEnemy(int waveIndex,int spwanIndex, WaveBase wave)
    {
        yield return new WaitForSeconds(wave.SpwanInterval * spwanIndex);
        Transform point = GameManager.Instance.enemyPoints.FindPointByID(wave.PointID);
        GameManager.Instance.enemyManager.Spwan(wave.EnemyID,point);
        aliveEnemyNum++;
        if (waveIndex == waveBaseList.Count - 1 && spwanIndex == wave.SpwanCount - 1)
        {
            Debug.Log("该波次完成了所有的怪物生成");
            overSpwan = true;
        }


    }





}
        