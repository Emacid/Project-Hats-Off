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
    public bool isManuallySelected = false; // Manuel seçim durumu
    private MouseIconChanger MouseIconChanger;
    public Button button;
    private GameObject ballotBlack;

    // Eski layer'larý saklamak için bir sözlük
    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>();

    void Start()
    {
        DeselectButton(); // Baþlangýçta buton deselect edilir.
        MouseIconChanger = GameObject.Find("Mouse Icon Changer").GetComponent<MouseIconChanger>();
        button = gameObject.GetComponent<Button>();
        ballotBlack = GameObject.Find("BallotBlack");
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
            DeselectButton();
            print("click on stamper!");
        }
        else
        {
            // Buton seçili deðilse, select iþlemi yapýlýr
            isClickedOnAsistant = true;
            SelectButton();
        }
    }

    private void SelectButton()
    {
        isManuallySelected = true; // Manuel olarak seçildiðini iþaretle
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
        isManuallySelected = false; // Manuel seçim durumunu sýfýrla
        EventSystem.current.SetSelectedGameObject(null);
        SetLayersBack();
        print("deselect on stamper!");
    }

    private void SetLayerRecursively(Transform obj, int layer)
    {
        obj.gameObject.layer = layer; // Objeye layer'ý uygula
        foreach (Transform child in obj)
        {
            SetLayerRecursively(child, layer); // Child objelere de ayný iþlemi uygula
        }
    }

    public void ChangeLayers()
    {
        originalLayers.Clear(); // Önce sözlüðü temizliyoruz

        foreach (GameObject obj in objectsToIgnoreRaycast)
        {
            SaveAndSetLayerRecursively(obj.transform, 2); // Layer'ý 2 olarak ayarla
        }
    }

    public void SetLayersBack()
    {
        foreach (var entry in originalLayers)
        {
            RestoreLayerRecursively(entry.Key.transform, entry.Value); // Orijinal layer'larý geri yükle
        }
    }

    private void SaveAndSetLayerRecursively(Transform obj, int layer)
    {
        if (!originalLayers.ContainsKey(obj.gameObject))
        {
            originalLayers[obj.gameObject] = obj.gameObject.layer; // Eski layer'ý sakla
        }

        obj.gameObject.layer = layer; // Yeni layer'ý uygula

        foreach (Transform child in obj)
        {
            SaveAndSetLayerRecursively(child, layer); // Child objelere de ayný iþlemi uygula
        }
    }

    private void RestoreLayerRecursively(Transform obj, int originalLayer)
    {
        obj.gameObject.layer = originalLayer; // Orijinal layer'ý geri yükle

        foreach (Transform child in obj)
        {
            if (originalLayers.TryGetValue(child.gameObject, out int childOriginalLayer))
            {
                RestoreLayerRecursively(child, childOriginalLayer); // Child objelere de uygula
            }
        }
    }
}
