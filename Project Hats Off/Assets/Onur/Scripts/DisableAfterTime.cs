using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public GameObject page01, page02, page03;
    void Start()
    {
        Invoke("DeactivateObject", 0.5f);
        //StartCoroutine(DeactivateObject(1.5f));
    }

    
    void DeactivateObject()
    {
        page02.SetActive(false);
        page03.SetActive(true);
        StartCoroutine(DeactivateObject(0.1f));
    }
 
    private IEnumerator DeactivateObject(float delay) 
    {
        yield return new WaitForSeconds(delay);
        page03.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        page01.SetActive(true);
    }
}
