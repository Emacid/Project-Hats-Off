using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    private int currentPage = 1; // Ba�lang�� sayfas� (ilk sayfa 1 olarak ayarlan�r)
    private List<GameObject> pages = new List<GameObject>();

    void Start()
    {
        // T�m child objeleri al ve listeye ekle
        for (int i = 0; i < transform.childCount; i++)
        {
            pages.Add(transform.GetChild(i).gameObject);
        }

        // Sadece ilk sayfay� a��k tut
        UpdatePageVisibility();
    }

    // �leri sayfaya ge�i�
    public void NextPage()
    {
        if (currentPage < pages.Count)
        {
            currentPage++;
            UpdatePageVisibility();
        }
    }

    // Geri sayfaya ge�i�
    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdatePageVisibility();
        }
    }

    // Sayfalar�n g�r�n�rl���n� g�ncelle
    private void UpdatePageVisibility()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == currentPage - 1); // Sayfa numaras�n� 1'e g�re d�zenle
        }
    }
}
