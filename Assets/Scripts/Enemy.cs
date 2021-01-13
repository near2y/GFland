using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    public Transform matchTarget;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        anim.SetIKPosition(AvatarIKGoal.RightHand, matchTarget.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, matchTarget.rotation);
    }
}
