using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectOutline : MonoBehaviour
{
    public SpriteRenderer grayOutlineRenderer;
    public SpriteRenderer redOutlineRenderer;
    public SpriteRenderer redOutlineRenderer2;
    public SpriteRenderer redOutlineRenderer3;
    public SpriteRenderer redOutlineRenderer4;

    public ButtonManager buttonManager;

    public bool canInteract = true;
    public bool notTalking = true;

    public bool isOutlinedRed = false;

    public Asistant asistantMechanic;

    private int objectCount = 0; // Object etiketine sahip nesnelerin sayýsý
    private int photoCount = 0;  // Photo etiketine sahip nesnelerin sayýsý

    void Start()
    {
        if (buttonManager != null)
        {
            buttonManager.OnCanTriggerTalkAgainChanged += HandleCanTriggerTalkAgainChanged;
        }
    }

    void OnDestroy()
    {
        if (buttonManager != null)
        {
            buttonManager.OnCanTriggerTalkAgainChanged -= HandleCanTriggerTalkAgainChanged;
        }
    }
    private void HandleCanTriggerTalkAgainChanged(bool value)
    {
        if (value)
        {
            notTalking = true;
        }
    }

    void Update()
    {
        if (buttonManager.canCloseTheRed) 
        {
            StartCoroutine(CloseTheRed());
        }
        
        if (Input.GetKeyUp(KeyCode.F4))
        {
            redOutlineRenderer.enabled = false;
        }

        if (buttonManager.canCloseTheOutline)
        {
            CloseTheOutline();
        }

        if (buttonManager.CanTriggerTalkAgain)
        {
            notTalking = true;
        }
    }

    private void OnMouseOver()
    {
        if (canInteract )
        {
            grayOutlineRenderer.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (canInteract)
        {
            grayOutlineRenderer.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (isOutlinedRed && notTalking && buttonManager.clickedOnSuspect && buttonManager.canShowOutlineOfEvidiences) 
        {
            StartCoroutine(CloseTheOutlineByClickingAgain());
        }
        
        if (canInteract && notTalking && asistantMechanic.isClickedOnAsistant)
        {
            asistantMechanic.SpawnAsistantText();
        }

        else if (canInteract && notTalking )
        {
            redOutlineRenderer.enabled = true;
            buttonManager.clickedOnSuspect = true;
            buttonManager.canShowOutlineOfEvidiences = true;
            isOutlinedRed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectCount++;
        }
        else if (collision.CompareTag("Photo"))
        {
            photoCount++;
        }

        // Eðer objectCount veya photoCount sýfýrdan büyükse canInteract false olur
        canInteract = (objectCount == 0 && photoCount == 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectCount = Mathf.Max(0, objectCount - 1); // Sayaç negatif olmamalý
        }
        else if (collision.CompareTag("Photo"))
        {
            photoCount = Mathf.Max(0, photoCount - 1); // Sayaç negatif olmamalý
        }

        // Eðer objectCount ve photoCount sýfýr ise canInteract true olur
        canInteract = (objectCount == 0 && photoCount == 0);
    }

    private void CloseTheOutline()
    {
        Debug.Log("Close The Outline Çalýþtý!");
        redOutlineRenderer.enabled = false;
        redOutlineRenderer2.enabled = false;
        redOutlineRenderer3.enabled = false;
        redOutlineRenderer4.enabled = false;
        notTalking = false;
        buttonManager.canCloseTheOutline = false;
        redOutlineRenderer.enabled = false;
    }

    private IEnumerator CloseTheOutlineByClickingAgain()
    {
        print("tekrar tiklayip kapandi kirmizi!");
        redOutlineRenderer.enabled = false;
        redOutlineRenderer2.enabled = false;
        redOutlineRenderer3.enabled = false;
        redOutlineRenderer4.enabled = false;
        notTalking = false;
        buttonManager.canCloseTheOutline = false;
        buttonManager.clickedOnSuspect = false;
        buttonManager.canShowOutlineOfEvidiences = false;
        isOutlinedRed = false;
        yield return new WaitForSeconds(0.5f);
        notTalking = true;
    }

    private IEnumerator CloseTheRed()
    {
        isOutlinedRed = false;
        yield return new WaitForSeconds(0.05f);
        buttonManager.canCloseTheRed = false;

    }

}