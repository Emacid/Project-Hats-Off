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

    private void Start()
    {
        // Ba�lang��ta t�m sayfalar� kapat ve sadece myPage'i aktif yap
        for (int i = 0; i < allPages.Length; i++)
        {
            allPages[i].SetActive(i == myPage - 1); // Sadece myPage-1 a��k
        }

        // Ba�lang�� sprite'�n� ayarla
        UpdateFolderSprite(myPage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�klama
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mouse'un pozisyonundaki Collider'� bul
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject) // Do�rudan bu objeyi kontrol et
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
            }
        }
    }

    private void HandlePageChange(int direction)
    {
        int nextPage = myPage + direction;

        // Mevcut sayfay� deaktive et
        if (myPage > 0 && myPage <= allPages.Length)
        {
            allPages[myPage - 1].SetActive(false);
        }

        // Bir sonraki sayfay� aktive et
        if (nextPage > 0 && nextPage <= allPages.Length)
        {
            allPages[nextPage - 1].SetActive(true);
        }

        // Sprite dosyas�n� de�i�tir
        UpdateFolderSprite(nextPage);

        // Sayfa indeksini g�ncelle
        myPage = nextPage;
    }

    private void UpdateFolderSprite(int nextPage)
    {
        // nextPage 1'den b�y�k ve changingSprites dizisinde mevcutsa g�ncelle
        if (nextPage > 0 && nextPage <= changingSprites.Length)
        {
            folderSprite.sprite = changingSprites[nextPage - 1];
        }
    }
}
