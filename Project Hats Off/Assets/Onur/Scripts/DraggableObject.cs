using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DraggableObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    private Camera cam;
    private Vector2 minBounds, maxBounds;
    public BoxCollider2D takeToHandZone;




    // Her bir kenar için ayrý offset deðerleri
    public float rightOffset = 1.55f;
    public float leftOffset = 1.55f;
    public float topOffset = 1.55f;
    public float bottomOffset = 0.5f;

    void Start()
    {
        cam = Camera.main;

        // Kameranýn köþe noktalarýný hesaplayalým
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));

        // Minimum ve maksimum x, y sýnýrlarýný ayarlayýn (Her kenar için farklý offset deðerleriyle)
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

        // Pozisyonu ekran sýnýrlarýna göre sýnýrlýyoruz (Her kenar için ayrý offsetlerle)
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        // Objenin pozisyonunu ayarlýyoruz
        transform.position = newPos;
    }

    



}
