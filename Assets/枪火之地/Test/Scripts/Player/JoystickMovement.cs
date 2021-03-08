using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JoystickMovement 
{
    public float m_MoveSpeed;
    public Action<float,float> m_CompleteUpdate;


    Transform m_Transform;
    Joystick m_Joystick;
    Vector3 m_MoveValue;
    bool m_Inited = false;
    CharacterController m_Controller;


    public void InitMovement(Transform trans,Joystick joystick)
    {
        m_Inited = true;
        m_Transform = trans;
        m_Joystick = joystick;
        m_MoveValue = new Vector3();
        m_Controller = m_Transform.GetComponent<CharacterController>();
        Check();
    }


    void Check()
    {
#if UNITY_EDITOR
        if (m_Controller == null)
        {
            Debug.LogError("当前玩家对象身上没有角色控制器，检查！");
            return; 
        }

        if (!m_Inited)
        {
            Debug.LogError("当前对象的joystick没有初始化，检查！");
            return;
        }
#endif
    }


    public void UpdateMovement()
    {
        Check();
        m_MoveValue.x = m_Joystick.movement.x;
        m_MoveValue.z = m_Joystick.movement.z;
        if(m_MoveValue.sqrMagnitude > 1)
        {
            m_MoveValue = m_MoveValue.normalized;
        }
        m_MoveValue = m_MoveValue * m_MoveSpeed * Time.deltaTime;
        m_Controller.SimpleMove(m_MoveValue);
        m_CompleteUpdate?.Invoke(m_MoveValue.x,m_MoveValue.z);
    }


    #region Look At
    //[MixEnum]
    //public MovementLookType m_LookType;
    Vector3 lookPoint;
    [Header("Look At Set")]
    public LayerMask m_Layer;
    public float m_LookLerp;
    public void UpdateLookAt(Transform target)
    {
        Check();
        if (target == null)
        {
            //raycast
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, 100, m_Layer))
            {
                lookPoint = floorHit.point;
            }
            else
            {
                return;
            }
        }
        else
        {
            //target
            lookPoint = target.position;
        }
        Vector3 playerToMouse = lookPoint - m_Transform.position;
        playerToMouse.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(playerToMouse);
        if (Mathf.Abs(m_Transform.rotation.eulerAngles.y - lookRot.eulerAngles.y) > 10)
        {
            m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, lookRot, 5 * Time.deltaTime);
        }
        else
        {
            m_Transform.rotation = lookRot;
        }
    }
    #endregion
}
