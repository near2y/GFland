using System;
using UnityEngine;


[Serializable]
public class PlayerBehaviour
{
    PlayerTest m_Player;

    bool hadAni = false;
    Animator ani;
    Vector3 lastPosition;
    Quaternion lastRotation = new Quaternion();
    Transform transform;
    Vector3 aniDir = new Vector3();


    int m_VerticalID = Animator.StringToHash("Vertical");
    int m_HorizontalID = Animator.StringToHash("Horizontal");
    int m_RotateID = Animator.StringToHash("Rotate");

    public PlayerBehaviour(PlayerTest p)
    {
        m_Player = p;
        ani = m_Player.GetComponent<Animator>();
        hadAni = ani != null;
        transform = m_Player.transform;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    public void MoveAnimate(float h, float v)
    {
        if (!hadAni) return;
        if (transform.position != lastPosition)
        {
            aniDir.Set(h, 0, v);
            aniDir = transform.InverseTransformDirection(aniDir);
            float tarH = Mathf.Lerp(ani.GetFloat(m_HorizontalID), aniDir.x, 0.75f);
            float tarV = Mathf.Lerp(ani.GetFloat(m_VerticalID), aniDir.z, 0.75f);
            ani.SetFloat(m_HorizontalID, tarH);
            ani.SetFloat(m_VerticalID, tarV);
            lastPosition = transform.position;
        }
        else
        {
            ani.SetFloat(m_VerticalID, 0);
            ani.SetFloat(m_HorizontalID, 0);
        }
        //turn
        float rot = 1;
        if (transform.rotation != lastRotation)
        {
            rot = 0;
            lastRotation = transform.rotation;
        }
        ani.SetFloat(m_RotateID, Mathf.Lerp(ani.GetFloat(m_RotateID), rot, 0.3f));
    }

}
