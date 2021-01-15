using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int aniID_Horizontal = Animator.StringToHash("Horizontal");
    int aniID_Vertical = Animator.StringToHash("Vertical");
    int aniID_Turning = Animator.StringToHash("Turning");

    [Header("< 玩家参数 >")]
    public float speed = 3;
    public float attackDis = 5f;
    public float aniSpeed = 1.2f;
    [Range(0, 1)]
    public float rotLerp = 0.75f;

    [Header("< 玩家游戏中变量展示 >")]
    public Joystick joystick = null;
    public bool hadJoystick = false;

    Animator anim;
    Vector3 aniDir;

    //Enemy
    public Enemy enemy;
    
    Vector3 movement;

    CharacterController cCtrl;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = aniSpeed;
        cCtrl = GetComponent<CharacterController>();
        movement = new Vector3();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.enemyManager.ClearEnemy(enemy);
        }
    }


    private void FixedUpdate()
    {

        if(!hadJoystick)
        {
            GameUI ui = UIManager.Instance.FindWindowByName<GameUI>(ConStr.GAMEPANEL);
            if(ui!=null && ui.m_Panel.joystick != null)
            {
                hadJoystick = true;
                joystick = ui.m_Panel.joystick;
            }
        }
        ComMovement();
    }

    void ComMovement()
    {
        float h = 0, v = 0;
        if (hadJoystick)
        {
            h = joystick.movement.x;
            v = joystick.movement.z;
        }
        Turning();
        Animating(h, v);
        Move(h, v);
    }


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
        movement = movement * speed * Time.deltaTime;
        cCtrl.SimpleMove(movement);
    }

    #region 转向
    float ratio = 0;
    void Turning()
    {
        enemy = GameManager.Instance.enemyManager.FindCloseEnemy(attackDis);
        if (!Enemy.ReferenceEquals(enemy,null)  )
        { 
            //transform.LookAt(enemy.transform.position);
            Vector3 playerToMouse = enemy.transform.position - transform.position;
            playerToMouse.y = 0f;
            //lerp
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            if (Mathf.Abs(transform.rotation.eulerAngles.y - newRotation.eulerAngles.y) > 5)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 5 * Time.deltaTime);
                ratio = 1;
                anim.SetFloat(aniID_Turning, ratio);
            }
            else
            {
                transform.rotation = newRotation;
                ratio *= 0.9f;
                if (ratio < 0.1) ratio = 0;
                anim.SetFloat(aniID_Turning, ratio);
            }
        }
        if(Enemy.ReferenceEquals(enemy,null) && anim.GetFloat(aniID_Turning) != 0)
        {
            anim.SetFloat(aniID_Turning, 0);
        }
    }
    #endregion
}
