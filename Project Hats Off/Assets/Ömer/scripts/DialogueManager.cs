using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextAsset[] textAssets;
    private TextAsset[,] textFiles = new TextAsset[3, 4];
    public string[] dialogueLines;

    public enum Suspect { Suspect1, Suspect2, Suspect3 }
    public Suspect currentSuspect;
    public enum Question {  Question1, Question2, Question3, Question4 }
    public Question currentQuestion;


    public static DialogueManager instance;

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
        LoadDialogue();  
    }

    public void SetQuestion(int questionNumber)
    {
        currentQuestion = (Question)questionNumber;
    }

    private void LoadDialogue()
    {
        int suspectIndex = (int)currentSuspect;
        int questionIndex = (int)currentQuestion;

        if (textFiles[suspectIndex, questionIndex] != null)
        {
            dialogueLines = textFiles[suspectIndex, questionIndex].text.Split('\n');
        };
        }
     
}

