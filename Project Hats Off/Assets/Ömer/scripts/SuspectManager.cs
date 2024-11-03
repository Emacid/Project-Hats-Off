using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SuspectManager : MonoBehaviour
{

    public Suspect suspect;
    public Suspect2 suspect2;
    public Suspect3 suspect3;
    
    public bool isMoving1;
    public bool isMoving2;
    public bool isMoving3;

  
    public static SuspectManager instance;


    private void Awake()
    {
        if(instance != null && instance != this)
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
            case 0:
                suspect.MovementSuspect();
               
                break;
            case 1:
                suspect2.MovementSuspect();
                
                break;
            case 2:
                suspect3.MovementSuspect();
                
                break;

        }

    }
    
}
