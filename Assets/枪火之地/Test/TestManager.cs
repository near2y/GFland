using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public PlayerTest player = null;

    private void Start()
    {
        GameUI gameUI = (GameUI)UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL);
        

        if(player != null)
        {
            player.m_movement.InitJoyStick(gameUI.m_Panel.joystick);
        }
    }

}
