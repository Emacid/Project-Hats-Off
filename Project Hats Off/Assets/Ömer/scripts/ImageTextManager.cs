using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class ImageTextManager : MonoBehaviour
{
    public List<GameObject> parentGameObjects;

    public static ImageTextManager instance;

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

    public void DialogueWrite()
    {
        int dialogueIndex = 0;
        

        foreach (GameObject parent in parentGameObjects)
        {
            Image[] images = parent.GetComponentsInChildren<Image>();

            foreach (Image img in images)
            {
                if (dialogueIndex < DialogueManager.instance.dialogueLines.Length)
                {
                    TextMeshProUGUI imageText = img.GetComponentInChildren<TextMeshProUGUI>();
                    if (imageText != null)
                    {

                        imageText.text = DialogueManager.instance.dialogueLines[dialogueIndex];
                        dialogueIndex++; 
                    }
                    else
                    {
                        Debug.LogWarning("Image'in içinde Text bileþeni bulunamadý: " + img.name);
                    }
                }
                else
                {
                    Debug.LogWarning("Cümle dizisinde yeterli eleman yok!");
                }
            }
        }
    }
}
