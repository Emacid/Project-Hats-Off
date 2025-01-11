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
    private AudioSource[] gibberishTalksCharlie;
    private AudioSource[] gibberishTalksAmy;
    private AudioSource[] gibberishTalksDavid;
    [SerializeField] private int textBoxNumber = 6; // Unity'den ayarlanabilir maksimum sýnýr
    private int currentTriggerIndex = -1; // Hangi trigger'da olduðumuzu tutar

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
        gibberishTalksCharlie = new AudioSource[12];
        gibberishTalksCharlie[0] = GameObject.Find("Sfx1").GetComponent<AudioSource>();
        gibberishTalksCharlie[1] = GameObject.Find("Sfx1 (1)").GetComponent<AudioSource>();
        gibberishTalksCharlie[2] = GameObject.Find("Sfx1 (2)").GetComponent<AudioSource>();
        gibberishTalksCharlie[3] = GameObject.Find("Sfx1 (3)").GetComponent<AudioSource>();
        gibberishTalksCharlie[4] = GameObject.Find("Sfx1 (4)").GetComponent<AudioSource>();
        gibberishTalksCharlie[5] = GameObject.Find("Sfx1 (5)").GetComponent<AudioSource>();
        gibberishTalksCharlie[6] = GameObject.Find("Sfx1 (6)").GetComponent<AudioSource>();
        gibberishTalksCharlie[7] = GameObject.Find("Sfx1 (7)").GetComponent<AudioSource>();
        gibberishTalksCharlie[8] = GameObject.Find("Sfx1 (8)").GetComponent<AudioSource>();
        gibberishTalksCharlie[9] = GameObject.Find("Sfx1 (9)").GetComponent<AudioSource>();
        gibberishTalksCharlie[10] = GameObject.Find("Sfx1 (10)").GetComponent<AudioSource>();
        gibberishTalksCharlie[11] = GameObject.Find("Sfx1 (11)").GetComponent<AudioSource>();
        gibberishTalksAmy = new AudioSource[12];
        gibberishTalksAmy[0] = GameObject.Find("Sfx2").GetComponent<AudioSource>();
        gibberishTalksAmy[1] = GameObject.Find("Sfx2 (1)").GetComponent<AudioSource>();
        gibberishTalksAmy[2] = GameObject.Find("Sfx2 (2)").GetComponent<AudioSource>();
        gibberishTalksAmy[3] = GameObject.Find("Sfx2 (3)").GetComponent<AudioSource>();
        gibberishTalksAmy[4] = GameObject.Find("Sfx2 (4)").GetComponent<AudioSource>();
        gibberishTalksAmy[5] = GameObject.Find("Sfx2 (5)").GetComponent<AudioSource>();
        gibberishTalksAmy[6] = GameObject.Find("Sfx2 (6)").GetComponent<AudioSource>();
        gibberishTalksAmy[7] = GameObject.Find("Sfx2 (7)").GetComponent<AudioSource>();
        gibberishTalksAmy[8] = GameObject.Find("Sfx2 (8)").GetComponent<AudioSource>();
        gibberishTalksAmy[9] = GameObject.Find("Sfx2 (9)").GetComponent<AudioSource>();
        gibberishTalksAmy[10] = GameObject.Find("Sfx2 (10)").GetComponent<AudioSource>();
        gibberishTalksAmy[11] = GameObject.Find("Sfx2 (11)").GetComponent<AudioSource>();
        gibberishTalksDavid = new AudioSource[10];
        gibberishTalksDavid[0] = GameObject.Find("Sfx3").GetComponent<AudioSource>();
        gibberishTalksDavid[1] = GameObject.Find("Sfx3 (1)").GetComponent<AudioSource>();
        gibberishTalksDavid[2] = GameObject.Find("Sfx3 (2)").GetComponent<AudioSource>();
        gibberishTalksDavid[3] = GameObject.Find("Sfx3 (3)").GetComponent<AudioSource>();
        gibberishTalksDavid[4] = GameObject.Find("Sfx3 (4)").GetComponent<AudioSource>();
        gibberishTalksDavid[5] = GameObject.Find("Sfx3 (5)").GetComponent<AudioSource>();
        gibberishTalksDavid[6] = GameObject.Find("Sfx3 (6)").GetComponent<AudioSource>();
        gibberishTalksDavid[7] = GameObject.Find("Sfx3 (7)").GetComponent<AudioSource>();
        gibberishTalksDavid[8] = GameObject.Find("Sfx3 (8)").GetComponent<AudioSource>();
        gibberishTalksDavid[9] = GameObject.Find("Sfx3 (9)").GetComponent<AudioSource>();

    }

    // Butonla çaðrýlacak fonksiyon
    public void TriggerNextAnimation()
    {
        Debug.Log("TriggerNextAnimation baþladý");

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
                // Debug.Log, TalkAnim ve Animator tetikleme kodu burada çalýþacak
                Debug.Log($"Trigger çalýþtýrýlýyor: New Trigger {currentTriggerIndex}");
                //StartCoroutine(TalkAnim());  // Animasyon tetiklemeleri
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
            //asistantMechanic.SpawnAsistantText(); // Eðer gerekli deðilse burayý atlayabilirsiniz
        }
    }

    private IEnumerator WaitForDelay(int triggerNumber)
    {
        Debug.Log($"WaitForDelay baþladýðý trigger: {triggerNumber}");
        yield return new WaitForSeconds(0.5f);  // Gecikme
        if (triggerNumber < texts.Length)
        {
            Transform parent = texts[triggerNumber].transform.parent?.parent?.parent;
            if (parent != null && parent.name == "Right")
            {
                StartCoroutine(TalkAnim());
                if(ButtonManager.suspectInMiddle == 1) 
                {
                    playRandomSoundCharlie();
                }
                else if (ButtonManager.suspectInMiddle == 2)
                {
                    playRandomSoundAmy();
                }
                else if (ButtonManager.suspectInMiddle == 3)
                {
                    playRandomSoundDavid();
                }
            }
            texts[triggerNumber].SetActive(true);  // Text'i göster
        }
    }

    private IEnumerator DestroyTheText(float destroytime)
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(transform.parent.gameObject);  // Nesneyi yok et
    }

    public void TextsVanish()
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

    private IEnumerator TalkAnim()
    {
        // Debug mesajý ile hangi animasyonlarýn tetiklendiðini kontrol edebiliriz
        yield return new WaitForSeconds(0.0f);
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

    private void playRandomSoundCharlie() 
    {
        gibberishTalksCharlie[Random.Range(0,12)].Play();
    }

    private void playRandomSoundAmy()
    {
        gibberishTalksAmy[Random.Range(0, 12)].Play();
    }

    private void playRandomSoundDavid()
    {
        gibberishTalksDavid[Random.Range(0, 10)].Play();
    }

}
