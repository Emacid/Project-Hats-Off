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
        // Sol fare tuþuna basýldýðýnda sürükleme baþlar
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Cursor.SetCursor(clickTexture, hotSpot, cursorMode);
        }

        // Sol fare tuþu býrakýldýðýnda sürükleme biter
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        // Eðer sürükleme devam ediyorsa
        if (isDragging)
        {
            // Buraya sürükleme sýrasýnda yapýlacak diðer iþlemleri ekleyebilirsiniz.
        }
    }

    void OnMouseEnter()
    {
        if (!isDragging) // Sürükleme yapýlmýyorsa hover ikonunu deðiþtir
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }

    void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        if (!isDragging) // Sürükleme yapýlmýyorsa fare ikonunu normale döndür
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
        if (!isDragging) // Sürükleme yapýlmýyorsa normale döndür
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
