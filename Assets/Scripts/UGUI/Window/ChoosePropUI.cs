using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePropUI : Window
{
    public ChoosePropPanel m_Panel;

    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<ChoosePropPanel>();

        AddBtnClickListener(m_Panel.btnProp1, ClickPropBox);
        AddBtnClickListener(m_Panel.btnProp2, ClickCloseBtn);
        AddBtnClickListener(m_Panel.btnProp3, ClickCloseBtn);
        AddBtnClickListener(m_Panel.btnChange, ClickCloseBtn);
    }


    void ClickPropBox()
    {
        UIManager.Instance.CloseWindow(Name);
        GameManager.Instance.playerTrajactoryCount++;
        GameManager.Instance.gameSceneMgr.player.OverChooseProp();
    }

    void ClickCloseBtn()
    {
        UIManager.Instance.CloseWindow(Name);
        GameManager.Instance.gameSceneMgr.player.OverChooseProp();
    }
}
