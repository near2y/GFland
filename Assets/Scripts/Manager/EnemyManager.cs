﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager :  MonoBehaviour
{
    /// <summary>
    /// 怪物参数表
    /// </summary>
    EnemyData enemyData;
    /// <summary>
    /// 当前正在场上的敌人集合
    /// </summary>
    public List<EnemyTest> enemyList;

    public float sqrTargetDis;

    void Start()
    {
        enemyData = ConfigerManager.Instance.FindData<EnemyData>(CFG.TABLE_ENEMY);
        enemyList = new List<EnemyTest>();
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void Spwan(int id,Transform fullPoint)
    {
        //生成
        EnemyBase data = enemyData.FindByID(id);
        EnemyTest enemy = ObjectManager.Instance.InstantiateObject(data.PrefabPath).GetComponent<EnemyTest>();
        enemy.transform.SetParent(transform);
        //Enemy enemy = obj.GetComponent<Enemy>();
        //enemy.Init(data);

        ////TODO位置
        //obj.transform.position = fullPoint.position;
        //obj.transform.rotation = fullPoint.rotation;
        enemy.InStage(SceneManager.Instance.player.transform,fullPoint);

        //加到集合中
    }

    /// <summary>
    /// 找到最近的敌人
    /// </summary>
    /// <returns></returns>
    public EnemyTest FindCloseEnemy(float attackDis)
    {
        EnemyTest enemy = null;
        float sqrDis = 0;
        if (enemyList.Count > 0)
        {
            enemy = enemyList[0];
            sqrDis = enemy.targetSqrDis;
            for(int i = 1; i < enemyList.Count; i++)
            {
                if(sqrDis > enemyList[i].targetSqrDis)
                {
                    sqrDis = enemyList[i].targetSqrDis;
                    enemy = enemyList[i];
                }
            }
        }
        sqrTargetDis = sqrDis;
        if (sqrDis > attackDis*attackDis) enemy = null; 
        return enemy;
    }

    public void ClearEnemy(EnemyTest enemy)
    {
        if (enemy != null)
        {
            enemyList.Remove(enemy);
            SceneManager.Instance.waveManager.CurrentWave.aliveEnemyNum--;
        }
    }

    public void AddEnemy(EnemyTest enemy)
    {
        enemyList.Add(enemy);
    }
}
