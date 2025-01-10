using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public GameObject targetObject; // Enable/disable yapýlacak obje

    void Start()
    {
        int gameMode = PlayerPrefs.GetInt("GameMode", 1); // Varsayýlan: Kolay mod
        if (gameMode == 1)
        {
            targetObject.SetActive(true); // Kolay mod: Objeyi etkinleþtir
        }
        else if (gameMode == 2)
        {
            targetObject.SetActive(false); // Zor mod: Objeyi devre dýþý býrak
        }
    }
}
