using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGrav : MonoBehaviour
{

    public bool inPhotoZone = false;
    private Rigidbody2D rigidbody2D;
    public bool caseFile = false;
    public bool caseFileOnMiddle = false;
    public bool caseFileOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SquareSprite"))
        {
            inPhotoZone = true;
        }
        if (collision.CompareTag("MiddleScreen"))
        {
            caseFileOnMiddle = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SquareSprite"))
        {
            inPhotoZone = false;
        }
        if (other.CompareTag("MiddleScreen"))
        {
            caseFileOnMiddle = false;
        }
    }

    private void OnMouseUp()
    {
        if (inPhotoZone)
        {
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = Vector2.zero;
            Debug.Log("eþya gravity'si 0!");
        }

        if (!inPhotoZone && !caseFile)
        {
            rigidbody2D.gravityScale = 1f;
            Debug.Log("eþya gravity'si 1!");
        }
        if (!inPhotoZone && caseFile && caseFileOnMiddle && !caseFileOpen)
        {
            rigidbody2D.gravityScale = 1f;
            Debug.Log("eþya gravity'si 1!");
        }
    }

}