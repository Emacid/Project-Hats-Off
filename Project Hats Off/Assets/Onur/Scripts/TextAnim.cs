using System.Collections;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    private Animator animator;
    public Animator VanishRightAnimator;
    public Animator VanishLeftAnimator;
    private Asistant asistantMechanic;

    public GameObject[] texts;
    [SerializeField] private int textBoxNumber = 6; // Unity'den ayarlanabilir maksimum sýnýr
    private int currentTriggerIndex = -1; // Hangi trigger'da olduðumuzu tutar

    public ButtonManager ButtonManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        ButtonManager = GameObject.Find("Question Button Manager").GetComponent<ButtonManager>();
        asistantMechanic = GameObject.Find("AsistantMechanic").GetComponent<Asistant>();
    }

    // Butonla çaðrýlacak fonksiyon
    public void TriggerNextAnimation()
    {

        if (!asistantMechanic.isClickedOnAsistant) 
        {
            // Eðer mevcut tetikleme sayýsý belirlenen sýnýrý aþarsa hiçbir þey yapma
            if (currentTriggerIndex >= textBoxNumber - 1)
            {
                Debug.Log("Maksimum sýnýra ulaþýldý.");
                return; // Daha fazla iþlem yapýlmaz
            }

            // Sonraki trigger'ý çalýþtýr
            currentTriggerIndex++; // Index'i artýr
            if (currentTriggerIndex < textBoxNumber - 1)
            {
                animator.SetTrigger($"New Trigger {currentTriggerIndex}");
                StartCoroutine(WaitForDelay(currentTriggerIndex));
            }
            else if (currentTriggerIndex == textBoxNumber - 1)
            {
                // Eðer belirtilen sayýya ulaþýldýysa animasyonlarý tetikle
                VanishRightAnimator.SetTrigger("VanishRightTrigger");
                VanishLeftAnimator.SetTrigger("VanishLeftTrigger");
                Debug.Log("Animasyon tetiklendi: VanishRightTrigger ve VanishLeftTrigger");
                ButtonManager.startedConversation = false;
                ButtonManager.CanTriggerTalkAgain = true;
                ButtonManager.clickedOnSuspect = false;
                ButtonManager.canCloseTheRed = true;
                StartCoroutine(DestroyTheText(3.5f));
            }
            }
        else if(asistantMechanic.isClickedOnAsistant)
        {
            //asistantMechanic.SpawnAsistantText();
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

    private IEnumerator DestroyTheText(float destroytime)
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(transform.parent.gameObject);
    }
}
