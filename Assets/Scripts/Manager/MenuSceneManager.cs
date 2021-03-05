using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuSceneManager : MonoSingleton<MenuSceneManager>
{

    public Camera mainCamera = null;
    public CinemachineStateDrivenCamera cameraDriven = null;
    public Animator playerAnimator = null;
    public float playerEmissionLightRange = 0.34f;
    public SkinManager skinManager = null;

    Animator anim;


    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void ToSkin()
    {
        ToState(MenuState.Skin);
        StartCoroutine(DelayPlay(cameraDriven.m_DefaultBlend.BlendTime, playerAnimator, "show"));
    }

    IEnumerator DelayPlay(float time,Animator anim, string name)
    {
        yield return new WaitForSeconds(time);
        anim.Play(name);
    }

    public void ToState(string state)
    {
        anim.Play(state);
    }

    public void OutMenu()
    {
        anim.Play(MenuState.OutMenu);
        playerAnimator.Play("hm_lemon_outMenu");
    }



}

public class MenuState
{
    public static string Normal = "Normal";
    public static string Skin = "Skin";
    public static string Equip = "Equip";
    public static string Skill = "Skill";
    public static string OutMenu = "OutMenu";
}
