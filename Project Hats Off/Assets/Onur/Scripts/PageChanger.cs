using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] allPages;

    [SerializeField]
    private int myPage; // Ba�lang�� sayfas�

    public SpriteRenderer folderSprite;
    public Sprite[] changingSprites;

    public int maxPageNumber = 4;

    private void Start()
    {
        UpdatePageVisibility();
        UpdateFolderSprite(myPage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�klama
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // T�m Collider'lar� kontrol etmek i�in RaycastAll kullan�yoruz
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    if (this.gameObject.name == "Forward")
                    {
                        Debug.Log("Bir Sonraki Sayfaya Ge�i� Yap�l�yor!");
                        HandlePageChange(1); // �leri sayfa
                    }
                    else if (this.gameObject.name == "Backward")
                    {
                        Debug.Log("Bir �nceki Sayfaya Ge�i� Yap�l�yor!");
                        HandlePageChange(-1); // Geri sayfa
                    }
                    break; // Do�ru objeyi buldu�umuzda d�ng�y� sonland�r
                }
            }
        }
    }


    private void HandlePageChange(int direction)
    {
        int nextPage = myPage + direction;

        if (nextPage < 1 || nextPage > maxPageNumber)
        {
            Debug.Log("Sayfa s�n�rlar� a��ld�.");
            return;
        }

        // Mevcut sayfay� kapat
        if (myPage > 0 && myPage <= allPages.Length)
        {
            allPages[myPage - 1].SetActive(false);
        }

        // Bir sonraki sayfay� a�
        if (nextPage > 0 && nextPage <= allPages.Length)
        {
            allPages[nextPage - 1].SetActive(true);
        }

        // Sprite g�ncelle
        UpdateFolderSprite(nextPage);

        // Sayfa numaras�n� g�ncelle
        //myPage = nextPage;
    }

    private void UpdatePageVisibility()
    {
        for (int i = 0; i < allPages.Length; i++)
        {
            allPages[i].SetActive(i == myPage - 1);
        }
    }

    private void UpdateFolderSprite(int page)
    {
        if (page > 0 && page <= changingSprites.Length)
        {
            folderSprite.sprite = changingSprites[page - 1];
        }
    }
}
