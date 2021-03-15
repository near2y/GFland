using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public EquipmentGun equipmentGun;
    public Transform monsterGather;

    List<Monster> monsters;

    int showMonsterIndex = 0;
    int outMonsterIndex = 0;
    GFLandPlayer player;
    private void Start()
    {
        GameUI gameUI = (GameUI)UIManager.Instance.PopUpWindow(ConstString.GAMEPANEL);
        player = GameManager.Instance.gameSceneMgr.m_Player;
        player.m_Movement.InitMovement(player.transform, gameUI.m_Panel.joystick);
        monsters = new List<Monster>();
        foreach (var monster in monsterGather.GetComponentsInChildren<Monster>())
        {
            monsters.Add(monster);
        }
        showMonsterIndex = 0;
        equipmentGun.test = player.gameObject;

    }

    List<CombatAbility> m_campAbilityList = new List<CombatAbility>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (showMonsterIndex < monsters.Count)
            {
                monsters[showMonsterIndex].ToStage(player.transform);
                monsters[showMonsterIndex].CompleteSpawn();
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            CampManager.Instance.FindCampByType(CampType.Opposite,player.m_CombatAbility, ref m_campAbilityList);
            Debug.Log("当前玩家敌对阵营的数量：" + m_campAbilityList.Count);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            CampManager.Instance.FindCampByType(CampType.Opposite, ConstString.MonsterCampName, ref m_campAbilityList);
            Debug.Log("当前怪物敌对阵营的数量：" + m_campAbilityList.Count);
        }
    }

}
