using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFLandPlayer : MonoBehaviour
{
    public AnimateStateCompiler m_AnimateStateCompiler;
    public JoystickMovement m_Movement = new JoystickMovement();
    [HideInInspector]
    public Transform m_LookTarget = null;
    public PlayerAttackSystem m_playerAttackSystem;


    private void Start()
    {
        m_Movement.m_CompleteUpdate += MoveCallBack;
        m_AnimateStateCompiler.Init(typeof(GFLandPlayer));
        ToState(PlayerState.ToStage);

        m_playerAttackSystem.Init(this);

    }

    private void OnDestroy()
    {
        m_playerAttackSystem.OnDestroy();
    }

    public void ToState(int aniID)
    {
        m_AnimateStateCompiler.m_Animator.SetTrigger(aniID);
    }

    public void InGameMove()
    {
        m_Movement.UpdateMovement();
        m_Movement.UpdateLookAt(m_LookTarget);

        m_playerAttackSystem.OnMoveUpdate();
    }

    #region Move Connect Animator
    int m_VerticalID = Animator.StringToHash("Vertical");
    int m_HorizontalID = Animator.StringToHash("Horizontal");
    int m_RotateID = Animator.StringToHash("Rotate");
    Vector3 lastPosition = new Vector3();
    Quaternion lastRotation = new Quaternion();
    Vector3 aniDir = new Vector3();
    private void MoveCallBack(float h,float v)
    {
        if (transform.position != lastPosition)
        {
            aniDir.Set(h, 0, v);
            aniDir = transform.InverseTransformDirection(aniDir);
            float tarH = Mathf.Lerp(m_AnimateStateCompiler.m_Animator.GetFloat(m_HorizontalID), aniDir.x, 0.75f);
            float tarV = Mathf.Lerp(m_AnimateStateCompiler.m_Animator.GetFloat(m_VerticalID), aniDir.z, 0.75f);
            m_AnimateStateCompiler.m_Animator.SetFloat(m_HorizontalID, tarH);
            m_AnimateStateCompiler.m_Animator.SetFloat(m_VerticalID, tarV);
            lastPosition = transform.position;
        }
        else
        {
            m_AnimateStateCompiler.m_Animator.SetFloat(m_VerticalID, Mathf.Lerp(m_AnimateStateCompiler.m_Animator.GetFloat(m_VerticalID),0,0.25f));
            m_AnimateStateCompiler.m_Animator.SetFloat(m_HorizontalID, Mathf.Lerp(m_AnimateStateCompiler.m_Animator.GetFloat(m_HorizontalID), 0, 0.25f));
        }
        //turn
        float rot = 1;
        if (transform.rotation != lastRotation)
        {
            rot = 0;
            lastRotation = transform.rotation;
        }
        m_AnimateStateCompiler.m_Animator.SetFloat(m_RotateID, Mathf.Lerp(m_AnimateStateCompiler.m_Animator.GetFloat(m_RotateID), rot, 0.3f));
    }
    #endregion






}

public class PlayerState
{
    public static int ToStage = Animator.StringToHash("ToStage");
    public static int ToRelive = Animator.StringToHash("ToRelive");
    public static int ToDying = Animator.StringToHash("ToDying");
    public static int ToWin = Animator.StringToHash("ToWin");
}
