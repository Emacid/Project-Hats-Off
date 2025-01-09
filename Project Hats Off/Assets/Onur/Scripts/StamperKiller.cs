using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StamperKiller : MonoBehaviour
{
    private Button buttonOfStamper;

    // Start is called before the first frame update
    void Start()
    {
        buttonOfStamper = GameObject.Find("Stamper").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("StamperCollider"))
        {
            buttonOfStamper.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StamperCollider"))
        {
            buttonOfStamper.enabled = true;
        }
    }
}
