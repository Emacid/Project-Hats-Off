using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        // R tu�una bas�l�p bas�lmad���n� kontrol eder
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Mevcut sahneyi yeniden y�kler
            SceneManager.LoadScene(1);
        }

        // R tu�una bas�l�p bas�lmad���n� kontrol eder
        if (Input.GetKeyDown(KeyCode.F8))
        {
            // Mevcut sahneyi yeniden y�kler
            SceneManager.LoadScene(4);
        }
    }
}
