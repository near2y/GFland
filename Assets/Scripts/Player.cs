using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoSingleton<Player>
{
    int aniID_Horizontal = Animator.StringToHash("Horizontal");
    int aniID_Vertical = Animator.StringToHash("Vertical");
    int aniID_Turning = Animator.StringToHash("Turning");


    public float speed = 3;
    public float aniSpeed = 1.2f;
    [Range(0,1)]
    public float rotLerp = 0.75f;

    public Joystick joystick = null;
    public bool hadJoystick = false;

    Animator anim;
    public Vector3 aniDir;

    //Enemy
    public Transform enemy;
    
    Vector3 movement;

    int floorMask;
    float camRayLength = 100f;

    CharacterController cCtrl;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        anim.speed = aniSpeed;
        cCtrl = GetComponent<CharacterController>();
        movement = new Vector3();
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

    float ratio = 0;
    void Turning()
    {
        //Vector3 playerToMouse = enemy.position;
        //playerToMouse.y = 0f;

        //Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

        //if (Mathf.Abs(transform.rotation.eulerAngles.y - newRotation.eulerAngles.y) > 5)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 5 * Time.deltaTime);
        //    //ratio = (newRotation.eulerAngles.y - transform.rotation.eulerAngles.y) / offY;
        //    //Debug.Log(ratio);
        //    ratio = 1;
        //    anim.SetFloat(aniID_Turning, ratio);
        //}
        //else
        //{
        //    transform.rotation = newRotation;
        //    ratio *= 0.9f;
        //    if (ratio < 0.1) ratio = 0;
        //    anim.SetFloat(aniID_Turning, ratio);
        //}

        transform.LookAt(enemy.position);

        return;


        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit floorHit;
        //if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //{
        //    Vector3 playerToMouse = floorHit.point - transform.position;
        //    playerToMouse.y = 0f;

        //    Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

        //    if (Mathf.Abs(transform.rotation.eulerAngles.y - newRotation.eulerAngles.y)>5)
        //    {
        //        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 5 * Time.deltaTime);
        //        //ratio = (newRotation.eulerAngles.y - transform.rotation.eulerAngles.y) / offY;
        //        //Debug.Log(ratio);
        //        ratio = 1;
        //        anim.SetFloat(aniID_Turning, ratio);
        //    }
        //    else
        //    {
        //        transform.rotation = newRotation;
        //        ratio *= 0.9f;
        //        if (ratio < 0.1) ratio = 0;
        //        anim.SetFloat(aniID_Turning, ratio);
        //    }
        //}
    }
}
