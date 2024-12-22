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

    public bool canCloseTheRed = false;

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
                        if (obj.name == "AnimatedCircle1") //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[0]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[3]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[16]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[26]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AsistantEvidienceHover")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText();
                        }
                        if (obj.name == "AnimatedCircle2" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[9]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle2" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[22]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle2" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[8]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle3" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[10]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle3" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[30]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle3" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[31]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle4" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[11]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle4" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[33]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle4" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[32]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle5" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[12]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle5" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[35]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle5" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[34]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle6" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[13]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle6" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[36]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle6" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[37]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle7" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[14]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle7" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[21]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle7" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[29]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle8") //&& suspectInMiddle == 1 // BURASI PROBLEMLI!!!!!!!
                        {
                            Debug.Log("BOZUKK!");
                            /*
                            InstantiateTexts(texts[15]);
                            obj.gameObject.SetActive(false);
                            */
                        }
                        if (obj.name == "AnimatedCircle9" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[38]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle9" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[39]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle9" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[40]);
                            obj.gameObject.SetActive(false);
                        }

                        if (obj.name == "AnimatedCircleLetter" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[14]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircleLetter" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[21]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircleLetter" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[29]);
                            obj.gameObject.SetActive(false);
                        }

                        if (obj.name == "evidence_hover2" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[4]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover2" && suspectScripts[1].notTalking && suspectInMiddle == 2) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[15]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover2" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[27]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover3" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[8]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover4" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[2]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover4" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[17]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover4" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[25]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover5" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[5]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover5" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[18]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover5" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[24]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover6" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[7]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover6" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[20]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover6" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[28]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover7" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[6]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover7" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[19]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover7" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[23]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle10" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[41]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle10" && suspectScripts[1].notTalking && suspectInMiddle == 2) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[42]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle10" && suspectScripts[2].notTalking && suspectInMiddle == 3) 
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[43]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle11" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[44]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle11" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[45]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle11" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[46]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle12" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[47]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle12" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[48]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle12" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[49]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle13" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[50]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle13" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[51]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle13" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi lazým!");
                            InstantiateTexts(texts[52]);
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
        canShowOutlineOfEvidiences = false;

        for (int i = 0; i < suspectScripts.Length; i++)
        {
            suspectScripts[i].notTalking = false;
        }
    }
}