using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform[] targets = null;
    public float bulletSpeed = 5;
    public int shootCount = 1;
    public float bulletFrequency = 100;
    public float shootFrequency = 100;
    public GameObject bulletPre = null;
    public bool penetrate = false;
    public bool diffraction = false;


    ShootTrajactory shootTrajactory = null;
    DiffractionTrajactory diffractionTrajactory = null;
    private void Start()
    {
        GameObject shootObj = GameObject.Instantiate(bulletPre);
        shootObj.transform.SetParent(transform);
        shootObj.transform.localPosition = Vector3.zero;
        shootObj.transform.localEulerAngles = Vector3.zero;
        shootTrajactory = shootObj.AddComponent<ShootTrajactory>();
        shootTrajactory.bulletProp.BulletSpeed = bulletSpeed;
        shootTrajactory.bulletProp.ShootCount = shootCount;
        shootTrajactory.bulletProp.BulletFrequency = bulletFrequency;
        shootTrajactory.bulletProp.ShootFrequency = shootFrequency;
        shootTrajactory.test = this;

        if (penetrate)
        {
            GameObject penetrateObj = GameObject.Instantiate(bulletPre);
            PenetrateTrajactory pTrajactory = penetrateObj.AddComponent<PenetrateTrajactory>();
            pTrajactory.bulletProp.BulletSpeed = bulletSpeed;
            pTrajactory.bulletProp.ShootCount = 1;
            pTrajactory.bulletProp.BulletFrequency = bulletFrequency;
            pTrajactory.bulletProp.ShootFrequency = shootFrequency;
            shootTrajactory.pTrajatory = pTrajactory;
            pTrajactory.parentSystem = shootTrajactory.system;
        }

        if (diffraction)
        {
            GameObject diffractionObj = GameObject.Instantiate(bulletPre);
            DiffractionTrajactory dTrajactory = diffractionObj.AddComponent<DiffractionTrajactory>();
            dTrajactory.bulletProp.BulletSpeed = bulletSpeed;
            dTrajactory.bulletProp.ShootCount = 1;
            dTrajactory.bulletProp.BulletFrequency = bulletFrequency;
            dTrajactory.bulletProp.ShootFrequency = shootFrequency;
            shootTrajactory.dTrajatory = dTrajactory;
            dTrajactory.parentSystem = shootTrajactory.system;
            dTrajactory.test = this;
        }
    }
     
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootTrajactory.InShoot = true;
            //shootTrajactory.system.trigger.SetCollider(0, null);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            shootTrajactory.InShoot = false;
        }
    }


}
