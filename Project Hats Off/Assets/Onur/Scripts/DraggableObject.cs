using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Camera cam;
    private Vector2 minBounds, maxBounds;
    public bool isDraggable = true; // Sürüklemeyi kontrol eden boolean
    public BoxCollider2D takeToHandZone;
    private Vector2 originalPos;
    public GameObject draggableObject;
    public GameObject folderDraggableObject;
    public SwapPos swapPosScript;

    public float rightOffset = 1.55f;
    public float leftOffset = 1.55f;
    public float topOffset = 1.55f;
    public float bottomOffset = 0.5f;

    void Start()
    {
        cam = Camera.main;
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
        originalPos = new Vector2(0, 0);
    }

    private void OnMouseDown()
    {
        if (!isDraggable) return; // Sürükleme devrede deðilse iþlemi durdur
        difference = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        if (swapPosScript.folderUp)
        {
            draggableObject.transform.localPosition = originalPos;
        }

        if (!swapPosScript.folderUp)
        {
            folderDraggableObject.transform.localPosition = originalPos;
        }
    }

    private void OnMouseDrag()
    {
        if (swapPosScript.folderUp)
        {
            draggableObject.transform.localPosition = originalPos;
        }

        if (!swapPosScript.folderUp)
        {
            folderDraggableObject.transform.localPosition = originalPos;
        }

        if (!isDraggable) return; // Sürükleme devrede deðilse iþlemi durdur
        Vector2 newPos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - difference;

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
    }
}
