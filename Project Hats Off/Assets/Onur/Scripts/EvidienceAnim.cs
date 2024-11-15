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
        // Ýlk child objeyi referans alýyoruz
        if (transform.childCount > 0)
        {
            childObject = transform.GetChild(0).gameObject;
            childObject.SetActive(false); // Baþlangýçta deaktif yapýyoruz
        }

        // Bu scriptin baðlý olduðu collider'ý alýyoruz
        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Fare pozisyonunu dünya koordinatlarýna çeviriyoruz
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Fare collider'ýn içinde mi kontrol ediyoruz
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
