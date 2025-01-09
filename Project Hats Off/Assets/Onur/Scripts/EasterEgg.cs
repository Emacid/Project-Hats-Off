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
        // Ba�lang��ta video oynat�c�lar� devre d��� b�rak
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

            // E�er s�re 10 saniyeyi a�arsa saya� s�f�rlan�r
            if (timer > 10f)
            {
                ResetClickCounter();
            }
        }
    }

    private void OnMouseDown()
    {
        // T�klama BoxCollider2D �zerine yap�lm��sa
        if (!isTimerRunning)
        {
            isTimerRunning = true; // Zamanlay�c�y� ba�lat
        }

        clickCount++;

        // 10 t�klama say�s�na ula��l�rsa video oynat�c�lar� etkinle�tir
        if (clickCount >= 10)
        {
            if (videoplayer != null)
                videoplayer.SetActive(true);

            if (videoplayer2 != null)
                videoplayer2.SetActive(true);
            radio.isMuted = false;
            radio.OnDoubleClick();
            ResetClickCounter(); // Saya� s�f�rlan�r
        }
    }

    private void ResetClickCounter()
    {
        clickCount = 0;
        timer = 0f;
        isTimerRunning = false;
    }
}
