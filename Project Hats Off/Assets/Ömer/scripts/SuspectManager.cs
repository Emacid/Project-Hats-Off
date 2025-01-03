using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SuspectManager : MonoBehaviour
{

    public GameObject[] suspects;  // Þüpheli GameObject'leri
    public Transform middlePosition;
    public Transform rightPosition;
    public float moveSpeed = 0.5f;  // Hareket hýzý

    private GameObject currentSuspect;  // Þu an ortada olan þüpheli

    public bool isPressed;
    public GameObject parentObject;

    public static SuspectManager instance;
    public ButtonManager buttonManager;
    public Asistant asistantScript;
    private SuspectOutline[] suspectOutlines;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        asistantScript = GameObject.Find("AsistantMechanic").GetComponent<Asistant>();

        suspectOutlines = new SuspectOutline[suspects.Length];
        for (int i = 0; i < suspects.Length; i++)
        {
            // Suspect GameObject'in içindeki SuspectOutline bileþenini al
            suspectOutlines[i] = suspects[i].GetComponent<SuspectOutline>();
            if (suspectOutlines[i] == null)
            {
                Debug.LogWarning($"Suspect {suspects[i].name} nesnesinde SuspectOutline bileþeni yok!");
            }
        }

        ShowSuspect(0);
    }
    public void OnButtonPressed(int buttonIndex)
    {
        if (!buttonManager.startedConversation) 
        { 
            foreach (var outline in suspectOutlines)
            {
                outline.CloseTheOutlineByClickingAgainn();
            }
        }
        else if (buttonManager.startedConversation) 
        {
            foreach (var outline in suspectOutlines)
            {
                outline.CloseTheOutlineByClickingAgainn();
                TriggerTextAnimScripts();
            }
        }
        switch (buttonIndex)
        {
            case 0 when !isPressed:
                if (buttonManager.startedConversation)
                {
                    asistantScript.SpawnAsistantText(0);
                    break;
                }
                else
                ShowSuspect(0);
                break;
            case 1 when !isPressed:
                if (buttonManager.startedConversation)
                {
                    asistantScript.SpawnAsistantText(0);
                    break;
                }
                else
                    ShowSuspect(1);
                break;
            case 2 when !isPressed:
                if (buttonManager.startedConversation)
                {
                    asistantScript.SpawnAsistantText(0);
                    break;
                }
                else
                    ShowSuspect(2);
                break;

        }

    }
    public void ShowSuspect(int suspectIndex)
    {
        if (currentSuspect != null)
        {
            StartCoroutine(MoveToPosition(currentSuspect, rightPosition.position));
        }

        currentSuspect = suspects[suspectIndex];
        StartCoroutine(MoveToPosition(currentSuspect, middlePosition.position));
        buttonManager.suspectInMiddle = suspectIndex+1;
    }

    private IEnumerator MoveToPosition(GameObject suspect, Vector3 targetPosition)
    {
        float lerpTime = 0f;
        Vector3 startingPosition = suspect.transform.position;
        while (lerpTime < 1f)
        {
            suspect.transform.position = Vector3.Lerp(startingPosition, targetPosition, lerpTime);
            lerpTime += Time.deltaTime * moveSpeed;
            isPressed = true;
            yield return null;
            suspect.transform.position = targetPosition;
            isPressed = false;
        }
    }

    public void TriggerTextAnimScripts()
    {
        // Parent objenin altýndaki tüm child objeleri kontrol et.
        TextAnim[] textAnimScripts = parentObject.GetComponentsInChildren<TextAnim>();

        // Her TextAnim scripti için TextsVanish fonksiyonunu çaðýr.
        foreach (TextAnim textAnim in textAnimScripts)
        {
            textAnim.TextsVanish();
        }
    }

}


