using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIconChanger : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D clickTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private bool canOpenHover = true;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Sol fare tu�una bas�ld���nda s�r�kleme ba�lar
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Cursor.SetCursor(clickTexture, hotSpot, cursorMode);
        }

        // Sol fare tu�u b�rak�ld���nda s�r�kleme biter
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        // E�er s�r�kleme devam ediyorsa
        if (isDragging)
        {
            // Buraya s�r�kleme s�ras�nda yap�lacak di�er i�lemleri ekleyebilirsiniz.
        }
    }

    void OnMouseEnter()
    {
        if (!isDragging) // S�r�kleme yap�lm�yorsa hover ikonunu de�i�tir
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        if (!isDragging) // S�r�kleme yap�lm�yorsa fare ikonunu normale d�nd�r
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    public void MouseHover()
    {
        if (canOpenHover && !isDragging)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    public void MouseNormal()
    {
        if (!isDragging)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    private IEnumerator ClickChanger()
    {
        canOpenHover = false;
        Cursor.SetCursor(clickTexture, hotSpot, cursorMode);
        yield return new WaitForSeconds(0.2f);
        canOpenHover = true;
        if (!isDragging) // S�r�kleme yap�lm�yorsa normale d�nd�r
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
