using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextAsset[] textAssets;
    private TextAsset[,] textFiles = new TextAsset[3, 4];
    public string[] dialogueLines;
    public DialogueMovement dialogueMovement;

    public float checkpointWaitTime = 1.0f; 

    public enum Suspect { Suspect1, Suspect2, Suspect3 }
    public Suspect currentSuspect;
    public enum Question {  Question1, Question2, Question3, Question4 }
    public Question currentQuestion;
    public enum Evidence { obj1, obj2, obj3, obj4 }
    public Evidence currentEvidence;

    public static DialogueManager instance;

    private int dialogueIndex = 0;

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
        InitializeTextFiles();
    }

    private void Update()
    {
        ChooseEvidence();
    }

    void InitializeTextFiles()
    {
        int index = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                textFiles[i, j] = textAssets[index];
                index++;
            }
        }

    }


    public void SetSuspect(int suspectNumber)
    {
        currentSuspect = (Suspect)suspectNumber;
      //  DialogueLoader.instance.DialogueUpdate();
    }

    public void SetQuestion(int questionNumber)
    {
        currentQuestion = (Question)questionNumber;
    }

    public void SetEvidence(int objectNumber)
    {
        currentEvidence = (Evidence)objectNumber;

        switch (currentEvidence)
        {
            case Evidence.obj1:
                SetQuestion(0);
                break;
            case Evidence.obj2:
                SetQuestion(1);
                break;
            case Evidence.obj3:
                SetQuestion(2);
                break;
            case Evidence.obj4:
                SetQuestion(3);
                break;
        }
    }

    void ChooseEvidence()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetEvidence(0);
            LoadDialogue(); //diyaloglarý belirliyor 
            ImageTextManager.instance.DialogueWrite(); //diyaloglarý image image ayýrýp yazýyor
            StartCoroutine(MovementCoroutine()); //diyaloglarýn aþaðý inmesini saðlýyor.

        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SetEvidence(1);
            LoadDialogue();
            ImageTextManager.instance.DialogueWrite();
            StartCoroutine(MovementCoroutine()); 
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SetEvidence(2);
            LoadDialogue();
            ImageTextManager.instance.DialogueWrite();
            StartCoroutine(MovementCoroutine());

        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SetEvidence(3);
            LoadDialogue();
            ImageTextManager.instance.DialogueWrite();
            StartCoroutine(MovementCoroutine());
        }
    }

    private IEnumerator MovementCoroutine()
    {
        yield return StartCoroutine(StartMovement());
     
    }

    private IEnumerator StartMovement()
    {
        while (dialogueIndex < dialogueLines.Length)
        {

            while (!dialogueMovement.Movement())
            {
                yield return null; 
                
            }

            yield return new WaitForSeconds(checkpointWaitTime);
            dialogueIndex++;
        }
        dialogueIndex = 0;

        print("diyalog bitti");
        dialogueMovement.FinishMove();
    }

    public void LoadDialogue()
    {
        int suspectIndex = (int)currentSuspect;
        int questionIndex = (int)currentQuestion;

        if (textFiles[suspectIndex, questionIndex] != null)
        {
            dialogueLines = textFiles[suspectIndex, questionIndex].text.Split('\n');
            
        };
        }
     
}

