using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    new public Vector2 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
