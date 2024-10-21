using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickableObject : MonoBehaviour
{
    private Vector3 mouseDownPosition;
    private float dragThreshold = 0.1f; // Týklama ve sürüklemeyi ayýrt etmek için kullanýlacak mesafe eþiði
    public bool canOpenTheFolder = false;
    public Vector2 currentPos;

    public GameObject folder1;

    public bool canGoUp = false;

    public float deltaTimeValue = 5.0f;
    public float waitForSecondValue = 1.35f;

    private Collider2D collider2D;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGoUp) 
        {

            StartCoroutine(FloatBackUp());
            Debug.Log("Float Back Up BRO!");

        }
        
        if(Input.GetKeyDown(KeyCode.J))
            {
            Debug.Log(currentPos);
            }
    }

    private void OnMouseDown()
    {
        // Fareye týklanýldýðýnda pozisyonu kaydediyoruz
        mouseDownPosition = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        // Fare býrakýldýðýnda pozisyonu kontrol ediyoruz
        Vector3 mouseUpPosition = Input.mousePosition;

        // Fareyi ne kadar hareket ettirdiðimizi hesaplýyoruz
        float distance = Vector3.Distance(mouseDownPosition, mouseUpPosition);

        // Eðer hareket edilen mesafe küçükse, bu bir týklamadýr
        if (distance < dragThreshold)
        {
            //folder1.gameObject.SetActive(true);
            //OpenFolder1();
        }

        if (canOpenTheFolder)
        {
            OpenFolder1();
            canOpenTheFolder = false;
        }

    }

    public void OpenFolder1() 
        {
        folder1.gameObject.SetActive(true);
        currentPos = Random.insideUnitCircle + (Vector2)transform.position;

        collider2D.enabled = false;
        rigidbody2D.gravityScale = 0;

        Vector2 newPos = new Vector2(currentPos.x, currentPos.y - 10);
        gameObject.transform.position = newPos; 
    }

    private IEnumerator FloatBackUp()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(currentPos.x, -3.0f), deltaTimeValue * Time.deltaTime);
        yield return new WaitForSeconds(waitForSecondValue);
        canGoUp = false;
        collider2D.enabled = true;
        rigidbody2D.gravityScale = 1;
        Debug.Log("Float Back Up FALSE!!");
    }
}
