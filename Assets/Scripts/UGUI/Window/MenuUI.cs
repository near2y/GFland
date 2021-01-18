using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : Window
{
    private MenuPanel m_Panel;

    //需要打开的场景
    string openSceneName = null;

    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<MenuPanel>();
        AddBtnClickListener(m_Panel.m_SoundBtn, ClickSoundBtn);
        AddBtnClickListener(m_Panel.m_StartBtn, ClickStartBtn);
    }


    private void ClickStartBtn()
    {
        UIManager.Instance.CloseWindow(Name);
        if (TestData.Instance.TestConfig.inTest)
        {
            openSceneName = TestData.Instance.TestConfig.testSceneName.ToString();
        }
        GameMapManager.Instance.LoadScene(openSceneName,2);
    }

    private void ClickSoundBtn()
    {
        Debug.Log("点击了声音按钮");
    }
}
