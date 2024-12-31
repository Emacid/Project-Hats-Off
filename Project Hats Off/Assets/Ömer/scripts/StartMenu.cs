using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public RectTransform character;
    public RectTransform background;

    public RectTransform buttons;

    Vector3 targetIn;
    Vector3 targetOut;
    Vector3 targetScale;
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
    }

    private void Update()
    {    
        if (isZoomed)
        {
            ZoomIn();
            StartCoroutine(NextScene());
        }
    }

    public void StartGame()
    {
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

    IEnumerator Movement(Vector3 target)
    {
        float lerpTime = 0;

        while (lerpTime < 1)
        {
            isMoving = true;
            lerpTime += Time.deltaTime * moveSpeed;

            buttons.localPosition = Vector3.Lerp(buttons.localPosition, target, lerpTime);

            if (lerpTime >= 1)
            {
                isMoving = false;
                StopAllCoroutines(); 
                buttons.localPosition = target;
                lerpTime = 0;
            }
            yield return null;

        }

    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(transitionTime);
        print("next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
    }

    public void Move()
    {
        if (isMoving) return;

        if (!onPressed)
        {
            StartCoroutine(Movement(targetIn));
            onPressed = true;
        }
        else if (onPressed)
        {
            StartCoroutine(Movement(targetOut));
            onPressed = false;
        }
    }

}
