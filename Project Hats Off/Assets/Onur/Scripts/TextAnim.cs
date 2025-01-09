using System.Collections;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    private Animator animator;
    public Animator VanishRightAnimator;
    public Animator VanishLeftAnimator;
    private Asistant asistantMechanic;
    private Animator[] suspectAnimators;
    private Debugging debuggingScript;

    public GameObject[] texts;
    [SerializeField] private int textBoxNumber = 6; // Unity'den ayarlanabilir maksimum s�n�r
    private int currentTriggerIndex = -1; // Hangi trigger'da oldu�umuzu tutar

    public ButtonManager ButtonManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        ButtonManager = GameObject.Find("Question Button Manager").GetComponent<ButtonManager>();
        asistantMechanic = GameObject.Find("AsistantMechanic").GetComponent<Asistant>();
        suspectAnimators = new Animator[9];
        suspectAnimators[0] = GameObject.Find("Suspect_1").GetComponent<Animator>();
        suspectAnimators[1] = GameObject.Find("Suspect Gray Outlined One").GetComponent<Animator>();
        suspectAnimators[2] = GameObject.Find("Suspect Red Outlined One").GetComponent<Animator>();
        suspectAnimators[3] = GameObject.Find("Suspect_1 (1)").GetComponent<Animator>();
        suspectAnimators[4] = GameObject.Find("Suspect Gray Outlined Two").GetComponent<Animator>();
        suspectAnimators[5] = GameObject.Find("Suspect Red Outlined Two").GetComponent<Animator>();
        suspectAnimators[6] = GameObject.Find("Suspect_1 (2)").GetComponent<Animator>();
        suspectAnimators[7] = GameObject.Find("Suspect Gray Outlined Three").GetComponent<Animator>();
        suspectAnimators[8] = GameObject.Find("Suspect Red Outlined Three").GetComponent<Animator>();
        debuggingScript = GameObject.Find("Debug()").GetComponent<Debugging>();
    }

    // Butonla �a�r�lacak fonksiyon
    public void TriggerNextAnimation()
    {
        Debug.Log("TriggerNextAnimation ba�lad�");

        if (!asistantMechanic.isClickedOnAsistant)
        {
            // E�er mevcut tetikleme say�s� belirlenen s�n�r� a�arsa hi�bir �ey yapma
            if (currentTriggerIndex >= textBoxNumber - 1)
            {
                Debug.Log("Maksimum s�n�ra ula��ld�.");
                return; // Daha fazla i�lem yap�lmaz
            }

            // Sonraki trigger'� �al��t�r
            currentTriggerIndex++; // Index'i art�r
            if (currentTriggerIndex < textBoxNumber - 1)
            {
                // Debug.Log, TalkAnim ve Animator tetikleme kodu burada �al��acak
                Debug.Log($"Trigger �al��t�r�l�yor: New Trigger {currentTriggerIndex}");
                StartCoroutine(TalkAnim());  // Animasyon tetiklemeleri
                animator.SetTrigger($"New Trigger {currentTriggerIndex}");
                StartCoroutine(WaitForDelay(currentTriggerIndex));
            }
            else if (currentTriggerIndex == textBoxNumber - 1)
            {
                TextsVanish();
            }
        }
        else if (asistantMechanic.isClickedOnAsistant)
        {
            //asistantMechanic.SpawnAsistantText(); // E�er gerekli de�ilse buray� atlayabilirsiniz
        }
    }

    private IEnumerator WaitForDelay(int triggerNumber)
    {
        Debug.Log($"WaitForDelay ba�lad��� trigger: {triggerNumber}");
        yield return new WaitForSeconds(0.5f);  // Gecikme
        if (triggerNumber < texts.Length)
        {
            texts[triggerNumber].SetActive(true);  // Text'i g�ster
        }
    }

    private IEnumerator DestroyTheText(float destroytime)
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(transform.parent.gameObject);  // Nesneyi yok et
    }

    public void TextsVanish()
    {
        // E�er belirtilen say�ya ula��ld�ysa animasyonlar� tetikle
        VanishRightAnimator.SetTrigger("VanishRightTrigger");
        VanishLeftAnimator.SetTrigger("VanishLeftTrigger");
        Debug.Log("Animasyon tetiklendi: VanishRightTrigger ve VanishLeftTrigger");
        ButtonManager.startedConversation = false;
        ButtonManager.CanTriggerTalkAgain = true;
        ButtonManager.clickedOnSuspect = false;
        ButtonManager.canCloseTheRed = true;
        StartCoroutine(DestroyTheText(3.5f));
    }

    private IEnumerator TalkAnim()
    {
        // Debug mesaj� ile hangi animasyonlar�n tetiklendi�ini kontrol edebiliriz
        yield return new WaitForSeconds(0.6f);
        suspectAnimators[0].SetTrigger("TalkTrigger");
        suspectAnimators[1].SetTrigger("TalkTrigger");
        suspectAnimators[2].SetTrigger("TalkTrigger");
        suspectAnimators[3].SetTrigger("TalkTrigger");
        suspectAnimators[4].SetTrigger("TalkTrigger");
        suspectAnimators[5].SetTrigger("TalkTrigger");
        suspectAnimators[6].SetTrigger("TalkTrigger");
        suspectAnimators[7].SetTrigger("TalkTrigger");
        suspectAnimators[8].SetTrigger("TalkTrigger");
    }
}
