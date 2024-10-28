using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwapPos : MonoBehaviour
{
    public bool inTheZone = false;
    public bool inTheSecondZone = false;
    public bool folderUp = true;
    
    public Animator animator;
    public Animator FolderPageAnimator;

    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    public BoxCollider2D tableCollider;

    public GameObject draggableObject;
    public GameObject folderObject;

    public DraggableObject DraggableObjectScript;
    private Vector2 originalPos;

    public float[] values;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        originalPos = new Vector2(0, 0);

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
            inTheSecondZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TakeToHandZone"))
        {
            inTheZone = false;
        }
        if (other.CompareTag("TakeBackZone"))
        {
            inTheSecondZone = false;
        }
    }

    private void OnMouseUp()
    {
        if (inTheZone && folderUp)
        {
            Swap();
        }

        if (inTheZone && !folderUp && inTheSecondZone)
        {
            SwapBack();
        }
    }

    private void Swap() 
    {

        DraggableObjectScript.isDraggable = false;
        rigidBody2D.gravityScale = 0f;
        boxCollider2D.enabled = false;
        boxCollider2D.size = new Vector2(values[2], values[3]);
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
        folderObject.transform.localPosition = Vector2.MoveTowards(folderObject.transform.localPosition, Vector2.zero, -150.0f * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DraggableObjectScript.isDraggable = true;
        DraggableObjectScript.PageOffset();
    }

    private void SwapBack() 
    {

        DraggableObjectScript.isDraggable = false;
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(values[0], values[1]);
        animator.SetTrigger("goToOriginalPos");
        tableCollider.enabled = false;
        FolderPageAnimator.SetTrigger("goToOriginalPos");
        StartCoroutine(ReturnToNormalPos2(1.0f));
    }

    private IEnumerator ReturnToNormalPos2(float delay)
    {
        yield return new WaitForSeconds(delay);


        gameObject.transform.Translate(0, 2.2f, 0);



        
        draggableObject.transform.localPosition = Vector2.MoveTowards(draggableObject.transform.localPosition, Vector2.zero, 100.0f * Time.deltaTime);


        
        yield return new WaitForSeconds(0.2f);
        folderUp = true;
        inTheZone = false;
        inTheSecondZone = false;
        boxCollider2D.isTrigger = false;
        //boxCollider2D.enabled = true;
        tableCollider.enabled = true;
        rigidBody2D.gravityScale = 1.0f;
        //draggableObject.transform.localPosition = Vector2.MoveTowards(draggableObject.transform.localPosition, Vector2.zero, 175.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("goBackIdle");
        FolderPageAnimator.SetTrigger("goBackIdle");
        yield return new WaitForSeconds(0.5f);
        DraggableObjectScript.isDraggable = true;
        DraggableObjectScript.Offset();
        //boxCollider2D.enabled = true;
        //boxCollider2D.isTrigger = true;
    }
}
