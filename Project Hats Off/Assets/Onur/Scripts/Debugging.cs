using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public Animator[] animators;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10)) 
        {
            callTriggers();
        }

        
    }
    private void callTriggers()
    {
        animators[0].SetTrigger("TalkTrigger");
        animators[1].SetTrigger("TalkTrigger");
    }
}
