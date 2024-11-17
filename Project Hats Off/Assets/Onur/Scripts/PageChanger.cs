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

    public int maxPageNumber = 4;

    private void Start()
    {
        UpdatePageVisibility();
        UpdateFolderSprite(myPage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týklama
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Tüm Collider'larý kontrol etmek için RaycastAll kullanýyoruz
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
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
                    break; // Doðru objeyi bulduðumuzda döngüyü sonlandýr
                }
            }
        }
    }


    private void HandlePageChange(int direction)
    {
        int nextPage = myPage + direction;

        if (nextPage < 1 || nextPage > maxPageNumber)
        {
            Debug.Log("Sayfa sýnýrlarý aþýldý.");
            return;
        }

        // Mevcut sayfayý kapat
        if (myPage > 0 && myPage <= allPages.Length)
        {
            allPages[myPage - 1].SetActive(false);
        }

        // Bir sonraki sayfayý aç
        if (nextPage > 0 && nextPage <= allPages.Length)
        {
            allPages[nextPage - 1].SetActive(true);
        }

        // Sprite güncelle
        UpdateFolderSprite(nextPage);

        // Sayfa numarasýný güncelle
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
