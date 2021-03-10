using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject spiderPre;
    public Transform monsterGather;

    List<Monster> monsters;

    int showMonsterIndex = 0;
    int outMonsterIndex = 0;
    GFLandPlayer player;
    private void Start()
    {
        GameUI gameUI = (GameUI)UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL);
        player = GameManager.Instance.gameSceneMgr.m_Player;
        player.m_Movement.InitMovement(player.transform, gameUI.m_Panel.joystick);
        monsters = new List<Monster>();
        foreach (var monster in monsterGather.GetComponentsInChildren<Monster>())
        {
            monsters.Add(monster);
        }
        showMonsterIndex = 0;

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (showMonsterIndex < monsters.Count)
            {
                monsters[showMonsterIndex].ToStage(player.transform);
                showMonsterIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (outMonsterIndex < showMonsterIndex)
            {
                monsters[outMonsterIndex].OutStage();
                outMonsterIndex++;
                
            }
        }
    }

}
