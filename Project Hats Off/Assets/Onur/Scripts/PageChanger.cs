using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] allPages;

    [SerializeField]
    private int myPage; // Baþlangýç sayfasý

    public SpriteRenderer folderSprite;
    public Sprite[] changingSprites;

    private void Start()
    {
        // Baþlangýçta tüm sayfalarý kapat ve sadece myPage'i aktif yap
        for (int i = 0; i < allPages.Length; i++)
        {
            allPages[i].SetActive(i == myPage - 1); // Sadece myPage-1 açýk
        }

        // Baþlangýç sprite'ýný ayarla
        UpdateFolderSprite(myPage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týklama
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mouse'un pozisyonundaki Collider'ý bul
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject) // Doðrudan bu objeyi kontrol et
            {
                if (this.gameObject.name == "Forward")
                {
                    Debug.Log("Bir Sonraki Sayfaya Geçiþ Yapýlýyor!");
                    HandlePageChange(1); // Ýleri sayfa
                }
                else if (this.gameObject.name == "Backward")
                {
                    Debug.Log("Bir Önceki Sayfaya Geçiþ Yapýlýyor!");
                    HandlePageChange(-1); // Geri sayfa
                }
            }
        }
    }

    private void HandlePageChange(int direction)
    {
        int nextPage = myPage + direction;

        // Mevcut sayfayý deaktive et
        if (myPage > 0 && myPage <= allPages.Length)
        {
            allPages[myPage - 1].SetActive(false);
        }

        // Bir sonraki sayfayý aktive et
        if (nextPage > 0 && nextPage <= allPages.Length)
        {
            allPages[nextPage - 1].SetActive(true);
        }

        // Sprite dosyasýný deðiþtir
        UpdateFolderSprite(nextPage);

        // Sayfa indeksini güncelle
        myPage = nextPage;
    }

    private void UpdateFolderSprite(int nextPage)
    {
        // nextPage 1'den büyük ve changingSprites dizisinde mevcutsa güncelle
        if (nextPage > 0 && nextPage <= changingSprites.Length)
        {
            folderSprite.sprite = changingSprites[nextPage - 1];
        }
    }
}
