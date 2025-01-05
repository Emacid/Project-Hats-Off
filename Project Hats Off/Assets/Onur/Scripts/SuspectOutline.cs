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

    private BoxCollider2D boxCollider2D;

    public ButtonManager buttonManager;

    public bool canInteract = true;
    public bool notTalking = true;

    public bool isOutlinedRed = false;

    public Asistant asistantMechanic;

    private int objectCount = 0; // Object etiketine sahip nesnelerin sayýsý
    private int photoCount = 0;  // Photo etiketine sahip nesnelerin sayýsý

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

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

        if (buttonManager.canCloseTheOutline)
        {
            CloseTheOutline();
        }

        if (buttonManager.CanTriggerTalkAgain)
        {
            notTalking = true;
        }

        HandleMouseInteractions();

        if(Input.GetKeyDown(KeyCode.C)) 
        {
            boxCollider2D.enabled = !boxCollider2D.enabled;
        }
    }

    private void HandleMouseInteractions()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider == boxCollider2D)
        {
            if (canInteract)
            {
                grayOutlineRenderer.enabled = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }
        else
        {
            grayOutlineRenderer.enabled = false;
        }
    }

    private void HandleMouseClick()
    {
        if (isOutlinedRed && notTalking && buttonManager.clickedOnSuspect && buttonManager.canShowOutlineOfEvidiences)
        {
            // Kýrmýzý outline'ý tekrar týklayýnca kapatma
            StartCoroutine(CloseTheOutlineByClickingAgain());
        }
        else if (asistantMechanic.isClickedOnAsistant)
        {
            // Asistant mekanizmasý ile etkileþim
            if(buttonManager.suspectInMiddle == 1) 
            {
                asistantMechanic.SpawnAsistantText(8);
            }
            else if (buttonManager.suspectInMiddle == 2)
            {
                asistantMechanic.SpawnAsistantText(9);
            }
            else if (buttonManager.suspectInMiddle == 3)
            {
                asistantMechanic.SpawnAsistantText(10);
            }

        }
        else if (canInteract && notTalking)
        {
            // Kýrmýzý outline'ý aktif et
            redOutlineRenderer.enabled = true;
            redOutlineRenderer2.enabled = true;
            redOutlineRenderer3.enabled = true;
            redOutlineRenderer4.enabled = true;

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

    public void CloseTheOutline()
    {
        Debug.Log("Close The Outline Çalýþtý!");
        redOutlineRenderer.enabled = false;
        redOutlineRenderer2.enabled = false;
        redOutlineRenderer3.enabled = false;
        redOutlineRenderer4.enabled = false;
        notTalking = false;
        buttonManager.canCloseTheOutline = false;
    }

    public IEnumerator CloseTheOutlineByClickingAgain()
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

    public void CloseTheOutlineByClickingAgainn() 
    {
        StartCoroutine(CloseTheOutlineByClickingAgain());
    }

}
