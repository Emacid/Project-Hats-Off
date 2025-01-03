using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Time.deltaTime yerine Time.unscaledDeltaTime kullan
    float letterInterval = 0.1f; // Harfler aras�nda ge�en s�re
    float timer = 0f;

    void Start()
    {
        Time.timeScale = 1f; // Zaman �l�e�ini varsay�lan olarak ayarla
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime; // Zamana ba�l� olmayan �l��m
        if (timer >= letterInterval)
        {
            // Bir sonraki harfi yaz
            timer = 0f;
        }
    }
   
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
