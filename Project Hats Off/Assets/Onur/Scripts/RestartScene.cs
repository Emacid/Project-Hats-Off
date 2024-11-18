using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void Update()
    {
        // R tuþuna basýlýp basýlmadýðýný kontrol eder
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Mevcut sahneyi yeniden yükler
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
