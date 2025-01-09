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
    public GameObject outlinedObjectVariant;
    private Asistant asistantScript;
    public bool changeOffset = false;
    private bool isDragging = false; // Sürükleme kontrolü
    private Vector2 initialMousePosition; // Ýlk týklama pozisyonu
    public float dragThresholdDistance = 0.1f; // Sürükleme baþlama mesafesi
    public float rightOffset = 1.55f;
    public float leftOffset = 1.55f;
    public float rightOffsetWhenPageIsUp = 1.55f;
    public float leftOffsetWhenPageIsUp = 1.55f;
    public float topOffset = 1.55f;
    public float bottomOffset = 0.5f;
    public float topOffsetWhenPageIsUp = 1.55f;
    public float bottomOffsetWhenPageIsUp = 0.5f;

    private GameObject zoneObject;

    public float outOfBoundsOffset = 0.5f; // Ekrandan ne kadar uzakta olursa resetleneceðini ayarlamak için

    void Start()
    {
        cam = Camera.main;
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
        originalPos = new Vector2(0, 0);
        asistantScript = GameObject.Find("AsistantMechanic").GetComponent<Asistant>();
        zoneObject = GameObject.Find("zone");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
            Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
            minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
            maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
        }

        CheckOutOfBounds(); // Ekrandan çýkýþ kontrolü her karede yapýlýr
    }

    private void CheckOutOfBounds()
    {
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);

        // Eðer ekranýn dýþýnda outOfBoundsOffset mesafesinde ise sýfýr konumuna döner
        if (screenPos.x < -outOfBoundsOffset || screenPos.x > 1 + outOfBoundsOffset ||
            screenPos.y < -outOfBoundsOffset || screenPos.y > 1 + outOfBoundsOffset)
        {
            transform.position = originalPos;
        }
    }

    private void OnMouseDown()
    {
        if (!isDraggable) return; // Sürükleme devrede deðilse iþlemi durdur

        difference = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

        initialMousePosition = cam.ScreenToWorldPoint(Input.mousePosition); // Ýlk týklama pozisyonu
        isDragging = false; // Henüz sürükleme baþlamadý

        if (swapPosScript.folderUp)
        {
            draggableObject.transform.localPosition = originalPos;
        }
        else
        {
            folderDraggableObject.transform.localPosition = originalPos;
        }
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return; // Sürükleme devrede deðilse iþlemi durdur

        Vector2 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float dragDistance = Vector2.Distance(initialMousePosition, currentMousePosition); // Fare hareketi mesafesi

        // Fare hareketi belirli bir mesafeyi geçerse sürüklemeyi baþlat
        if (!isDragging && dragDistance >= dragThresholdDistance)
        {
            isDragging = true;

            // Sürükleme baþladýðýnda zoneObject aktif edilir
            if (!zoneObject.activeSelf)
            {
                zoneObject.SetActive(true);
            }
        }

        if (isDragging)
        {
            Vector2 newPos = currentMousePosition - difference;

            newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
            newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

            transform.position = newPos;
        }
    }

    private void OnMouseUp()
    {
        // Sürükleme býrakýldýðýnda zoneObject deaktif edilir
        if (zoneObject.activeSelf)
        {
            zoneObject.SetActive(false);
        }

        isDragging = false; // Sürükleme sona erdi
    }


    private void OnMouseOver()
    {
        if (asistantScript.isClickedOnAsistant)
        {
            outlinedObjectVariant.gameObject.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        outlinedObjectVariant.gameObject.SetActive(false);
    }

    public void PageOffset()
    {
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffsetWhenPageIsUp, bottomLeft.y + bottomOffsetWhenPageIsUp);
        maxBounds = new Vector2(topRight.x - rightOffsetWhenPageIsUp, topRight.y - topOffsetWhenPageIsUp);
    }

    public void Offset()
    {
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
    }
}