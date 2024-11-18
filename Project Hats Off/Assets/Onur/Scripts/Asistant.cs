using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Button bile�eni i�in gerekli
using UnityEngine.EventSystems; // EventSystem kullanmak i�in gerekli

public class Asistant : MonoBehaviour
{
    public bool isClickedOnAsistant = false;

    public ButtonManager ButtonManager;

    public GameObject asistantTextForMidterm;
    public Transform ParentForSpawn;

    public bool triggerToClosingTheOutline = false;

    public Button assistantButton; // Butonu referans olarak ekliyoruz

    // Start is called before the first frame update
    void Start()
    {
        // Ba�lang��ta butonun rengini veya durumunu ayarlayabiliriz.
        DeselectButton(); // Ba�lang��ta buton deselect edilir.
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClickedOnAsistant()
    {
        if (isClickedOnAsistant)
        {
            // E�er buton zaten se�iliyse, unselect i�lemi yap�l�r
            isClickedOnAsistant = false;
            ButtonManager.canShowOutlineOfEvidiences = false;
            triggerToClosingTheOutline = false; // Unselect s�ras�nda outline'� kapatma i�lemi
            DeselectButton(); // Butonu deselect et
        }
        else
        {
            // Buton se�ili de�ilse, select i�lemi yap�l�r
            isClickedOnAsistant = true;
            ButtonManager.canShowOutlineOfEvidiences = true;
            SelectButton(); // Butonu select et
        }
    }

    public void SpawnAsistantText()
    {
        Instantiate(asistantTextForMidterm, ParentForSpawn);
        isClickedOnAsistant = false;
        triggerToClosingTheOutline = true;
        DeselectButton(); // Spawn sonras� butonu deselect et
    }

    private void SelectButton()
    {
        // Butonu se�ili yap
        EventSystem.current.SetSelectedGameObject(assistantButton.gameObject);
    }

    private void DeselectButton()
    {
        // Butonu deselect et
        EventSystem.current.SetSelectedGameObject(null);
    }
}
