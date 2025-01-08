using UnityEngine;
using TMPro; // TextMesh Pro'yu kullanabilmek i�in

public class TMPColorChanger : MonoBehaviour
{
    // Parent objeye ait fonksiyon
    public void ChangeTMPColorsToBlack()
    {
        // Parent objesinin alt�ndaki t�m child objeleri ve onlar�n child'lar�n� tarar
        ChangeColorRecursively(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            ChangeTMPColorsToBlack();
        }
    }
    // Rek�rsif olarak t�m child objeleri tarayan fonksiyon
    private void ChangeColorRecursively(Transform parent)
    {
        // TextMesh Pro bile�enlerini ara
        TextMeshProUGUI[] tmpTexts = parent.GetComponentsInChildren<TextMeshProUGUI>(true); // true: inactive objelerde de ara

        // Bulunan t�m TMP'lerin rengini siyah yap
        foreach (TextMeshProUGUI tmp in tmpTexts)
        {
            tmp.color = new Color(0f, 0f, 0f); // Siyah renk
        }

        // Bu fonksiyon her child objeyi de kontrol etmeye devam edecek
        foreach (Transform child in parent)
        {
            // Rek�rsif olarak child objelere de ayn� i�lemi uygula
            ChangeColorRecursively(child);
        }
    }
}
