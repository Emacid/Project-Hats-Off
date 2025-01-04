using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    public GameObject page1; // 1. Sayfa
    public GameObject page2; // 2. Sayfa
    public GameObject page3; // 3. Sayfa
    private int currentPage = 1; // Ba�lang�� sayfas�

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Fare t�klamas� alg�la
        {
            // Fare pozisyonunu d�nya koordinatlar�na d�n��t�r
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast ile t�klanan objeyi kontrol et
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // E�er collider'a t�kland�ysa
            if (hit.collider != null)
            {
                Debug.Log("T�kland�: " + hit.collider.gameObject.name);  // T�klanan objenin ismini logla

                // E�er do�ru GameObject'e t�klan�yorsa, sayfa ge�i�i yap
                if (hit.collider.gameObject == page1)
                {
                    Debug.Log("1. sayfaya t�kland�.");
                    SwitchPage();
                }
                else if (hit.collider.gameObject == page2)
                {
                    Debug.Log("2. sayfaya t�kland�.");
                    SwitchPage();
                }
                else if (hit.collider.gameObject == page3)
                {
                    Debug.Log("3. sayfaya t�kland�.");
                    SwitchPage();
                }
            }
            else
            {
                Debug.Log("Hi�bir objeye t�klanmad�");
            }
        }
    }

    private void SwitchPage()
    {
        // Sayfalar aras�nda ge�i�
        if (currentPage == 1)
        {
            page1.SetActive(false);
            page3.SetActive(false);
            page2.SetActive(true);
            currentPage = 2;
        }
        else if (currentPage == 2)
        {
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);
            currentPage = 3;
        }
        else if (currentPage == 3)
        {
            page2.SetActive(false);
            page3.SetActive(false);
            page1.SetActive(true);
            currentPage = 1;
        }
    }
}
