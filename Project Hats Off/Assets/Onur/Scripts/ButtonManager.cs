using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare basýldýðý andaki zamaný saklayacak
    public float clickThreshold = 0.2f; // Týklama sayýlacak maksimum süre (0.2 saniye örneði)
    public bool clickedOnSuspect = false;
    public bool canShowOutlineOfEvidiences = false;

    public Asistant asistantMechanic;

    public int suspectInMiddle = 1;

    public SuspectOutline[] suspectScripts;

    private bool canTriggerTalkAgain;
    public bool CanTriggerTalkAgain
    {
        get => canTriggerTalkAgain;
        set
        {
            if (canTriggerTalkAgain != value)
            {
                canTriggerTalkAgain = value;
                OnCanTriggerTalkAgainChanged?.Invoke(value); // Olay tetikleniyor
            }
        }
    }

    public bool startedConversation = false;

    public GameObject[] texts;
    public Transform allTextsParent;

    public bool canCloseTheOutline = false;

    // Event tanýmlandý
    public event Action<bool> OnCanTriggerTalkAgainChanged;

    private void Start()
    {
        CanTriggerTalkAgain = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare butonu basýldýðýnda
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            if (hits.Length > 0)
            {
                clickedObjectOnMouseDown = hits[0].collider.gameObject;
                mouseDownTime = Time.time; // Fare basýldýðý zamaný kaydet
            }
        }

        if (Input.GetMouseButtonUp(0)) // Sol fare butonu býrakýldýðýnda
        {
            float mouseUpTime = Time.time; // Fare býrakýldýðý zamaný al
            float heldTime = mouseUpTime - mouseDownTime; // Basýlý tutma süresini hesapla

            if (heldTime <= clickThreshold) // Eðer basýlý tutma süresi belirlenen eþikten küçükse
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                if (hits.Length > 0)
                {
                    List<GameObject> clickedObjects = hits
                        .Select(hit => hit.collider.gameObject)
                        .OrderByDescending(obj => GetSortingOrder(obj))
                        .ToList();

                    Debug.Log("Týklanan nesneler (üstten alta doðru):");
                    foreach (var obj in clickedObjects)
                    {
                        Debug.Log($"{obj.name} (Layer: {GetSortingOrder(obj)})");

                        if (obj.name == "Clickable2")
                        {
                            Debug.Log("Sonunda týkladýn!");
                        }
                        if (obj.name == "AnimatedCircle1" && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[0]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover" && suspectInMiddle == 1 && suspectScripts[0].notTalking)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[1]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AsistantEvidienceHover")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText();
                        }
                    }
                }
            }

            clickedObjectOnMouseDown = null;
        }
    }

    private int GetSortingOrder(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        }

        return spriteRenderer != null ? spriteRenderer.sortingOrder : int.MinValue;
    }

    private void InstantiateTexts(GameObject textnumber)
    {
        Instantiate(textnumber, allTextsParent);
        canCloseTheOutline = true;
        startedConversation = true;
        CanTriggerTalkAgain = false;
        canShowOutlineOfEvidiences = false;
    }
}