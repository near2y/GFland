using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public EnemyManager enemyManager;
    public Player player;
    public WaveManager waveManager;
    public EnemyPoints enemyPoints;
    public EffectManager effectManager;


    private void Start()
    {
        string wavePath = "Assets/RealFram/Data/Binary/WaveData_0701.bytes";
        WaveData waveData = ConfigerManager.Instance.FindData<WaveData>(wavePath);
        waveManager.Init(waveData);
    }


    public void GameStart()
    {
        //TODO
        //玩家登场表现
        //开始按照波次出怪
    }

    public void GamePause()
    {
        //TODO
        //游戏暂停
    }

    public void GameResume()
    {
        //TODO
        //游戏回复
    }

    public void GameOver()
    {
        //TODO
        //游戏结束
    }

    

    
}
