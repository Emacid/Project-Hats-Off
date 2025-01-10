using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public RectTransform character;
    public RectTransform background;
    public RectTransform credits;

    public RectTransform buttons;

    Vector3 targetIn;
    Vector3 targetOut;
    Vector3 targetScale;
    Vector3 creditsTargetIn;
    Vector3 creditsTargetOut;
    public float transitionTime;
    public float moveSpeed;
    public float scaleSpeed;
    public bool isZoomed;
    public bool isMoving;
    public bool onPressed;


    private void Start()
    {
        targetScale = new Vector3(12, 12, 1);
        targetIn = new Vector3(810, 200, 0);
        targetOut = new Vector3(810, 360, 0);
        creditsTargetIn = new Vector3(0, 0, 0);
        creditsTargetOut = new Vector3(0, 1000, 0);
    }

    private void Update()
    {    
        if (isZoomed)
        {
            ZoomIn();
            StartCoroutine(NextScene());
        }
    }

    public void StartGame1()
    {
        PlayerPrefs.SetInt("GameMode", 1); // 1: Kolay mod
        PlayerPrefs.Save();
        isZoomed = true;
    }

    public void StartGame2()
    {
        PlayerPrefs.SetInt("GameMode", 2); // 2: Zor mod
        PlayerPrefs.Save();
        isZoomed = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void ZoomIn()
    {
        character.localScale = Vector3.Lerp(character.localScale, targetScale, Time.deltaTime * scaleSpeed);
        background.localScale = Vector3.Lerp(background.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

   

    IEnumerator Movement(RectTransform transform,Vector3 target, float finishTime)
    {
        float lerpTime = 0;

        while (lerpTime < 1)
        {
            isMoving = true;
            lerpTime += Time.deltaTime * moveSpeed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, target, lerpTime);

            if (lerpTime >= finishTime)
            {
                isMoving = false;
                StopAllCoroutines(); 
                transform.localPosition = target;
                lerpTime = 0;
            }
            yield return null;
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(transitionTime);
        print("next");
        SceneManager.LoadScene("GameScene");    
    }

    public void Move(int buttonIndex)
    {
        if (isMoving) return;

        switch (buttonIndex)
        {
            case 0:
                if (!onPressed)
                {
                    onPressed = true;
                    StartCoroutine(Movement(buttons, targetIn, 0.1f));
                }
                else if (onPressed)
                {
                    onPressed = false;
                    StartCoroutine(Movement(buttons, targetOut, 0.1f));
                }
                break;
            case 1:
                if (!onPressed)
                {
                    onPressed = true;
                    StartCoroutine(Movement(credits, creditsTargetIn, 0.3f));
                }
                else if (onPressed)
                {
                    onPressed = false;
                    StartCoroutine(Movement(credits, creditsTargetOut, 0.3f));
                }
                break;

        }
        
        
    }

}
