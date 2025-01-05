using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Asistant : MonoBehaviour
{
    public bool isClickedOnAsistant = false;

    public ButtonManager ButtonManager;
    //
    public GameObject[] asistantTextForMidterm;
    public Transform ParentForSpawn;

    public bool triggerToClosingTheOutline = false;

    public Button assistantButton;
    public Sprite[] asistantButtonSprites;

    public SuspectOutline[] suspectOutlines;

    private bool isManuallySelected = false; // Manuel se�im durumu

    void Start()
    {
        DeselectButton(); // Ba�lang��ta buton deselect edilir.
        assistantButton.image.sprite = asistantButtonSprites[0];
    }

    void Update()
    {
        // Buton se�iliyken ba�ka bir yere t�klan�rsa tekrar se�ili hale getir
        if (isClickedOnAsistant && !EventSystem.current.currentSelectedGameObject)
        {
            // EventSystem'de se�ili obje yoksa ve buton aktif durumdaysa
            SelectButton();
        }
    }

    public void ClickedOnAsistant()
    {
        if (isClickedOnAsistant)
        {
            // E�er buton zaten se�iliyse, unselect i�lemi yap�l�r
            isClickedOnAsistant = false;
            ButtonManager.canShowOutlineOfEvidiences = false;
            triggerToClosingTheOutline = false;
            DeselectButton();
        }
        else
        {
            if (ButtonManager.clickedOnSuspect && ButtonManager.startedConversation) 
            {
                //SpawnAsistantText(8);
            }
                else if (ButtonManager.clickedOnSuspect)
            {
                
                foreach (var outline in suspectOutlines)
                {
                    outline.CloseTheOutlineByClickingAgainn();
                    //outline.isOutlinedRed = false;
                }
            }
            
            {
                // Buton se�ili de�ilse, select i�lemi yap�l�r
                isClickedOnAsistant = true;
            ButtonManager.canShowOutlineOfEvidiences = true;
            SelectButton();
            }
        }
    }

    public void SpawnAsistantText(int textNumber)
    {
        Instantiate(asistantTextForMidterm[textNumber], ParentForSpawn);
        isClickedOnAsistant = false;
        triggerToClosingTheOutline = true;
        DeselectButton();
    }

    private void SelectButton()
    {
        isManuallySelected = true; // Manuel olarak se�ildi�ini i�aretle
        EventSystem.current.SetSelectedGameObject(assistantButton.gameObject);
        assistantButton.image.sprite = asistantButtonSprites[1];
    }

    private void DeselectButton()
    {
        isManuallySelected = false; // Manuel se�im durumunu s�f�rla
        EventSystem.current.SetSelectedGameObject(null);
        assistantButton.image.sprite = asistantButtonSprites[0];
    }
}
