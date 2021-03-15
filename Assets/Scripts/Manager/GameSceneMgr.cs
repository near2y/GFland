using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMgr : MonoBehaviour
{
    [Header("< 设置 >")]
    [HideInInspector]
    public Player player;
    public GameObject m_PlayerPre;
    public Transform playerLookTarget;
    [HideInInspector]
    public GFLandPlayer m_Player;
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
        GameManager.Instance.gameSceneMgr = this;
        //init camp
        InitCamp();
        //monsterPoints
        var enemyPointObj = GameObject.Find("MonsterPoint");
        if (enemyPointObj != null)enemyPoints = new EnemyPoints(enemyPointObj.transform);
        //player
        if (m_Player != null) 
        { 
            m_Player = GameObject.Instantiate(m_PlayerPre).GetComponent<GFLandPlayer>();
            m_Player.m_LookTarget = playerLookTarget;
            CampManager.Instance.AddToCamp(ConstString.PlayerCampName, m_Player.m_CombatAbility);
        }
        //if (player == null) player = GameObject.Find("player").GetComponent<Player>();
        //TODO
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
        waveManager = new WaveManager(GameManager.Instance.waveJson, this);
        startedWave = true;
    }

    private void InitCamp()
    {
        CampManager.Instance.Reset();
        CampManager.Instance.RegisterCamp("Player");
        CampManager.Instance.RegisterCamp("Monster");
    }

    private void Update()
    {
        if (startedWave)
        {
            waveManager.Update();
        }
    }
    
}
