using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare bas�ld��� andaki zaman� saklayacak
    public float clickThreshold = 0.2f; // T�klama say�lacak maksimum s�re (0.2 saniye �rne�i)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare butonu bas�ld���nda
        {
            // Ekran t�klama pozisyonunu d�nya koordinatlar�na �evir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            if (hits.Length > 0)
            {
                // �lk t�klan�lan nesneyi sakla
                clickedObjectOnMouseDown = hits[0].collider.gameObject;
                mouseDownTime = Time.time; // Fare bas�ld��� zaman� kaydet
            }
        }

        if (Input.GetMouseButtonUp(0)) // Sol fare butonu b�rak�ld���nda
        {
            float mouseUpTime = Time.time; // Fare b�rak�ld��� zaman� al
            float heldTime = mouseUpTime - mouseDownTime; // Bas�l� tutma s�resini hesapla

            if (heldTime <= clickThreshold) // E�er bas�l� tutma s�resi belirlenen e�ikten k���kse
            {
                // Ekran t�klama pozisyonunu d�nya koordinatlar�na �evir
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                if (hits.Length > 0)
                {
                    // T�klanan b�t�n nesneleri kontrol et
                    List<GameObject> clickedObjects = new List<GameObject>();

                    foreach (RaycastHit2D hit in hits)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        clickedObjects.Add(clickedObject);
                    }

                    // T�klanan nesneleri loga yazd�r ve kontrol et
                    Debug.Log("T�klanan nesneler:");
                    foreach (GameObject obj in clickedObjects)
                    {
                        Debug.Log(obj.name);

                        // E�er MouseDown'da t�klanan nesne ile ayn�ysa
                        if (obj == clickedObjectOnMouseDown)
                        {
                            // E�er t�klanan nesnenin ad� "Clickable2" ise
                            if (obj.name == "Clickable2")
                            {
                                Debug.Log("Sonunda t�klad�n!");
                            }
                        }
                    }
                }
            }
            else
            {
                //Debug.Log("T�klama say�lmad�, ��nk� fare uzun s�re bas�l� tutuldu.");
            }

            // Mouse b�rak�ld���nda t�klanan nesneyi s�f�rla
            clickedObjectOnMouseDown = null;
        }
    }
}
