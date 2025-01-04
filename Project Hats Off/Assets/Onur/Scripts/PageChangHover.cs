using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChangHover : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D eksik! MAL!");
        }
    }

    void Update()
    {
        if (boxCollider != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider.OverlapPoint(mousePosition))
            {
                if (!spriteRenderer.enabled) // Yeniden etkinle�tirme �nleme
                {
                    spriteRenderer.enabled = true;
                    print("Fare collider alan�nda!");
                }
            }
            else
            {
                if (spriteRenderer.enabled) // Yeniden devre d��� b�rakma �nleme
                {
                    spriteRenderer.enabled = false;
                }
            }
        }
    }
}
