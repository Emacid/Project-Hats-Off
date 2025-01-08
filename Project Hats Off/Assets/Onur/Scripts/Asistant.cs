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

    private bool isManuallySelected = false; // Manuel se�im durumu
    private float lastSpawnTime = -5f; // Son tetiklenme zaman�n� tutar (ba�lang��ta 5 saniye �nce tetiklenmi� varsay�l�r)

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
                }
            }

            // Buton se�ili de�ilse, select i�lemi yap�l�r
            isClickedOnAsistant = true;
            ButtonManager.canShowOutlineOfEvidiences = true;
            SelectButton();
        }
    }

    public void SpawnAsistantText(int textNumber)
    {
        
        // E�er son tetiklenme �zerinden 5 saniyeden fazla ge�mi�se, fonksiyonu �al��t�r
        if (Time.time - lastSpawnTime > 5f)
        {
            Instantiate(asistantTextForMidterm[textNumber], ParentForSpawn);
            tmpColorChangerForAsistant.ChangeTMPColorsToBlack();
            isClickedOnAsistant = false;
            triggerToClosingTheOutline = true;
            DeselectButton();
            lastSpawnTime = Time.time; // Tetiklenme zaman�n� g�ncelle
        }
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
