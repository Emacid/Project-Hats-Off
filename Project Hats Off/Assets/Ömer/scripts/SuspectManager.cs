using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SuspectManager : MonoBehaviour
{

    public GameObject[] suspects;  // Þüpheli GameObject'leri
    public Transform middlePosition;
    public Transform rightPosition;
    public float moveSpeed = 0.5f;  // Hareket hýzý

    private GameObject currentSuspect;  // Þu an ortada olan þüpheli

    public bool isPressed;

    public static SuspectManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnButtonPressed(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0 when !isPressed:
                ShowSuspect(0);
                break;
            case 1 when !isPressed:
                ShowSuspect(1);
                break;
            case 2 when !isPressed:
                ShowSuspect(2);
                break;

        }

    }
    public void ShowSuspect(int suspectIndex)
    {
        if (currentSuspect != null)
        {
            StartCoroutine(MoveToPosition(currentSuspect, rightPosition.position));
        }

        currentSuspect = suspects[suspectIndex];
        StartCoroutine(MoveToPosition(currentSuspect, middlePosition.position));
    }

    private IEnumerator MoveToPosition(GameObject suspect, Vector3 targetPosition)
    {
        float lerpTime = 0f;
        Vector3 startingPosition = suspect.transform.position;
        while (lerpTime < 1f)
        {
            suspect.transform.position = Vector3.Lerp(startingPosition, targetPosition, lerpTime);
            lerpTime += Time.deltaTime * moveSpeed;
            isPressed = true;
            yield return null;
            suspect.transform.position = targetPosition;
            isPressed = false;
        }
    }
}
    

