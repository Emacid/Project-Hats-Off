using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceAnim : MonoBehaviour
{
    private GameObject childObject;
    private Collider2D objectCollider;

    public ButtonManager buttonManager;


    void Start()
    {
        // �lk child objeyi referans al�yoruz
        if (transform.childCount > 0)
        {
            childObject = transform.GetChild(0).gameObject;
            childObject.SetActive(false); // Ba�lang��ta deaktif yap�yoruz
        }

        // Bu scriptin ba�l� oldu�u collider'� al�yoruz
        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Fare pozisyonunu d�nya koordinatlar�na �eviriyoruz
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Fare collider'�n i�inde mi kontrol ediyoruz
        if (objectCollider != null && objectCollider.OverlapPoint(mousePosition))
        {
            if (childObject != null && !childObject.activeSelf && buttonManager.clickedOnSuspect) 

            {
                childObject.SetActive(true); // Child objeyi aktif yap
            }
        }
        else
        {
            if (childObject != null && childObject.activeSelf)
            {
                childObject.SetActive(false); // Child objeyi deaktif yap
            }
        }
    }
}
