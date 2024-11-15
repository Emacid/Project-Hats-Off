using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public GameObject text01;
    public Transform allTextsParent; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F10))
        {
            text01.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.F11))
        {
            
            Instantiate(text01, allTextsParent);
        }
    }
}
