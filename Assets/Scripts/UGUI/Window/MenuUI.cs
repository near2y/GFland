using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : Window
{
    private MenuPanel m_Panel;


    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<MenuPanel>();
        AddBtnClickListener(m_Panel.m_SoundBtn, ClickSoundBtn);
    }
    

    private void ClickSoundBtn()
    {
        Debug.Log("点击了声音按钮");
    }
}
