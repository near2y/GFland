using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager :  MonoSingleton<EnemyManager>
{
    /// <summary>
    /// 怪物参数表
    /// </summary>
    EnemyData enemyData;
    /// <summary>
    /// 当前正在场上的敌人集合
    /// </summary>
    List<Enemy> enemyList;

    public float sqrTargetDis;

    void Start()
    {
        enemyData = ConfigerManager.Instance.FindData<EnemyData>(CFG.TABLE_ENEMY);
        enemyList = new List<Enemy>();

        string wavePath = "Assets/RealFram/Data/Binary/WaveData_0701.bytes";
        WaveData waveData = ConfigerManager.Instance.FindData<WaveData>(wavePath);
        WaveBase waveBase = waveData.FindByID(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Spwan(2001);
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            Spwan(2004);
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            Spwan(2005);
        }
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void Spwan(int id)
    {
        //生成
        EnemyBase data = enemyData.FindByID(id);
        GameObject obj = ObjectManager.Instance.InstantiateObject(data.PrefabPath, true);
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Init(data);

        //TODO位置
        Vector3 pos = new Vector3(-4.58f, 0, 0);
        obj.transform.position = pos;

        //加到集合中
        enemyList.Add(enemy);
    }

    /// <summary>
    /// 找到最近的敌人
    /// </summary>
    /// <returns></returns>
    public Enemy FindCloseEnemy(float attackDis)
    {
        Enemy enemy = null;
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

    public void ClearEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            ObjectManager.Instance.ReleaseObject(enemy.gameObject);
            enemyList.Remove(enemy);
        }
    }
}
