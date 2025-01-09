using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuslumGurses : MonoBehaviour
{
    public GameObject videoplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || (Input.GetKeyUp(KeyCode.Space))) 
            {
            Destroy(videoplayer);
            Destroy(gameObject);
        }
    }
}
