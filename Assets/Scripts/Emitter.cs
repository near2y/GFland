using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{

    [Header("< 弹道相关参数 >")]
    public float bulletSpeed = 5;
    public int shootCount = 1;
    public float bulletFrequency = 100;
    public float shootFrequency = 100;
    public int penetrateCount = 0;
    public int diffractionCount = 0;

    [Header(" <暂存> ")]
    public GameObject bulletPre = null;
    public Transform[] targets = null;



    Trajactory shootTrajactory = null;

    private void Start()
    {
        GameObject shootObj = GameObject.Instantiate(bulletPre);
        shootObj.transform.SetParent(transform);
        shootObj.transform.localPosition = Vector3.zero;
        shootObj.transform.localEulerAngles = Vector3.zero;
        shootTrajactory = shootObj.AddComponent<Trajactory>();
        shootTrajactory.Init(this, bulletSpeed, shootCount, bulletFrequency, shootFrequency);

        for(int i = 0; i < penetrateCount; i++)
        {
            AddPenetrateAbility();
        }

        for(int i = 0; i < diffractionCount; i++)
        {
            AddDiffractionAbility();
        }
    }
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootTrajactory.InShoot = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            shootTrajactory.InShoot = false;
        }
    }

    void AddDiffractionAbility()
    {
        Trajactory t = shootTrajactory;
        while (t.dTrajactory !=null)
        {
            t = t.dTrajactory;
        }
        GameObject shootObj = GameObject.Instantiate(bulletPre);
        shootObj.transform.localPosition = Vector3.zero;
        shootObj.transform.localEulerAngles = Vector3.zero;
        t.dTrajactory = shootObj.AddComponent<Trajactory>();
        t.dTrajactory.Init(this, bulletSpeed, shootCount, bulletFrequency, shootFrequency);
    }

    void AddPenetrateAbility()
    {
        Trajactory t = shootTrajactory;
        while (t.pTrajactory != null)
        {
            t = t.pTrajactory;
        }
        GameObject shootObj = GameObject.Instantiate(bulletPre);
        shootObj.transform.localPosition = Vector3.zero;
        shootObj.transform.localEulerAngles = Vector3.zero;
        t.pTrajactory = shootObj.AddComponent<Trajactory>();
        t.pTrajactory.Init(this, bulletSpeed, shootCount, bulletFrequency, shootFrequency);
    }
}
