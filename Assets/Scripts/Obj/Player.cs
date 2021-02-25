﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int aniID_Horizontal = Animator.StringToHash("Horizontal");
    int aniID_Vertical = Animator.StringToHash("Vertical");
    int aniID_Turning = Animator.StringToHash("Turning");
    int aniID_Attack = Animator.StringToHash("Attack");
    int aniID_StartGame = Animator.StringToHash("StartGame");

    [Header("< 玩家参数 >")]
    public float speed = 3;
    public float attackDis = 5f;
    public float aniSpeed = 1.2f;
    [Range(0, 1)]
    public float rotLerp = 0.75f;
    public Renderer meshRenderer = null;


    [Header("< 攻击相关 >")]
    public Transform attackEffectPos;
    public GameObject attackEffect;
    public ParticleSystem attackParticle;
    public ParticleSystem shootParticle;
    public bool inAttack;
    public float ATK = 5;
    public Emitter emitter = null;
    public PlayerBoomSkill skill = null;
    public PlayerSkillBar playerSkillBar = null;
    public Transform playerSkillBarTrans = null;

    //[Header("< 玩家游戏中变量展示 >")]
    //public Joystick joystick = null;
    //public bool hadJoystick = false;

    Animator anim;
    Vector3 aniDir;
    //bool hitting = false;

    //glitter
    //float startColorrange = 0;
    float glitterTimer = 0;
    bool inGlitter = false;
    public float glitterTime = 1;
    const string emissionColorStr = "_EmissionColor";
    public List<Renderer> meshRenders;
    Dictionary<int, Color> renderEmissionStandColorDic = new Dictionary<int, Color>();

    //Enemy
    public Enemy enemy;
    
    Vector3 movement;

    CharacterController cCtrl;
    bool inGame = false;

    //ray
    float camRayLength = 100f;
    int floorMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = aniSpeed;
        cCtrl = GetComponent<CharacterController>();
        movement = new Vector3();
        floorMask = LayerMask.GetMask("InputRay");
        //startColorrange = Method.GetColorrangeInRender(meshRenderer);
        //StartGame = false;
    }

    private void Start()
    {
        meshRenders = new List<Renderer>();
        foreach (var item in transform.GetComponentsInChildren<Renderer>())
        {
            renderEmissionStandColorDic.Add(meshRenders.Count, item.material.GetColor(emissionColorStr));
            meshRenders.Add(item);
        }
        playerSkillBar = new PlayerSkillBar(playerSkillBarTrans);
        playerSkillBar.SkillProgress = 0;
    }



    private void FixedUpdate()
    {
        if (!inGame) return;
        //skillBar
        playerSkillBar.Update();

        ////Move
        //if(!hadJoystick)
        //{
        //    GameUI ui = UIManager.Instance.FindWindowByName<GameUI>(ConStr.GAMEPANEL);
        //    if(ui!=null && ui.m_Panel.joystick != null)
        //    {
        //        hadJoystick = true;
        //        joystick = ui.m_Panel.joystick;
        //    }
        //}
        //ComMovement();

        Attack();

        if (inGlitter)
        {
            glitterTimer += Time.deltaTime;
            for (int i = 0; i < meshRenders.Count; i++)
            {
                Color color = Color.Lerp(meshRenders[i].material.GetColor(emissionColorStr), renderEmissionStandColorDic[i], glitterTimer / glitterTime);
                meshRenders[i].material.SetColor(emissionColorStr, color);
            }
            if (glitterTimer >= glitterTime) inGlitter = false;
        }

    }

    public void OnWin()
    {
        inGame = false;
        //停止发射武器
        emitter.Attack(false);
        anim.Play("OverShow");
        SceneManager.Instance.gameUI.ShowClear();
    }

    void OverInStage()
    {
        //TODO
        return;
        emitter.trajactoryCount = GameManager.Instance.playerTrajactoryCount;
        emitter.SetActive(true);
        SceneManager.Instance.StartWave();
        inGame = true;

        skill = SceneManager.Instance.effectManager.GetEffect(4006).GetComponent<PlayerBoomSkill>();
        skill.player = transform;
    }

    public void StartGame()
    {
        if (inGame) return;
        inGame = true;
        skill = SceneManager.Instance.effectManager.GetEffect(4006).GetComponent<PlayerBoomSkill>();
        skill.player = transform;
        anim.SetBool(aniID_StartGame, true);
        SceneManager.Instance.gameUI = UIManager.Instance.PopUpWindow(ConStr.GAMEPANEL, true) as GameUI;
    }


    public void OverChooseProp()
    {
        emitter.trajactoryCount = GameManager.Instance.playerTrajactoryCount;
        emitter.SetActive(true);
        SceneManager.Instance.StartWave();
        if (!SceneManager.Instance.bossGame) StartGame();
    }

    void FullGround()
    {
        GameObject effect = SceneManager.Instance.effectManager.GetEffect(4007);
        effect.transform.position = transform.position;
        effect.transform.localScale = Vector3.one;
        Handheld.Vibrate();
    }

    //void ComMovement()
    //{
    //    float h = 0, v = 0;
    //    if (hadJoystick)
    //    {
    //        h = joystick.movement.x;
    //        v = joystick.movement.z;
    //    }
    //    Move(h, v);
    //    Turning();
    //    Animating(h, v);
    //}


    void Animating(float h,float v)
    {
        aniDir.Set(h, 0, v);
        aniDir = transform.InverseTransformDirection(aniDir);
        float tarH = Mathf.Lerp(anim.GetFloat(aniID_Horizontal), aniDir.x,rotLerp);
        float tarV = Mathf.Lerp(anim.GetFloat(aniID_Vertical), aniDir.z, rotLerp);
        anim.SetFloat(aniID_Horizontal, tarH);
        anim.SetFloat(aniID_Vertical, tarV);
    }

    void Move(float h,float v)
    {
        movement.x = h;
        movement.z = v;
        if (h*h + v*v > 1)
        {
            movement = movement.normalized;
        }
        
        movement =  movement* speed * Time.deltaTime;

        //cCtrl.Move(movement);
        cCtrl.SimpleMove(movement);
        //transform.Translate(movement);
    }

    void Attack()
    {

        if (inAttack)
        {
            //attackEffect.transform.position = attackEffectPos.position;
            //attackEffect.transform.rotation = transform.rotation;
            if(enemy == null)
            {
                //没有目标了，停止攻击
                //attackParticle.Stop();
                inAttack = false;
                //shootParticle.gameObject.SetActive(false);
                emitter.Attack(false);
            }
        }
        else
        {
            if(enemy != null)
            {
                //attackParticle.Play();
                inAttack = true;
                //shootParticle.gameObject.SetActive(true);
                emitter.Attack(true);
            }
        }
        anim.SetBool(aniID_Attack, inAttack);
    }

    //public bool StartGame
    //{
    //    set
    //    {
    //        anim.SetBool(aniID_StartGame, value);
    //    }
    //}


    #region 转向
    float ratio = 0;
    Vector3 point = new Vector3();
    void Turning()
    {
        enemy = SceneManager.Instance.enemyManager.FindCloseEnemy(attackDis);
        if (enemy != null  )
        {
            point = enemy.transform.position;
        }
        else
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                point = floorHit.point;
            }
            else
            {
                return;
            }
        }
        //转向具体点
        //transform.LookAt(enemy.transform.position);
        Vector3 playerToMouse = point - transform.position;
        playerToMouse.y = 0f;
        //lerp
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        if (Mathf.Abs(transform.rotation.eulerAngles.y - newRotation.eulerAngles.y) > 10)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 5 * Time.deltaTime);
            ratio = 1;
            anim.SetFloat(aniID_Turning, ratio);
        }
        else
        {
            //emitter.bulletPos.LookAt(enemy.transform);
            transform.rotation = newRotation;
            ratio *= 0.5f;
            if (ratio < 0.1) ratio = 0;
            anim.SetFloat(aniID_Turning, 0);
        }

        if (enemy == null && anim.GetFloat(aniID_Turning) != 0)
        {
            anim.SetFloat(aniID_Turning, 0);
        }
    }
    #endregion

    //闪光
    void Glitter()
    {
        for (int i = 0; i < meshRenders.Count; i++)
        {
            meshRenders[i].material.SetColor(emissionColorStr, Color.white);
        }

        glitterTimer = 0;
        inGlitter = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        Glitter();
        //hitting = Method.SetRenderColorRange(meshRenderer, 5);

    }
}
