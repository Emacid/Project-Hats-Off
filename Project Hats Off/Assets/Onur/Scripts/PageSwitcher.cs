using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    public GameObject page1; // 1. Sayfa
    public GameObject page2; // 2. Sayfa
    public GameObject page3; // 3. Sayfa
    private int currentPage = 1; // Baþlangýç sayfasý

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Fare týklamasý algýla
        {
            // Fare pozisyonunu dünya koordinatlarýna dönüþtür
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast ile týklanan objeyi kontrol et
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Eðer collider'a týklandýysa
            if (hit.collider != null)
            {
                Debug.Log("Týklandý: " + hit.collider.gameObject.name);  // Týklanan objenin ismini logla

                // Eðer doðru GameObject'e týklanýyorsa, sayfa geçiþi yap
                if (hit.collider.gameObject == page1)
                {
                    Debug.Log("1. sayfaya týklandý.");
                    SwitchPage();
                }
                else if (hit.collider.gameObject == page2)
                {
                    Debug.Log("2. sayfaya týklandý.");
                    SwitchPage();
                }
                else if (hit.collider.gameObject == page3)
                {
                    Debug.Log("3. sayfaya týklandý.");
                    SwitchPage();
                }
            }
            else
            {
                Debug.Log("Hiçbir objeye týklanmadý");
            }
        }
    }

    private void SwitchPage()
    {
        // Sayfalar arasýnda geçiþ
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
