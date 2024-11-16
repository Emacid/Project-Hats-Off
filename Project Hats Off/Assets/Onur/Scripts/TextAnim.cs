using System.Collections;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    private Animator animator;
    public Animator VanishRightAnimator;
    public Animator VanishLeftAnimator;

    public GameObject[] texts;
    [SerializeField] private int textBoxNumber = 6; // Unity'den ayarlanabilir maksimum s�n�r
    private int currentTriggerIndex = -1; // Hangi trigger'da oldu�umuzu tutar

    public ButtonManager ButtonManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        ButtonManager = GameObject.Find("Question Button Manager").GetComponent<ButtonManager>();
    }

    // Butonla �a�r�lacak fonksiyon
    public void TriggerNextAnimation()
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
            animator.SetTrigger($"New Trigger {currentTriggerIndex}");
            StartCoroutine(WaitForDelay(currentTriggerIndex));
        }
        else if (currentTriggerIndex == textBoxNumber - 1)
        {
            // E�er belirtilen say�ya ula��ld�ysa animasyonlar� tetikle
            VanishRightAnimator.SetTrigger("VanishRightTrigger");
            VanishLeftAnimator.SetTrigger("VanishLeftTrigger");
            Debug.Log("Animasyon tetiklendi: VanishRightTrigger ve VanishLeftTrigger");
            ButtonManager.startedConversation = false;
            ButtonManager.CanTriggerTalkAgain = true;
            ButtonManager.clickedOnSuspect = false;
        }
    }

    private IEnumerator WaitForDelay(int triggerNumber)
    {
        yield return new WaitForSeconds(0.5f);
        if (triggerNumber < texts.Length)
        {
            texts[triggerNumber].SetActive(true);
        }
    }
}
