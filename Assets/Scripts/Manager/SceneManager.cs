using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    
    public EnemyManager enemyManager;
    public Player player;
    public WaveManager waveManager;
    public EffectManager effectManager;
    public Transform pointsTrans;
    public GameCamera gameCamera;
    public EnemyPoints enemyPoints;

    string playerPrePath = "Assets/枪火之地/Prefabs/player.prefab";


    private void Start()
    {
        Init();

        string wavePath = "Assets/RealFram/Data/Binary/WaveData_0701.bytes";
#if UNITY_EDITOR
        //if (!System.IO.File.Exists(wavePath))
        //{
        //    Debug.LogError(TestData.Instance.TestConfig.smallLevelID + "配置表文件不存在！请查看！");
        //    UnityEditor.EditorApplication.isPaused = true;
        //}
#endif
        WaveData waveData = ConfigerManager.Instance.FindData<WaveData>(wavePath);
        waveManager.Init(waveData);
    }

    //初始化
    void Init()
    {
        //points
        enemyPoints = new EnemyPoints(pointsTrans);
        player = ObjectManager.Instance.InstantiateObject(playerPrePath).GetComponent<Player>();
        player.transform.SetParent(transform);
        gameCamera.SetTarget(player.transform);

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
