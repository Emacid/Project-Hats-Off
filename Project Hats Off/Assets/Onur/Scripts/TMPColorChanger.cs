using UnityEngine;
using TMPro; // TextMesh Pro'yu kullanabilmek için

public class TMPColorChanger : MonoBehaviour
{
    // Parent objeye ait fonksiyon
    public void ChangeTMPColorsToBlack()
    {
        // Parent objesinin altýndaki tüm child objeleri ve onlarýn child'larýný tarar
        ChangeColorRecursively(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            ChangeTMPColorsToBlack();
        }
    }
    // Rekürsif olarak tüm child objeleri tarayan fonksiyon
    private void ChangeColorRecursively(Transform parent)
    {
        // TextMesh Pro bileþenlerini ara
        TextMeshProUGUI[] tmpTexts = parent.GetComponentsInChildren<TextMeshProUGUI>(true); // true: inactive objelerde de ara

        // Bulunan tüm TMP'lerin rengini siyah yap
        foreach (TextMeshProUGUI tmp in tmpTexts)
        {
            tmp.color = new Color(0f, 0f, 0f); // Siyah renk
        }

        // Bu fonksiyon her child objeyi de kontrol etmeye devam edecek
        foreach (Transform child in parent)
        {
            // Rekürsif olarak child objelere de ayný iþlemi uygula
            ChangeColorRecursively(child);
        }
    }
}
