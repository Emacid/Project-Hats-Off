using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwapPos : MonoBehaviour
{
    public bool inTheZone = false;
    public bool folderUp = true;
    
    public Animator animator;
    public Animator FolderPageAnimator;

    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    public BoxCollider2D tableCollider;

    public GameObject draggableObject;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TakeToHandZone"))
        {
            inTheZone = true;
        }

        if (other.CompareTag("TakeBackZone"))
        {
            Debug.Log("TakeBackZone!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TakeToHandZone"))
        {
            inTheZone = false;
        }
    }

    private void OnMouseUp()
    {
        if (inTheZone && folderUp)
        {
            Swap();
        }

        if (inTheZone && !folderUp)
        {
            SwapBack();
        }
    }

    private void Swap() 
    {
        
        rigidBody2D.gravityScale = 0f;
        boxCollider2D.enabled = false;

        StartCoroutine(ReturnToNormalPos(0.8f));

        //animator.SetBool("canOpenTheObject", true);
        animator.SetTrigger("canOpenTheObject");
        FolderPageAnimator.SetTrigger("GoUpNow");
        Debug.Log("Folder konumlarý Swaplanýyor!");
        inTheZone = false;
        folderUp = false;
    }

    private IEnumerator ReturnToNormalPos(float delay) 
    {
        yield return new WaitForSeconds(delay);
        gameObject.transform.Translate(0, 5.0f, 0);
        boxCollider2D.enabled = true;
        boxCollider2D.isTrigger = true;
    }

    private void SwapBack() 
    {
        
        boxCollider2D.isTrigger = true;
        animator.SetTrigger("goToOriginalPos");
        tableCollider.enabled = false;
        StartCoroutine(ReturnToNormalPos2(1.0f));
    }

    private IEnumerator ReturnToNormalPos2(float delay)
    {
        yield return new WaitForSeconds(delay);


        gameObject.transform.Translate(0, 2.2f, 0);

        


       draggableObject.transform.localPosition = Vector2.MoveTowards(draggableObject.transform.localPosition, Vector2.zero, 175.0f * Time.deltaTime);


        
        yield return new WaitForSeconds(0.2f);
        folderUp = true;
        inTheZone = false;
        boxCollider2D.isTrigger = false;
        //boxCollider2D.enabled = true;
        tableCollider.enabled = true;
        rigidBody2D.gravityScale = 1.0f;
        //boxCollider2D.enabled = true;
        //boxCollider2D.isTrigger = true;
    }
}
