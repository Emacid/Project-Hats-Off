using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Asistant : MonoBehaviour
{
    public bool isClickedOnAsistant = false;

    public ButtonManager ButtonManager;
    public GameObject[] asistantTextForMidterm;
    public Transform ParentForSpawn;

    public bool triggerToClosingTheOutline = false;

    public TMPColorChanger tmpColorChangerForAsistant;

    public Button assistantButton;
    public Sprite[] asistantButtonSprites;

    public SuspectOutline[] suspectOutlines;

    private bool isManuallySelected = false; // Manuel seçim durumu
    private float lastSpawnTime = -5f; // Son tetiklenme zamanýný tutar (baþlangýçta 5 saniye önce tetiklenmiþ varsayýlýr)

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
                }
            }

            // Buton seçili deðilse, select iþlemi yapýlýr
            isClickedOnAsistant = true;
            ButtonManager.canShowOutlineOfEvidiences = true;
            SelectButton();
        }
    }

    public void SpawnAsistantText(int textNumber)
    {
        
        // Eðer son tetiklenme üzerinden 5 saniyeden fazla geçmiþse, fonksiyonu çalýþtýr
        if (Time.time - lastSpawnTime > 5f)
        {
            Instantiate(asistantTextForMidterm[textNumber], ParentForSpawn);
            tmpColorChangerForAsistant.ChangeTMPColorsToBlack();
            isClickedOnAsistant = false;
            triggerToClosingTheOutline = true;
            DeselectButton();
            lastSpawnTime = Time.time; // Tetiklenme zamanýný güncelle
        }
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
