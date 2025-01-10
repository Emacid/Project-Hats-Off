using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public GameObject targetObject; // Enable/disable yap�lacak obje

    void Start()
    {
        int gameMode = PlayerPrefs.GetInt("GameMode", 1); // Varsay�lan: Kolay mod
        if (gameMode == 1)
        {
            targetObject.SetActive(true); // Kolay mod: Objeyi etkinle�tir
        }
        else if (gameMode == 2)
        {
            targetObject.SetActive(false); // Zor mod: Objeyi devre d��� b�rak
        }
    }
}
