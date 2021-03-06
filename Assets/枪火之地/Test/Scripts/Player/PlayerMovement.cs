using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerMovement 
{
    public float m_MoveSpeed;
    [MixEnum]
    public MovementLookType m_LookType;

    Transform m_Transform;
    Joystick m_Joystick;
    bool m_Inited = false;
    Vector3 movement;
    CharacterController m_Controller;


    public void InitMovement(Transform trans,Joystick joystick)
    {
        m_Inited = true;
        m_Transform = trans;
        m_Joystick = joystick;
        movement = new Vector3();
        m_Controller = m_Transform.GetComponent<CharacterController>();
        if(m_Controller == null)
        {
            Debug.LogError("当前玩家对象身上没有角色控制器，检查！");
        }


    }


    public void UpdateMovement()
    {
        if (!m_Inited) return;
        movement.x = m_Joystick.movement.x;
        movement.z = m_Joystick.movement.z;
        if(movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }
        Debug.Log("near2y:" + m_MoveSpeed);
        movement = movement * m_MoveSpeed * Time.deltaTime;
        m_Controller.SimpleMove(movement);
    }



}
