using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    private int currentPage = 1; // Baþlangýç sayfasý (ilk sayfa 1 olarak ayarlanýr)
    private List<GameObject> pages = new List<GameObject>();
    public GameObject ileri;
    public GameObject geri;

    void Start()
    {
        // Tüm child objeleri al ve listeye ekle
        for (int i = 0; i < transform.childCount; i++)
        {
            pages.Add(transform.GetChild(i).gameObject);
        }

        // Sadece ilk sayfayý açýk tut
        UpdatePageVisibility();
    }

    // Ýleri sayfaya geçiþ
    public void NextPage()
    {
        if (currentPage < pages.Count)
        {
            currentPage++;
            UpdatePageVisibility();
        }
    }

    // Geri sayfaya geçiþ
    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdatePageVisibility();
        }
    }

    // Sayfalarýn görünürlüðünü güncelle
    private void UpdatePageVisibility()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == currentPage - 1); // Sayfa numarasýný 1'e göre düzenle
        }

        // Butonlarýn aktiflik durumunu ayarla
        if (currentPage == 1)
        {
            ileri.SetActive(true); // Ýleri açýk
            geri.SetActive(false); // Geri kapalý
        }
        else if (currentPage == pages.Count)
        {
            ileri.SetActive(false); // Ýleri kapalý
            geri.SetActive(true); // Geri açýk
        }
        else
        {
            ileri.SetActive(true); // Ýleri açýk
            geri.SetActive(true); // Geri açýk
        }
    }
}
