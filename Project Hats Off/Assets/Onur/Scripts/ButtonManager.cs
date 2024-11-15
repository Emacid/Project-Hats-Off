using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ButtonManager : MonoBehaviour
{
    private GameObject clickedObjectOnMouseDown = null;
    private float mouseDownTime; // Fare bas�ld��� andaki zaman� saklayacak
    public float clickThreshold = 0.2f; // T�klama say�lacak maksimum s�re (0.2 saniye �rne�i)
    public bool clickedOnSuspect = false;

    public GameObject text01;
    public Transform allTextsParent;

    public bool canCloseTheOutline = false;

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
                    // T�klanan nesneleri `sortingOrder` de�erine g�re s�rala
                    List<GameObject> clickedObjects = hits
                        .Select(hit => hit.collider.gameObject)
                        .OrderByDescending(obj => GetSortingOrder(obj))
                        .ToList();

                    // T�klanan nesneleri s�ral� bir �ekilde loga yazd�r
                    Debug.Log("T�klanan nesneler (�stten alta do�ru):");
                    for (int i = 0; i < clickedObjects.Count; i++)
                    {
                        GameObject obj = clickedObjects[i];
                        int sortingOrder = GetSortingOrder(obj);
                        Debug.Log($"{i + 1}: {obj.name} (Layer: {sortingOrder})");

                        // E�er MouseDown'da t�klanan nesne ile ayn�ysa
                        if (obj == clickedObjectOnMouseDown)
                        {
                            
                            // E�er t�klanan nesnenin ad� "Clickable2" ise
                            if (obj.name == "Clickable2")
                            {
                                Debug.Log("Sonunda t�klad�n!");
                            }
                            else if (obj.name == "AnimatedCircle1")
                                {
                                InstantiateTexts(text01);
                                GameObject.Find("AnimatedCircle1").SetActive(false); //Buray� daha sonras�nda b�t�n ka��ttaki evidence'lar kapanacak �ekilde de�i�tir!!!!
                                }
                            }
                    }
                }
            }

            // Mouse b�rak�ld���nda t�klanan nesneyi s�f�rla
            clickedObjectOnMouseDown = null;
        }
    }

    // Nesnenin veya �ocuk nesnelerinin `SpriteRenderer` bile�eninden `sortingOrder` de�erini al�r
    private int GetSortingOrder(GameObject obj)
    {
        // �nce nesnenin kendisinde `SpriteRenderer` olup olmad���n� kontrol et
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        // E�er `SpriteRenderer` yoksa, alt nesnelerde kontrol et
        if (spriteRenderer == null)
        {
            spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        }

        // `SpriteRenderer` bulunduysa `sortingOrder` de�erini d�nd�r, yoksa en d���k de�eri d�nd�r
        return spriteRenderer != null ? spriteRenderer.sortingOrder : int.MinValue;
    }

    private void InstantiateTexts(GameObject textnumber) 
    {
        Instantiate(textnumber, allTextsParent);
        canCloseTheOutline = true;
    }
}
