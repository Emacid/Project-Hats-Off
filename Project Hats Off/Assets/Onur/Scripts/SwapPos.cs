using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwapPos : MonoBehaviour
{
    public bool inTheZone = false;
    public bool inTheSecondZone = false;
    public bool folderUp = true;
    public bool inLetterZone = false;
    public bool inIdBookZone = false;
    public bool isObjectLetter = false;
    public bool isObjectIdBook = false;

    public Animator animator;
    public Animator FolderPageAnimator;

    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    public BoxCollider2D tableCollider;

    public BoxCollider2D[] triggeringColliders;

    public GameObject draggableObject;
    public GameObject folderObject;

    public DraggableObject DraggableObjectScript;
    private Vector2 originalPos;
    private Vector2 decrasePos;
    private Vector2 decrasePosForMiniFolder;

    public float[] values;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        originalPos = new Vector2(0, 0);
        decrasePos = new Vector2(0, -20);
        decrasePosForMiniFolder = new Vector2(0, -19.125f);
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

        if (other.CompareTag("LetterZone"))
        {
            inLetterZone = true;
        }

        if (other.CompareTag("IDBookZone"))
        {
            inIdBookZone = true;
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
        if (other.CompareTag("LetterZone"))
        {
            inLetterZone = false;
        }
        if (other.CompareTag("IDBookZone"))
        {
            inIdBookZone = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TakeToHandZone"))
        {
            inTheZone = true;
        }

        if (collision.collider.CompareTag("TakeBackZone"))
        {
            Debug.Log("TakeBackZone!");
            inTheSecondZone = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TakeToHandZone"))
        {
            inTheZone = false;
        }
        if (collision.collider.CompareTag("TakeBackZone"))
        {
            inTheSecondZone = false;
        }
    }


    private void OnMouseUp()
    {

        if (inTheZone && folderUp)
        {
            Swap();
            //StartCoroutine(TriggeringCollidersWhileSwapping());
        }
        if (isObjectLetter && inLetterZone && !folderUp)
        {
            SwapBack();
            //StartCoroutine(TriggeringCollidersWhileSwapping());
            StartCoroutine(BoxColliderFix());
            
        }
        else if (isObjectIdBook && inIdBookZone && !folderUp)
        {
            SwapBack();
            //StartCoroutine(TriggeringCollidersWhileSwapping());
            StartCoroutine(BoxColliderFix());

        }
        else if (!isObjectLetter && !isObjectIdBook && !inTheZone && !folderUp && inTheSecondZone)
        {
            SwapBack();
            //StartCoroutine(TriggeringCollidersWhileSwapping());
            StartCoroutine(BoxColliderFix());
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
        folderObject.transform.localPosition = originalPos + decrasePos;
        animator.SetTrigger("canOpenTheObject");
        FolderPageAnimator.SetTrigger("GoUpNow");
        gameObject.layer = 8;
        Debug.Log("Folder konumlarý Swaplanýyor!");
        inTheZone = false;
        folderUp = false;
    }

    private IEnumerator ReturnToNormalPos(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.transform.Translate(0, 5.0f, 0);
        boxCollider2D.enabled = true;
        //boxCollider2D.isTrigger = true;
        //folderObject.transform.localPosition = Vector2.MoveTowards(folderObject.transform.localPosition, Vector2.zero, -350.0f * Time.deltaTime);
        yield return new WaitForSeconds(1);
        DraggableObjectScript.isDraggable = true;
        DraggableObjectScript.PageOffset();
    }

    private void SwapBack()
    {

        DraggableObjectScript.isDraggable = false;
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(values[0], values[1]);
        draggableObject.transform.localPosition = originalPos + decrasePosForMiniFolder;
        animator.SetTrigger("goToOriginalPos");
        //tableCollider.enabled = false;
        FolderPageAnimator.SetTrigger("goToOriginalPos");
        if (gameObject.CompareTag("Object"))
        {
            gameObject.layer = 6;
        }
        else if (gameObject.CompareTag("Photo"))
        {
            gameObject.layer = 7;
        }
        gameObject.layer = 6;
        gameObject.transform.Translate(0, 2.2f, 0);
        StartCoroutine(ReturnToNormalPos2(1.0f));
    }

    private IEnumerator ReturnToNormalPos2(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        //gameObject.transform.Translate(0, 2.2f, 0);




        //draggableObject.transform.localPosition = Vector2.MoveTowards(draggableObject.transform.localPosition, Vector2.zero, 190.0f * Time.deltaTime);




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

    private IEnumerator TriggeringCollidersWhileSwapping()
    {
        yield return new WaitForSeconds(1.0f);
        triggeringColliders[0].enabled = false;
        triggeringColliders[1].enabled = false;
        triggeringColliders[2].enabled = false;
        yield return new WaitForSeconds(1.1f);
        triggeringColliders[0].enabled = true;
        triggeringColliders[1].enabled = true;
        triggeringColliders[2].enabled = true;

    }

    private IEnumerator BoxColliderFix()
    {
        
        yield return new WaitForSeconds(0.5f);
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1.1f);
        boxCollider2D.enabled = true;
    }

}