using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement :MonoBehaviour
{
    public float moveSpeed = 5;
    public float angelSpeed = 120;

    public MovementMoveType m_type = MovementMoveType.CharactorCtrol;

    //===========   重力 =================
    public Rigidbody m_rigid = null;

    //==========   角色控制器 =================
    public bool m_AddCharatorControl = false;
    public CharacterController m_ccr = null;
    Vector3 m_SimpleMoveInput = new Vector3();
    public MovementCtrlType m_ctrlType = MovementCtrlType.Joystick;
    public MovementLookType m_lookType = MovementLookType.Input;
    public LayerMask m_inputLayer;
    public MovementLookAxisType m_lookAxisType = MovementLookAxisType.All;
    public Joystick m_Joystick = null;
    public bool hadJoystick = false;
    public float m_inputLength = 100;
    Vector3 m_inputPoint;
    public Action TurningAction = null;
    public Transform m_LookTarget = null;
    public float m_LookLerp = 1;
    Vector3 m_LastRotationEuler = new Vector3();

    //===========    寻路 ===================
    public bool m_AddNavmeshAgent = false;
    public NavMeshAgent m_agent = null;

    private void Start()
    {
        m_SimpleMoveInput = Vector3.zero;
    }

    public void InitJoyStick(Joystick joy)
    {
        if (joy != null)
        {
            m_Joystick = joy;
            hadJoystick = true;
        }
        else
        {
            Debug.LogError("初始化摇杆出错！请检查代码！");
        }

    }

    public void FixedUpdate()
    {
        switch (m_type)
        {
            case MovementMoveType.CharactorCtrol:
                CharactorUpdate();
                break;
            case MovementMoveType.Navmesh:
                NavmeshUpdate();
                break;
            case MovementMoveType.Rigidbody:
                RigidUpdate();
                break;
        }
    }


    void NavmeshUpdate()
    {

    }

    void CharactorUpdate()
    {
        if (hadJoystick)
        {
            float h = 0, v = 0;
            h = m_Joystick.movement.x;
            v = m_Joystick.movement.z;
            if(h*h + v * v > 1)
            {
                m_SimpleMoveInput = m_Joystick.movement.normalized;
            }
            else
            {
                m_SimpleMoveInput.x = m_Joystick.movement.x;
                m_SimpleMoveInput.z = m_Joystick.movement.z;
            }
            m_ccr.SimpleMove(m_SimpleMoveInput* moveSpeed * Time.deltaTime);
            LookTurning();
        }
    }

    void LookTurning()
    {
        if (m_lookType.Equals(MovementLookType.None)) return;
        m_inputPoint = transform.position;
        if (m_lookType.HasFlag(MovementLookType.Input))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, m_inputLength, m_inputLayer))
            {
                m_inputPoint = floorHit.point;
            }
        }
        if (m_lookType.HasFlag(MovementLookType.Target))
        {
            if (m_LookTarget != null) m_inputPoint = m_LookTarget.transform.position;
        }
        m_LastRotationEuler = transform.rotation.eulerAngles;
        transform.LookAt(m_inputPoint);
        var currentEuler = transform.rotation.eulerAngles;
        if (!m_lookAxisType.HasFlag(MovementLookAxisType.X))
        {
            currentEuler.x = m_LastRotationEuler.x;
        }
        if (!m_lookAxisType.HasFlag(MovementLookAxisType.Y))
        {
            currentEuler.y = m_LastRotationEuler.y;
        }
        if (!m_lookAxisType.HasFlag(MovementLookAxisType.Z))
        {
            currentEuler.z = m_LastRotationEuler.z;
        }
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(m_LastRotationEuler),Quaternion.Euler(currentEuler),m_LookLerp);
        TurningAction?.Invoke();
    }

    void RigidUpdate()
    {

    }
}



public enum MovementMoveType 
{
    CharactorCtrol,
    Navmesh,
    Rigidbody
}

public enum MovementCtrlType 
{
    Joystick,
    Keyboard
}

public enum MovementLookType
{
    None = 0, // Custom name for "Nothing" option
    Input = 1 << 0,
    Target = 1 << 1,
    All = ~0, // Custom name for "Everything" option
}

public enum MovementLookAxisType
{
    None = 0,
    X = 1<<0,
    Y = 1<<1,
    Z = 2<<1,
    All = ~0,
}