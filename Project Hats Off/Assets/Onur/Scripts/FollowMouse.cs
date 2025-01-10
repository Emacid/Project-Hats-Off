using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Fareye olan mesafeyi ayarlamak i�in de�i�ken
    public Vector2 offset = new Vector2(-500f, 0f);

    // Update is called once per frame
    void Update()
    {
        // Fare pozisyonunu al�yoruz
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z eksenini s�f�rl�yoruz, ��nk� 2D bir sahnede Z ekseni genellikle sabittir
        mousePosition.z = 0;

        // Fare pozisyonuna offset ekliyoruz
        mousePosition.x += offset.x;
        mousePosition.y += offset.y;

        // Nesnenin pozisyonunu fare ile belirtilen mesafeye g�re ayarl�yoruz
        transform.position = mousePosition;
    }
}
