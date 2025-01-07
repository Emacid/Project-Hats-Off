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
    public bool canShowOutlineOfEvidiences = false;

    public Asistant asistantMechanic;
    public GameObject notepadPopUp;

    public int suspectInMiddle = 1;

    public bool canCloseTheRed = false;

    public SuspectOutline[] suspectScripts;
    public NotePad[] notepadPages;

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

    // Event tan�mland�
    public event Action<bool> OnCanTriggerTalkAgainChanged;

    private void Start()
    {
        CanTriggerTalkAgain = false;
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            FillNotepad(0,0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            FillNotepad(1, 2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FillNotepad(1, 3);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            FillNotepad(2, 3);
        }
        */

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
                        if (obj.name == "AnimatedCircle1") //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[0]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[3]);
                            FillNotepad(0, 0);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[16]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[26]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AsistantEvidienceHover")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(0);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover2")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(1);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover3")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(2);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover4")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(16);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover5")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(17);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover6")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(5);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover7")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(6);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AsistantEvidienceHover8")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(7);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "OutlineCaseFile")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(11);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "OutlineNotepad")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(12);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "OutlineLetter")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(13);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "OutlineIdBook")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(14);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "OutlineNewspaper")
                        {
                            Debug.Log("Asistan Tetiklendi!!!");
                            asistantMechanic.SpawnAsistantText(15);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle2" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[9]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle2" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[22]);
                            FillNotepad(1, 2);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle2" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[8]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle3" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[10]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle3" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[30]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle3" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[31]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle4" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[11]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle4" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[33]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle4" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[32]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle5" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[12]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle5" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[35]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle5" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[34]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle6" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[13]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle6" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[36]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle6" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[37]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle7" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[14]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle7" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[21]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle7" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
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
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[38]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle9" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[39]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle9" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[40]);
                            obj.gameObject.SetActive(false);
                        }

                        if (obj.name == "AnimatedCircleLetter" && suspectScripts[0].notTalking && suspectInMiddle == 1) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[14]);
                            FillNotepad(0, 2);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircleLetter" && suspectScripts[1].notTalking && suspectInMiddle == 2) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[21]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircleLetter" && suspectScripts[2].notTalking && suspectInMiddle == 3) //&& suspectInMiddle == 1
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[29]);
                            obj.gameObject.SetActive(false);
                        }

                        if (obj.name == "evidence_hover2" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[4]);
                            FillNotepad(0, 0);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover2" && suspectScripts[1].notTalking && suspectInMiddle == 2) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[15]);
                            FillNotepad(1, 0);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover2" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[27]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover3" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[8]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover4" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[2]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover4" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[17]);
                            FillNotepad(1, 0);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover4" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[25]);
                            FillNotepad(2, 0);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover5" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[5]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover5" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[18]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover5" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[24]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover6" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[7]);
                            FillNotepad(0, 1);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover6" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[20]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover6" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[28]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "evidence_hover7" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[6]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover7" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[19]);
                            FillNotepad(1, 1);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "evidence_hover7" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[23]);
                            FillNotepad(2, 3);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle10" && suspectScripts[0].notTalking && suspectInMiddle == 1) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[41]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle10" && suspectScripts[1].notTalking && suspectInMiddle == 2) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[42]);
                            FillNotepad(1, 3);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle10" && suspectScripts[2].notTalking && suspectInMiddle == 3) 
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[43]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle11" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[44]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle11" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[45]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle11" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[46]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle12" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[47]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle12" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[48]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle12" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[49]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle13" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[50]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle13" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[51]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle13" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[52]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle14" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[53]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle14" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[54]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle14" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[55]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle15" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[56]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle15" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[57]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle15" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[58]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle16" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[59]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle16" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[60]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle16" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[61]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle17" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[62]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle17" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[63]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle17" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[64]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle18" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[65]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle18" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[66]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle18" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[67]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle19" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[68]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle19" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[69]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle19" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[70]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle20" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[71]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle20" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[72]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle20" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[73]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle21" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[74]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle21" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[75]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle21" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[76]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle22" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[77]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle22" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[78]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle22" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[79]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle23" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[80]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle23" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[81]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle23" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[82]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle24" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[83]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle24" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[84]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle24" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[85]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle25" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[86]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle25" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[87]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle25" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[88]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle26" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[89]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle26" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[90]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle26" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[91]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle27" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[92]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle27" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[93]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle27" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[94]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle28" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[95]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle28" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[96]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle28" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[97]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle29" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[98]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle29" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[99]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle29" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[100]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle30" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[101]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle30" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[102]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle30" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[103]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle31" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[104]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle31" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[105]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle31" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[106]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle32" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[107]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle32" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[108]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle32" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[109]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle33" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[110]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle33" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[111]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle33" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[112]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle34" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[113]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle34" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[114]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle34" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[115]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle35" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[116]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle35" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[117]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle35" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[118]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle36" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[119]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle36" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[120]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle36" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[121]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle37" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[122]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle37" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[123]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle37" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[124]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle38" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[125]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle38" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[126]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle38" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[127]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle39" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[128]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle39" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[129]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle39" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[130]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle40" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[131]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle40" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[132]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle40" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[133]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle41" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[134]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle41" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[135]);
                            FillNotepad(1, 4);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle41" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[136]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle42" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[137]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle42" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[138]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle42" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[139]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle43" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[140]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle43" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[141]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle43" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[142]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle44" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[143]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle44" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[144]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle44" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[145]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle45" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[146]);
                            FillNotepad(0, 3);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle45" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[147]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle45" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[148]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle46" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[149]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle46" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[150]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle46" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[151]);
                            FillNotepad(2, 2);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle47" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[152]);
                            FillNotepad(0, 4);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle47" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[153]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle47" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[154]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle48" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[155]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle48" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[156]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle48" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[157]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle49" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[158]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle49" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[159]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle49" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[160]);
                            FillNotepad(2, 1);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle50" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[161]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle50" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[162]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle50" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[163]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle51" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[164]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle51" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[165]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle51" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[166]);
                            FillNotepad(2, 4);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle52" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[167]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle52" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[168]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle52" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[169]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle53" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[170]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle53" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[171]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle53" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[172]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle54" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[173]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle54" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[174]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle54" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[175]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle55" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[176]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle55" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[177]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle55" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[178]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle56" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[179]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle56" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[180]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle56" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[181]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle57" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[182]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle57" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[183]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle57" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[184]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle58" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[185]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle58" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[186]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle58" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[187]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle59" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[188]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle59" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[189]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle59" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[190]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle60" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[191]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle60" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[192]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle60" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[193]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle61" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[194]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle61" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[195]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle61" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[196]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle62" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[197]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle62" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[198]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle62" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[199]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle63" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[200]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle63" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[201]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle63" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[202]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle64" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[203]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle64" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[204]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle64" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[205]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle65" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[206]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle65" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[207]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle65" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[208]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle66" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[209]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle66" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[210]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle66" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[211]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle67" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[212]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle67" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[213]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle67" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[214]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle68" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[215]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle68" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[216]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle68" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[217]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle69" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[218]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle69" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[219]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle69" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[220]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle70" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[221]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle70" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[222]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle70" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[223]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle71" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[224]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle71" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[225]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle71" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[226]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle72" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[227]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle72" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[228]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle72" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[229]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle73" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[230]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle73" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[231]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle73" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[232]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle74" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[233]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle74" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[234]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle74" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[235]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle75" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[236]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle75" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[237]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle75" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[238]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle76" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[239]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle76" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[240]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle76" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[241]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle77" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[242]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle77" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[243]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle77" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[244]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle78" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[245]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle78" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[246]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle78" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[247]);
                            obj.gameObject.SetActive(false);
                        }
                        if (obj.name == "AnimatedCircle79" && suspectScripts[0].notTalking && suspectInMiddle == 1)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[248]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle79" && suspectScripts[1].notTalking && suspectInMiddle == 2)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[249]);
                            obj.gameObject.SetActive(false);
                        }
                        else if (obj.name == "AnimatedCircle79" && suspectScripts[2].notTalking && suspectInMiddle == 3)
                        {
                            Debug.Log("Text gelmesi laz�m!");
                            InstantiateTexts(texts[250]);
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

    // Daha �nce �a�r�lan de�erleri saklamak i�in s�n�f d�zeyi bir HashSet
    private HashSet<(int, int)> usedNotepadHints = new HashSet<(int, int)>();

    private void FillNotepad(int notepadNumber, int hintNumber)
    {
        // E�er bu kombinasyon daha �nce kullan�ld�ysa fonksiyonu �al��t�rma.
        if (usedNotepadHints.Contains((notepadNumber, hintNumber)))
        {
            Debug.Log($"Notepad {notepadNumber}, Hint {hintNumber} zaten y�klendi!");
            return;
        }

        // E�er kullan�lmad�ysa ekleyip i�lemi yap.
        usedNotepadHints.Add((notepadNumber, hintNumber));
        notepadPages[notepadNumber].HintLoad(hintNumber);
        Instantiate(notepadPopUp);
        Debug.Log($"Notepad {notepadNumber}, Hint {hintNumber} y�klendi.");
    }



}