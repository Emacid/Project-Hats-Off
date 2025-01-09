using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Stamper : MonoBehaviour
{
    public bool isClickedOnAsistant = false;
    public GameObject allsuspects;
    public GameObject ballotObject;
    public bool isBallotUp = false;
    public Button assistantButton;
    public GameObject[] objectsToIgnoreRaycast;
    public bool isManuallySelected = false; // Manuel se�im durumu
    private MouseIconChanger MouseIconChanger;
    public Button button;
    private GameObject ballotBlack;

    // Eski layer'lar� saklamak i�in bir s�zl�k
    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>();

    void Start()
    {
        DeselectButton(); // Ba�lang��ta buton deselect edilir.
        MouseIconChanger = GameObject.Find("Mouse Icon Changer").GetComponent<MouseIconChanger>();
        button = gameObject.GetComponent<Button>();
        ballotBlack = GameObject.Find("BallotBlack");
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
            DeselectButton();
            print("click on stamper!");
        }
        else
        {
            // Buton se�ili de�ilse, select i�lemi yap�l�r
            isClickedOnAsistant = true;
            SelectButton();
        }
    }

    private void SelectButton()
    {
        isManuallySelected = true; // Manuel olarak se�ildi�ini i�aretle
        EventSystem.current.SetSelectedGameObject(assistantButton.gameObject);
        if (!isBallotUp)
        {
            isBallotUp = true;
            //SetLayerRecursively(allsuspects.transform, 2);
            Instantiate(ballotObject);
            ChangeLayers();
            button.interactable = false;
            ballotBlack.SetActive(true);
        }
        
    }

    public void DeselectButton()
    {
        isManuallySelected = false; // Manuel se�im durumunu s�f�rla
        EventSystem.current.SetSelectedGameObject(null);
        SetLayersBack();
        print("deselect on stamper!");
    }

    private void SetLayerRecursively(Transform obj, int layer)
    {
        obj.gameObject.layer = layer; // Objeye layer'� uygula
        foreach (Transform child in obj)
        {
            SetLayerRecursively(child, layer); // Child objelere de ayn� i�lemi uygula
        }
    }

    public void ChangeLayers()
    {
        originalLayers.Clear(); // �nce s�zl��� temizliyoruz

        foreach (GameObject obj in objectsToIgnoreRaycast)
        {
            SaveAndSetLayerRecursively(obj.transform, 2); // Layer'� 2 olarak ayarla
        }
    }

    public void SetLayersBack()
    {
        foreach (var entry in originalLayers)
        {
            RestoreLayerRecursively(entry.Key.transform, entry.Value); // Orijinal layer'lar� geri y�kle
        }
    }

    private void SaveAndSetLayerRecursively(Transform obj, int layer)
    {
        if (!originalLayers.ContainsKey(obj.gameObject))
        {
            originalLayers[obj.gameObject] = obj.gameObject.layer; // Eski layer'� sakla
        }

        obj.gameObject.layer = layer; // Yeni layer'� uygula

        foreach (Transform child in obj)
        {
            SaveAndSetLayerRecursively(child, layer); // Child objelere de ayn� i�lemi uygula
        }
    }

    private void RestoreLayerRecursively(Transform obj, int originalLayer)
    {
        obj.gameObject.layer = originalLayer; // Orijinal layer'� geri y�kle

        foreach (Transform child in obj)
        {
            if (originalLayers.TryGetValue(child.gameObject, out int childOriginalLayer))
            {
                RestoreLayerRecursively(child, childOriginalLayer); // Child objelere de uygula
            }
        }
    }
}
