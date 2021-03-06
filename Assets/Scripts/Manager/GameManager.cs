using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("测试使用")]
    public string[] waveID = { "0101" };
    int gameIndex = 0;

    [HideInInspector]
    public WaveJson waveJson;

    [HideInInspector]
    public EffectData effectData;

    [HideInInspector]
    public MonoBehaviour mono;

    public int playerTrajactoryCount = 1;
    public GameSceneMgr gameSceneMgr;


    private void Start()
    {
        mono = this;
        effectData = ConfigerManager.Instance.FindData<EffectData>(CFG.TABLE_EFFECT);
    }


    public void GameStart()
    {
        //TODO
        if (gameIndex >= waveID.Length) gameIndex = 0;
        //===========================
        string wavePath = "Assets/RealFram/Data/Json/WaveJson/WaveData_" + waveID[gameIndex] + ".json";
        waveJson = new WaveJson(wavePath);
        //=============================
        GameMapManager.Instance.LoadScene(waveJson.AllWaveList[0].SceneName,CompleteLoadScene, 2);
    }

    void CompleteLoadScene()
    {
        mono.StartCoroutine(SetScene());
    }

    IEnumerator SetScene()
    {
        yield return new WaitForFixedUpdate();
        var scene = SceneManager.GetActiveScene();
        var objs = scene.GetRootGameObjects();
        gameSceneMgr = null;
        for(int i=  0; i < objs.Length; i++)
        {
            gameSceneMgr = objs[i].GetComponent<GameSceneMgr>();
            if (gameSceneMgr != null) break;
        }
    }

    public void GameOver()
    {
        gameIndex++;

    }
}

