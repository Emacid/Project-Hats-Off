using System.Collections;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float startDelay = 1f;
    public float textSpeed = 0.07f; // Bu deðeri gerektiðinde ayarlayabilirsiniz
    private int index;
    public bool endText;
    private float textDelay;


    public static TextWriter instance;

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
        textComponent.text = string.Empty;
        endText = false;
    }

    public void StartDialogue()
    {
        index = 0;
        StopAllCoroutines();  // Diðer coroutineleri durdur
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty; // Diyalog baþýnda temizleyin
        foreach (char c in DialogueManager.instance.dialogueLines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < DialogueManager.instance.dialogueLines.Length - 1)
        {
            index++;
            StopAllCoroutines();  // Yeni satýra geçerken önceki coroutineleri durdur
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = ""; // Son diyalogdan sonra boþ býrak
        }
    }

    private void FixedUpdate()
    {
        textDelay = Time.fixedDeltaTime;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.text == DialogueManager.instance.dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = DialogueManager.instance.dialogueLines[index];
            }
        }
    }
}
