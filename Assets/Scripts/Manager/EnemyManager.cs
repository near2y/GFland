using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager 
{
    /// <summary>
    /// 怪物参数表
    /// </summary>
    EnemyData m_EnemyData;
    /// <summary>
    /// 当前正在场上的敌人集合
    /// </summary>
    public List<Enemy> m_EnemyList;

    public int enemyAliveCount = 0;

    Transform m_Parent;

    public EnemyManager(Transform parent)
    {
        m_Parent = parent;
        m_EnemyList = new List<Enemy>();
        m_EnemyData = ConfigerManager.Instance.FindData<EnemyData>(CFG.TABLE_ENEMY);
    }


    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Enemy Spwan(int id,Transform fullPoint,bool extra =false)
    {
        //生成
        EnemyBase data = m_EnemyData.FindByID(id);
        Enemy enemy = ObjectManager.Instance.InstantiateObject(data.PrefabPath).GetComponent<Enemy>();
        enemy.transform.SetParent(m_Parent);
        //Enemy enemy = obj.GetComponent<Enemy>();
        //enemy.Init(data);

        ////TODO位置
        //obj.transform.position = fullPoint.position;
        //obj.transform.rotation = fullPoint.rotation;
        enemy.InStage(GameManager.Instance.gameSceneMgr.player.transform,fullPoint);
        enemy.hp = data.Hp;
        //计数
        if(!extra)enemyAliveCount++;
        return enemy;
    }

    /// <summary>
    /// 找到最近的敌人
    /// </summary>
    /// <returns></returns>
    public Enemy FindCloseEnemy(float attackDis)
    {
        Enemy enemy = null;
        float standard = attackDis * attackDis;
        for (int i = 0; i < m_EnemyList.Count; i++)
        {
            if (m_EnemyList[i].died || m_EnemyList[i].targetSqrDis>standard || m_EnemyList[i].invalid) continue;
            if(enemy == null)
            {
                enemy = m_EnemyList[i];
            }
            if (enemy.targetSqrDis > m_EnemyList[i].targetSqrDis)
            {
                enemy = m_EnemyList[i];
            }
        }
        return enemy;
    }

    public void ClearEnemy(Enemy enemy,bool extra = false)
    {
        if (enemy != null)
        {
            enemy.Release();
            m_EnemyList.Remove(enemy);
            if(!extra)GameManager.Instance.gameSceneMgr.waveManager.CurrentWave.aliveEnemyNum--;
            if(!extra)enemyAliveCount--;
            GameManager.Instance.gameSceneMgr.player.playerSkillBar.SkillProgress+=5;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        if (enemy.addedInGame) return;
        m_EnemyList.Add(enemy);
        enemy.addedInGame = true;
    }

}
