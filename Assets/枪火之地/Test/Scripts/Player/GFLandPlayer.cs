using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFLandPlayer : MonoBehaviour
{
    public AnimateStateCompiler m_AnimateStateCompiler;
    public PlayerMovement m_Movement = new PlayerMovement();

    private void Update()
    {
        m_Movement.UpdateMovement();
    }

    

}
