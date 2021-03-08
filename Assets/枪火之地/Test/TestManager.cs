using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject spiderPre;



    private void Start()
    {
        GameUI gameUI = (GameUI)UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL);
        GFLandPlayer player = GameManager.Instance.gameSceneMgr.m_Player;
        player.m_Movement.InitMovement(player.transform, gameUI.m_Panel.joystick);
    }

}
