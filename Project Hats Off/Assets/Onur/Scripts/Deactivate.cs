using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableAfterDelay(0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
