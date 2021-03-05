using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Movement test = null;
    public GameObject spiderPre;



    private void Start()
    {
        GameUI gameUI = (GameUI)UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL);

        //var obj = Instantiate(spiderPre);

        if (test != null)
        {
            test.InitJoyStick(gameUI.m_Panel.joystick);
        }
    }

}
