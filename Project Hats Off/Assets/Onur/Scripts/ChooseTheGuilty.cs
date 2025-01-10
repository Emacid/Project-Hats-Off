using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTheGuilty : MonoBehaviour
{
    public GameObject fadeOutBlack;
    public int suspectNumber = 1;
    public int RightSuspect = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseTheKiller()
    {
        if (suspectNumber == RightSuspect)
        {
            StartCoroutine(RightKiller());
        }
        else if (suspectNumber != RightSuspect)
        {
            StartCoroutine(WrongKiller());
        }

    }
    public IEnumerator WrongKiller()
    {
        fadeOutBlack.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("WrongKillerMidterm");
    }

    public IEnumerator RightKiller()
    {
        fadeOutBlack.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("RightKillerMidterm");
    }

    public void WrongKillerr() 
    {
        StartCoroutine(WrongKiller());
    }

    public void RightKillerr()
    {
        StartCoroutine(RightKiller());
    }
}