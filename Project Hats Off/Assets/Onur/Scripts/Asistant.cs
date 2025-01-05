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

    private bool isManuallySelected = false; // Manuel seçim durumu

    void Start()
    {
        DeselectButton(); // Baþlangýçta buton deselect edilir.
        assistantButton.image.sprite = asistantButtonSprites[0];
    }

    void Update()
    {
        // Buton seçiliyken baþka bir yere týklanýrsa tekrar seçili hale getir
        if (isClickedOnAsistant && !EventSystem.current.currentSelectedGameObject)
        {
            // EventSystem'de seçili obje yoksa ve buton aktif durumdaysa
            SelectButton();
        }
    }

    public void ClickedOnAsistant()
    {
        if (isClickedOnAsistant)
        {
            // Eðer buton zaten seçiliyse, unselect iþlemi yapýlýr
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
                // Buton seçili deðilse, select iþlemi yapýlýr
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
        isManuallySelected = true; // Manuel olarak seçildiðini iþaretle
        EventSystem.current.SetSelectedGameObject(assistantButton.gameObject);
        assistantButton.image.sprite = asistantButtonSprites[1];
    }

    private void DeselectButton()
    {
        isManuallySelected = false; // Manuel seçim durumunu sýfýrla
        EventSystem.current.SetSelectedGameObject(null);
        assistantButton.image.sprite = asistantButtonSprites[0];
    }
}
