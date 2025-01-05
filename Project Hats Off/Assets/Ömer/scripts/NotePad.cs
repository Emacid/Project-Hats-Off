using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotePad : MonoBehaviour
{
    public TextAsset hintSentences;
    public List<GameObject> hintObjects = new List<GameObject>();
    public string[] sentences;
    private KeyCode key = KeyCode.T;
    private KeyCode key1 = KeyCode.Y;
    private KeyCode key2 = KeyCode.U;
    private KeyCode key3 = KeyCode.I;
    private KeyCode key4 = KeyCode.O;
    private KeyCode key5 = KeyCode.P;
    int hintIndex;


    private void Start()
    {
        hintObjects.Clear();
        hintIndex = 0;
        foreach (Transform child in transform)
        {
            GameObject childObject = child.gameObject;
            TextMeshProUGUI hintsText = child.GetComponent<TextMeshProUGUI>();
            hintObjects.Add(childObject);
        }
        for (int i = 0; i < hintObjects.Count; i++)
        {
            hintObjects[i].gameObject.SetActive(false);
        }

        sentences = hintSentences.text.Split('\n');

    }


    private void Update()
    {/*
        if(Input.GetKeyDown(key))
        {
            HintLoad(0);
        }
        if (Input.GetKeyDown(key1))
        {
            HintLoad(1);
        }
        if (Input.GetKeyDown(key2))
        {
            HintLoad(2);
        }
        if (Input.GetKeyDown(key3))
        {
            HintLoad(3);
        }
        if (Input.GetKeyDown(key4))
        {
            HintLoad(4);
        }
        if (Input.GetKeyDown(key5))
        {
            HintLoad(5);
        }
        */
    }

    public void HintLoad(int index)
    {
        if(hintIndex < hintObjects.Count)
        {
            GameObject hint = hintObjects[hintIndex];
            
            if (hint != null)
            {   
                hint.SetActive(true);
                TextMeshPro hintsText = hint.GetComponentInChildren<TextMeshPro>();

                if(hintsText != null)
                {
                    hintsText.text = sentences[index];
                }             
                hintIndex++;
            }
        }
        else if (hintIndex > hintObjects.Count)
        {
            hintIndex = 0;
        }

    }



}
