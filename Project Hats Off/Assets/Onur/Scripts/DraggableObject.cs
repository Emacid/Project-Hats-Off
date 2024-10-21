using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    private Camera cam;
    private Vector2 minBounds, maxBounds;
    public BoxCollider2D takeToHandZone;
    private ClickableObject clickableObjectScript;

    // Her bir kenar i�in ayr� offset de�erleri
    public float rightOffset = 1.55f;
    public float leftOffset = 1.55f;
    public float topOffset = 1.55f;
    public float bottomOffset = 0.5f;

    void Start()
    {
        clickableObjectScript = GetComponent<ClickableObject>();
        
        cam = Camera.main;

        // Kameran�n k��e noktalar�n� hesaplayal�m
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // Minimum ve maksimum x, y s�n�rlar�n� ayarlay�n (Her kenar i�in farkl� offset de�erleriyle)
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        Vector2 newPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;

        // Pozisyonu ekran s�n�rlar�na g�re s�n�rl�yoruz (Her kenar i�in ayr� offsetlerle)
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        // Objenin pozisyonunu ayarl�yoruz
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TakeToHandZone")) 
        {
            //Debug.Log("eline ald�!");
            //clickableObjectScript.OpenFolder1();
            clickableObjectScript.canOpenTheFolder = true;
        }
    }

}
