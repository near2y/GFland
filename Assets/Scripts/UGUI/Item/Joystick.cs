using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public RectTransform knob;
    public RectTransform safeArea;
    public RectTransform moveArea;
    private RectTransform canvasRect;
    private Vector2 mouseScreenPos;

    private bool inShow = true;
    private float homingSmooth = 0.5f;
    public float range = 5;

    [HideInInspector]
    public Vector3 movement;

    RectTransform joystickRectTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        joystickRectTransform = transform.GetComponent<RectTransform>();
        canvasRect = RFramework.Instance.m_UIRoot.GetComponent<RectTransform>();
        movement = new Vector3();
        mouseScreenPos = new Vector2();
        ShowHide(false);
    }

    // Update is called once per frame
    private Vector3 fix = new Vector3();
    void Update()
    {
        Smooth();

        if (inShow)
        {
            KnobMove();
        }

        if (!inShow && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            ShowHide(true);
            mouseScreenPos.Set(Input.mousePosition.x, Input.mousePosition.y);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasRect, mouseScreenPos , RFramework.Instance.m_UICamera, out mousePos);
            joystickRectTransform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShowHide(false);
        }


    }
    public void ShowHide(bool state)
    {
        moveArea.gameObject.SetActive(state);
        inShow = state;
    }

    void KnobMove()
    {
        Vector3 mousePos;
        //Vector2 mouseScreenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouseScreenPos.Set(Input.mousePosition.x, Input.mousePosition.y);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            joystickRectTransform, mouseScreenPos, RFramework.Instance.m_UICamera, out mousePos);

        Vector2 joyScreenPos = RectTransformUtility.WorldToScreenPoint(RFramework.Instance.m_UICamera, joystickRectTransform.position);
        Vector2 off = mouseScreenPos - joyScreenPos;
        float dis = Vector2.SqrMagnitude(off);
        float radius = moveArea.rect.width * 0.5f - knob.rect.width * 0.35f;
        float safeRadius = safeArea.rect.width * 0.5f;

        if (dis <= radius * radius)
        {
            knob.position = mousePos;
        }
        else
        {
            Vector3 knobPos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasRect, joyScreenPos + off.normalized * radius, RFramework.Instance.m_UICamera, out knobPos);
            knob.position = knobPos;
        }
        //rotation
        float z;
        if (mousePos.x > joystickRectTransform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - joystickRectTransform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePos - joystickRectTransform.position);
        }
        knob.localRotation = Quaternion.Euler(0, 0, z);


        //movement
        if (Vector3.SqrMagnitude(knob.localPosition) >= safeRadius * safeRadius)
        {
            movement.x = knob.localPosition.x / (moveArea.rect.width * 0.1f);
            movement.z = knob.localPosition.y / (moveArea.rect.height * 0.1f);
        }
    }


    void Smooth()
    {
        if(movement.x != 0)
        {
            movement.x *= homingSmooth;
            if (Mathf.Abs(movement.x) < 0.05f)
            {
                movement.x = 0;
            }
        }

        if (movement.z != 0)
        {
            movement.z *= homingSmooth;
            if (Mathf.Abs(movement.z) < 0.05f)
            {
                movement.z = 0;
            }
        }
    }

}
