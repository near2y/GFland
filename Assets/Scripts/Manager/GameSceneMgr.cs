using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMgr : MonoBehaviour
{
    [Header("< 设置 >")]

    public Player player;
    public EnemyManager enemyManager;
    public WaveManager waveManager = null;
    public EffectManager effectManager = null;
    [HideInInspector]
    public GameCamera gameCamera;
    public EnemyPoints enemyPoints;
    public GameUI gameUI = null;
    public bool bossGame = false;
    



    private void Start()
    {
        Init();
    }

    //初始化
    void Init()
    {

        //monsterPoints
        enemyPoints = new EnemyPoints(GameObject.Find("MonsterPoint").transform);
        //player
        //player = ObjectManager.Instance.InstantiateObject(playerPrePath).GetComponent<Player>();
        //player.transform.SetParent(transform);
        //camera
        gameCamera = GameObject.Find("Main Camera").GetComponent<GameCamera>();
        //enemyManager
        enemyManager = new EnemyManager(transform);
        //effectManager
        effectManager = new EffectManager(GameManager.Instance.effectData, transform);
        
    }

    //游戏胜利
    public void OnWin()
    {
        Debug.Log("游戏胜利了");
        player.OnWin();
    }

    //开始波次
    bool startedWave = false;
    public void StartWave()
    {
        //waveManager
        waveManager = new WaveManager(GameManager.Instance.waveData, this);
        startedWave = true;

        //TODO
        ////?
        //RFramework.instance.m_UIRoot.gameObject.SetActive(false);
        //RFramework.instance.m_UIRoot.gameObject.SetActive(true);
        //gameUI = UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL, true) as GameUI;
    }



    private void Update()
    {
        if (startedWave)
        {
            waveManager.Update();
        }
    }


    

    
}
