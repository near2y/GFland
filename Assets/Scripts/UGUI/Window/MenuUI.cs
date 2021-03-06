using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class MenuUI : Window
{
    private MenuPanel m_Panel;

    public override void OnAwake(params object[] paraList)
    {
        m_Panel = GameObject.GetComponent<MenuPanel>();
        AddBtnClickListener(m_Panel.m_StartBtn, ClickStartBtn);
        AddBtnClickListener(m_Panel.m_NormalBtn, ClickNormalBtn);
        AddBtnClickListener(m_Panel.m_SkinBtn, ClickSkinBtn);
        AddBtnClickListener(m_Panel.m_SkillBtn, ClickSkillBtn);
        AddBtnClickListener(m_Panel.m_EquipBtn, ClickEquipBtn);
        AddBtnClickListener(m_Panel.m_LeftSkinBtn, ClickLeftSkinBtn);
        AddBtnClickListener(m_Panel.m_RightSkinBtn, ClickRightSkinBtn);
        AddBtnClickListener(m_Panel.m_ChooseSkinBtn, ClickChooseSkinBtn);

        m_Panel.m_SkinPop.gameObject.SetActive(false);

        string jsonPath = "Assets/RealFram/Data/Json/EquipmentJson.json";

        var js = JSON.Parse(ResourceManager.Instance.LoadResource<Object>(jsonPath).ToString());
        m_Panel.m_Test.text = js[0]["装备名"];
    }



    private void ClickStartBtn()
    {
        UIManager.Instance.CloseWindow(Name);
        //GameManager.Instance.GameStart();
        MenuSceneManager.Instance.OutMenu();
    }

    private void ClickNormalBtn()
    {
        MenuSceneManager.Instance.ToState(MenuState.Normal);
        m_Panel.m_StartBtn.gameObject.SetActive(true);

    }
    private void ClickSkillBtn()
    {
        MenuSceneManager.Instance.ToState(MenuState.Skill);
        m_Panel.m_StartBtn.gameObject.SetActive(false);

    }
    private void ClickSkinBtn()
    {
        MenuSceneManager.Instance.ToSkin();
        m_Panel.m_StartBtn.gameObject.SetActive(false);
        m_Panel.m_SkinPop.gameObject.SetActive(true);

    }
    private void ClickEquipBtn()
    {
        MenuSceneManager.Instance.ToState(MenuState.Equip);
        m_Panel.m_StartBtn.gameObject.SetActive(false);
    }

    private void ClickLeftSkinBtn()
    {
        MenuSceneManager.Instance.skinManager.NextSkin();
    }

    private void ClickRightSkinBtn()
    {
        MenuSceneManager.Instance.skinManager.NextSkin();
    }

    private void ClickChooseSkinBtn()
    {
        m_Panel.m_SkinPop.gameObject.SetActive(false);
        ClickNormalBtn();
    }


}
