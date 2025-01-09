using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheCollider : MonoBehaviour
{
    
    public BoxCollider2D[] boxCollider2Ds;
    public SuspectOutline[] suspectOutlines;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (BoxCollider2D boxCollider in boxCollider2Ds)
        {
            boxCollider.enabled = false;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (BoxCollider2D boxCollider in boxCollider2Ds)
        {
            boxCollider.enabled = true;
        }

        foreach (SuspectOutline suspectOutline in suspectOutlines)
        {
            //suspectOutline.canInteract = true;
        }
    }
}
