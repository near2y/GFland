using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float speed = 3;



    int id_Horizontal = Animator.StringToHash("Horizontal");
    int id_Vertical = Animator.StringToHash("Vertical");

    Animator anim;
    CharacterController cCtrl;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Move");
        movement = new Vector3();
        cCtrl = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        anim.SetFloat(id_Horizontal, movement.x);
        anim.SetFloat(id_Vertical, movement.z);

        movement = movement * speed * Time.deltaTime;
        cCtrl.SimpleMove(movement);


    }

    
}
