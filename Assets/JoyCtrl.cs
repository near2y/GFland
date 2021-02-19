using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyCtrl : MonoBehaviour , IPointerDownHandler,IPointerUpHandler
{

    public Joystick joystick = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        joystick.ShowHide(true);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.ShowHide(false);
    }
}
