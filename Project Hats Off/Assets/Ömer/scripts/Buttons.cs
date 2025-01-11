using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;
    private bool isPressed;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger("start");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("end");
    }
}


