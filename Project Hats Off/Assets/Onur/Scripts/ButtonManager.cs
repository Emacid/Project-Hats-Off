using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare bas�ld��� andaki zaman� saklayacak
    public float clickThreshold = 0.2f; // T�klama say�lacak maksimum s�re (0.2 saniye �rne�i)
    public bool clickedOnSuspect = false;

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

    public GameObject text01;
    public Transform allTextsParent;

    public bool canCloseTheOutline = false;

    // Event tan�mland�
    public event Action<bool> OnCanTriggerTalkAgainChanged;

    private void Start()
    {
        CanTriggerTalkAgain = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare butonu bas�ld���nda
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            if (hits.Length > 0)
            {
                clickedObjectOnMouseDown = hits[0].collider.gameObject;
                mouseDownTime = Time.time; // Fare bas�ld��� zaman� kaydet
            }
        }

        if (Input.GetMouseButtonUp(0)) // Sol fare butonu b�rak�ld���nda
        {
            float mouseUpTime = Time.time; // Fare b�rak�ld��� zaman� al
            float heldTime = mouseUpTime - mouseDownTime; // Bas�l� tutma s�resini hesapla

            if (heldTime <= clickThreshold) // E�er bas�l� tutma s�resi belirlenen e�ikten k���kse
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                if (hits.Length > 0)
                {
                    List<GameObject> clickedObjects = hits
                        .Select(hit => hit.collider.gameObject)
                        .OrderByDescending(obj => GetSortingOrder(obj))
                        .ToList();

                    Debug.Log("T�klanan nesneler (�stten alta do�ru):");
                    foreach (var obj in clickedObjects)
                    {
                        Debug.Log($"{obj.name} (Layer: {GetSortingOrder(obj)})");

                        if (obj.name == "Clickable2")
                        {
                            Debug.Log("Sonunda t�klad�n!");
                        }
                        else if (obj.name == "AnimatedCircle1")
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(text01);
                            obj.gameObject.SetActive(false);
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
    }
}
