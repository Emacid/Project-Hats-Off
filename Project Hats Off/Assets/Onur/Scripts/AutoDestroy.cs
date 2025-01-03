using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Time.deltaTime yerine Time.unscaledDeltaTime kullan
    float letterInterval = 0.1f; // Harfler arasýnda geçen süre
    float timer = 0f;

    void Start()
    {
        Time.timeScale = 1f; // Zaman ölçeðini varsayýlan olarak ayarla
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime; // Zamana baðlý olmayan ölçüm
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
