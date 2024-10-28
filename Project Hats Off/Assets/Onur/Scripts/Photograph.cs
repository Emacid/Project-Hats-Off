using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photograph : MonoBehaviour
{
    
    public bool inPhotoZone = false;
    private SwapPos swapPosScript;
    private Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        swapPosScript = GetComponent<SwapPos>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PhotoZone"))
        {
            inPhotoZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PhotoZone"))
        {
            inPhotoZone = false;
        }
    }

    private void OnMouseUp()
    {
        if(inPhotoZone && swapPosScript.folderUp) 
        {
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = Vector2.zero;
            Debug.Log("foto gravity'si 0!");
        }

        if (!inPhotoZone && swapPosScript.folderUp)
        {
            rigidbody2D.gravityScale = 1f;
            Debug.Log("foto gravity'si 1!");
        }
    }

}
