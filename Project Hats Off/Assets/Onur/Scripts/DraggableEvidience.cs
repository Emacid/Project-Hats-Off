using UnityEngine;

public class DraggableEvidience : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Camera cam;
    private Vector2 minBounds, maxBounds;

    public bool isDraggable = true; // S�r�klemeyi kontrol eden boolean
    public float leftOffset = 0.5f;
    public float rightOffset = 0.5f;
    public float topOffset = 0.5f;
    public float bottomOffset = 0.5f;
    public float outOfBoundsOffset = 0.5f; // Ekrandan ne kadar uzakta olursa resetlenece�ini ayarlamak i�in

    private Vector2 originalPos;

    void Start()
    {
        cam = Camera.main;
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
        originalPos = transform.position;
    }

    private void Update()
    {
        CheckOutOfBounds(); // Ekrandan ��k�� kontrol� her karede yap�l�r
    }

    private void CheckOutOfBounds()
    {
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);

        // E�er ekran�n d���nda `outOfBoundsOffset` mesafesinde ise s�f�r konumuna d�ner
        if (screenPos.x < -outOfBoundsOffset || screenPos.x > 1 + outOfBoundsOffset ||
            screenPos.y < -outOfBoundsOffset || screenPos.y > 1 + outOfBoundsOffset)
        {
            transform.position = originalPos;
        }
    }

    private void OnMouseDown()
    {
        if (!isDraggable) return; // S�r�kleme devrede de�ilse i�lemi durdur
        difference = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return; // S�r�kleme devrede de�ilse i�lemi durdur
        Vector2 newPos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - difference;

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
    }
}
