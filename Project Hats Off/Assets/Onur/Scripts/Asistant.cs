using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Button bileþeni için gerekli
using UnityEngine.EventSystems; // EventSystem kullanmak için gerekli

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
        // Baþlangýçta butonun rengini veya durumunu ayarlayabiliriz.
        DeselectButton(); // Baþlangýçta buton deselect edilir.
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClickedOnAsistant()
    {
        if (isClickedOnAsistant)
        {
            // Eðer buton zaten seçiliyse, unselect iþlemi yapýlýr
            isClickedOnAsistant = false;
            ButtonManager.canShowOutlineOfEvidiences = false;
            triggerToClosingTheOutline = false; // Unselect sýrasýnda outline'ý kapatma iþlemi
            DeselectButton(); // Butonu deselect et
        }
        else
        {
            // Buton seçili deðilse, select iþlemi yapýlýr
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
        DeselectButton(); // Spawn sonrasý butonu deselect et
    }

    private void SelectButton()
    {
        // Butonu seçili yap
        EventSystem.current.SetSelectedGameObject(assistantButton.gameObject);
    }

    private void DeselectButton()
    {
        // Butonu deselect et
        EventSystem.current.SetSelectedGameObject(null);
    }
}
