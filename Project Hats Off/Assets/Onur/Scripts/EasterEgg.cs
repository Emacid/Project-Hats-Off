using System.Collections;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public GameObject videoplayer;
    public GameObject videoplayer2;
    public Radio radio;

    private int clickCount = 0;
    private float timer = 0f;
    private bool isTimerRunning = false;

    void Start()
    {
        // Baþlangýçta video oynatýcýlarý devre dýþý býrak
        if (videoplayer != null)
            videoplayer.SetActive(false);

        if (videoplayer2 != null)
            videoplayer2.SetActive(false);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;

            // Eðer süre 10 saniyeyi aþarsa sayaç sýfýrlanýr
            if (timer > 10f)
            {
                ResetClickCounter();
            }
        }
    }

    private void OnMouseDown()
    {
        // Týklama BoxCollider2D üzerine yapýlmýþsa
        if (!isTimerRunning)
        {
            isTimerRunning = true; // Zamanlayýcýyý baþlat
        }

        clickCount++;

        // 10 týklama sayýsýna ulaþýlýrsa video oynatýcýlarý etkinleþtir
        if (clickCount >= 10)
        {
            if (videoplayer != null)
                videoplayer.SetActive(true);

            if (videoplayer2 != null)
                videoplayer2.SetActive(true);
            radio.isMuted = false;
            radio.OnDoubleClick();
            ResetClickCounter(); // Sayaç sýfýrlanýr
        }
    }

    private void ResetClickCounter()
    {
        clickCount = 0;
        timer = 0f;
        isTimerRunning = false;
    }
}
