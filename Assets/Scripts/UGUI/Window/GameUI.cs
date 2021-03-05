using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : Window
{
    public GamePanel m_Panel;


    private List<GameObject> balls = new List<GameObject>();



    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<GamePanel>();
        AddBtnClickListener(m_Panel.homeBtn, ClickHomeBtn);
        AddBtnClickListener(m_Panel.nextGameBtn, ClickNextBtn);
        AddBtnClickListener(m_Panel.skillBtn, ClickSkillBtn);

        m_Panel.nextGameBtn.gameObject.SetActive(false);
        //m_Panel.skillBtn.interactable = false;
        m_Panel.skillBtn.gameObject.SetActive(false);
        //SceneManager.Instance.gameUI.m_Panel.skillBtn.gameObject.SetActive(false);


    }



    public override void OnClose()
    {
        base.OnClose();
        m_Panel.joystick.ShowHide(false);
    }

    void ClickHomeBtn()
    {
        UIManager.Instance.CloseWindow(Name);
        GameMapManager.Instance.LoadScene(ConStr.MENUSCENE, 1);
    }

    //点击下一关
    void ClickNextBtn()
    {
        GameManager.Instance.GameOver();
        UIManager.Instance.CloseWindow(Name);
        GameManager.Instance.GameStart();
    }

    void ClickSkillBtn()
    {
        GameManager.Instance.gameSceneMgr.player.skill.ReleaseSkill();
        //m_Panel.skillBtn.interactable = false;
        m_Panel.skillBtn.gameObject.SetActive(false);
        ////TODO
        ////伪等待时间
        //GameManager.Instance.mono.StartCoroutine(WaitSkillCool());
    }


    IEnumerator WaitSkillCool()
    {
        m_Panel.skillBtn.interactable = false;
        yield return new WaitForSeconds(10);
        m_Panel.skillBtn.interactable = true;
        
    }


    public void ShowClear()
    {
        m_Panel.nextGameBtn.gameObject.SetActive(true);
        m_Panel.joystick.gameObject.SetActive(false);
        
    }


}


