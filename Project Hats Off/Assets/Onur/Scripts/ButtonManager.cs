using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare basýldýðý andaki zamaný saklayacak
    public float clickThreshold = 0.2f; // Týklama sayýlacak maksimum süre (0.2 saniye örneði)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare butonu basýldýðýnda
        {
            // Ekran týklama pozisyonunu dünya koordinatlarýna çevir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            if (hits.Length > 0)
            {
                // Ýlk týklanýlan nesneyi sakla
                clickedObjectOnMouseDown = hits[0].collider.gameObject;
                mouseDownTime = Time.time; // Fare basýldýðý zamaný kaydet
            }
        }

        if (Input.GetMouseButtonUp(0)) // Sol fare butonu býrakýldýðýnda
        {
            float mouseUpTime = Time.time; // Fare býrakýldýðý zamaný al
            float heldTime = mouseUpTime - mouseDownTime; // Basýlý tutma süresini hesapla

            if (heldTime <= clickThreshold) // Eðer basýlý tutma süresi belirlenen eþikten küçükse
            {
                // Ekran týklama pozisyonunu dünya koordinatlarýna çevir
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

                if (hits.Length > 0)
                {
                    // Týklanan bütün nesneleri kontrol et
                    List<GameObject> clickedObjects = new List<GameObject>();

                    foreach (RaycastHit2D hit in hits)
                    {
                        GameObject clickedObject = hit.collider.gameObject;
                        clickedObjects.Add(clickedObject);
                    }

                    // Týklanan nesneleri loga yazdýr ve kontrol et
                    Debug.Log("Týklanan nesneler:");
                    foreach (GameObject obj in clickedObjects)
                    {
                        Debug.Log(obj.name);

                        // Eðer MouseDown'da týklanan nesne ile aynýysa
                        if (obj == clickedObjectOnMouseDown)
                        {
                            // Eðer týklanan nesnenin adý "Clickable2" ise
                            if (obj.name == "Clickable2")
                            {
                                Debug.Log("Sonunda týkladýn!");
                            }
                        }
                    }
                }
            }
            else
            {
                //Debug.Log("Týklama sayýlmadý, çünkü fare uzun süre basýlý tutuldu.");
            }

            // Mouse býrakýldýðýnda týklanan nesneyi sýfýrla
            clickedObjectOnMouseDown = null;
        }
    }
}
