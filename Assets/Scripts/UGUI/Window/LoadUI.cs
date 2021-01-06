using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : Window
{
    private LoadPanel m_Panel;

    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<LoadPanel>();
    }

    public override void OnUpdate()
    {
        m_Panel.m_ProcessTxt.text = GameMapManager.LoadingProgress.ToString() + "%";
        m_Panel.m_ProcessBar.value = GameMapManager.LoadingProgress / 100.0f;
        if (GameMapManager.LoadingProgress >= 100)
            OnSceneLoad();
    }

    private void OnSceneLoad()
    {
        UIManager.Instance.CloseWindow(Name);
        //打开首页
        UIManager.Instance.PopUpWindow(ConStr.MENUPANEL);

    }
}
