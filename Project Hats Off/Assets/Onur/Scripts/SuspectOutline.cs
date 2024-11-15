using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectOutline : MonoBehaviour
{
    public SpriteRenderer grayOutlineRenderer;
    public SpriteRenderer redOutlineRenderer;

    public ButtonManager buttonManager;

    public bool canInteract = true;
    private bool notTalking = true;

    private int objectCount = 0; // Object etiketine sahip nesnelerin say�s�
    private int photoCount = 0;  // Photo etiketine sahip nesnelerin say�s�

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F4))
        {
            redOutlineRenderer.enabled = false;
        }

        if (buttonManager.canCloseTheOutline) 
        {
            CloseTheOutline();
        }
    }

    private void OnMouseOver()
    {
        if (canInteract)
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
        if (canInteract && notTalking)
        {
            redOutlineRenderer.enabled = true;
            buttonManager.clickedOnSuspect = true;
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

        // E�er objectCount veya photoCount s�f�rdan b�y�kse canInteract false olur
        canInteract = (objectCount == 0 && photoCount == 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            objectCount = Mathf.Max(0, objectCount - 1); // Saya� negatif olmamal�
        }
        else if (collision.CompareTag("Photo"))
        {
            photoCount = Mathf.Max(0, photoCount - 1); // Saya� negatif olmamal�
        }

        // E�er objectCount ve photoCount s�f�r ise canInteract true olur
        canInteract = (objectCount == 0 && photoCount == 0);
    }

    private void CloseTheOutline() 
    {
        redOutlineRenderer.enabled = false;
        notTalking = false;
        buttonManager.canCloseTheOutline = false;
    }

}
