using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare basýldýðý andaki zamaný saklayacak
    public float clickThreshold = 0.2f; // Týklama sayýlacak maksimum süre (0.2 saniye örneði)
    public bool clickedOnSuspect = false;

    public GameObject text01;
    public Transform allTextsParent;

    public bool canCloseTheOutline = false;

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
                    // Týklanan nesneleri `sortingOrder` deðerine göre sýrala
                    List<GameObject> clickedObjects = hits
                        .Select(hit => hit.collider.gameObject)
                        .OrderByDescending(obj => GetSortingOrder(obj))
                        .ToList();

                    // Týklanan nesneleri sýralý bir þekilde loga yazdýr
                    Debug.Log("Týklanan nesneler (üstten alta doðru):");
                    for (int i = 0; i < clickedObjects.Count; i++)
                    {
                        GameObject obj = clickedObjects[i];
                        int sortingOrder = GetSortingOrder(obj);
                        Debug.Log($"{i + 1}: {obj.name} (Layer: {sortingOrder})");

                        // Eðer MouseDown'da týklanan nesne ile aynýysa
                        if (obj == clickedObjectOnMouseDown)
                        {
                            
                            // Eðer týklanan nesnenin adý "Clickable2" ise
                            if (obj.name == "Clickable2")
                            {
                                Debug.Log("Sonunda týkladýn!");
                            }
                            else if (obj.name == "AnimatedCircle1")
                                {
                                InstantiateTexts(text01);
                                GameObject.Find("AnimatedCircle1").SetActive(false); //Burayý daha sonrasýnda bütün kaðýttaki evidence'lar kapanacak þekilde deðiþtir!!!!
                                }
                            }
                    }
                }
            }

            // Mouse býrakýldýðýnda týklanan nesneyi sýfýrla
            clickedObjectOnMouseDown = null;
        }
    }

    // Nesnenin veya çocuk nesnelerinin `SpriteRenderer` bileþeninden `sortingOrder` deðerini alýr
    private int GetSortingOrder(GameObject obj)
    {
        // Önce nesnenin kendisinde `SpriteRenderer` olup olmadýðýný kontrol et
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        // Eðer `SpriteRenderer` yoksa, alt nesnelerde kontrol et
        if (spriteRenderer == null)
        {
            spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        }

        // `SpriteRenderer` bulunduysa `sortingOrder` deðerini döndür, yoksa en düþük deðeri döndür
        return spriteRenderer != null ? spriteRenderer.sortingOrder : int.MinValue;
    }

    private void InstantiateTexts(GameObject textnumber) 
    {
        Instantiate(textnumber, allTextsParent);
        canCloseTheOutline = true;
    }
}
