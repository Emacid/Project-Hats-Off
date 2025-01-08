using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public DraggableEvidence[] draggableEvidiences;
    public DraggableObject[] draggableObjects;

    void Update()
    {
        // P tu�una bas�ld���nda pause/continue i�lemi yap�l�r
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            // Oyun devam etsin
            Time.timeScale = 1f;
            isPaused = false;

            // Draggable nesnelerin scriptlerini tekrar aktif hale getir
            SetDraggableScriptsEnabled(true);
        }
        else
        {
            // Oyun duraklat�ls�n
            Time.timeScale = 0f;
            isPaused = true;

            // Draggable nesnelerin scriptlerini devre d��� b�rak
            SetDraggableScriptsEnabled(false);
        }
    }

    // Draggable nesnelerin scriptlerinin aktifli�ini ayarlayan fonksiyon
    void SetDraggableScriptsEnabled(bool isEnabled)
    {
        foreach (var draggableEvidence in draggableEvidiences)
        {
            var draggableScript = draggableEvidence.GetComponent<DraggableEvidence>();
            if (draggableScript != null)
            {
                draggableScript.enabled = isEnabled;
            }
        }

        foreach (var draggableObject in draggableObjects)
        {
            var draggableScript = draggableObject.GetComponent<DraggableObject>();
            if (draggableScript != null)
            {
                draggableScript.enabled = isEnabled;
            }
        }
    }
}
