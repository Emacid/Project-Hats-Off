using UnityEngine;

public class DraggableEvidence : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Camera cam;
    private Vector2 minBounds, maxBounds;

    public bool isDraggable = true; // Sürüklemeyi kontrol eden boolean
    public float leftOffset = 0.5f;
    public float rightOffset = 0.5f;
    public float topOffset = 0.5f;
    public float bottomOffset = 0.5f;
    public float outOfBoundsOffset = 0.5f; // Ekrandan ne kadar uzakta olursa resetleneceðini ayarlamak için
    public AudioClip clickSound;
    public AudioClip impactSound;
    public float clickVolume = 1f;
    public float impactVolume = 1f;
    private AudioSource audioSource;

    private Vector2 originalPos;

    private bool isDragging = false;
    private Vector2 initialMousePosition;
    private float dragThreshold = 0.1f; // Fare hareketi eþiði
    private float clickStartTime;
    private float clickThreshold = 0.2f; // Maksimum týklama süresi (saniye)

    private Rigidbody2D rb;

    private float lastSoundTime = -2f; // En son ses çalma zamaný
    private float soundCooldown = 2f; // Ayný sesi tekrar çalma süresi (saniye)

    void Start()
    {
        cam = Camera.main;
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.transform.position.z));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minBounds = new Vector2(bottomLeft.x + leftOffset, bottomLeft.y + bottomOffset);
        maxBounds = new Vector2(topRight.x - rightOffset, topRight.y - topOffset);
        originalPos = transform.position;

        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        CheckOutOfBounds(); // Ekrandan çýkýþ kontrolü her karede yapýlýr
    }

    private void CheckOutOfBounds()
    {
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);

        // Eðer ekranýn dýþýnda `outOfBoundsOffset` mesafesinde ise sýfýr konumuna döner
        if (screenPos.x < -outOfBoundsOffset || screenPos.x > 1 + outOfBoundsOffset ||
            screenPos.y < -outOfBoundsOffset || screenPos.y > 1 + outOfBoundsOffset)
        {
            transform.position = originalPos;
        }
    }

    private void OnMouseDown()
    {
        if (!isDraggable) return;

        clickStartTime = Time.time;
        initialMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        isDragging = false; // Sürükleme henüz baþlamadý
        difference = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        Debug.Log("MouseDown tetiklendi.");
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return;

        Vector2 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(initialMousePosition, currentMousePosition);

        // Eðer fare hareketi eþik deðerini geçerse, sürükleme algýlanýr
        if (distance > dragThreshold)
        {
            isDragging = true;
        }

        Vector2 newPos = currentMousePosition - difference;
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
        Debug.Log("Sürükleme algýlandý.");
    }

    private void OnMouseUp()
    {
        float clickDuration = Time.time - clickStartTime;
        Vector2 finalMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(initialMousePosition, finalMousePosition);

        Debug.Log("MouseUp tetiklendi.");
        Debug.Log($"Týklama süresi: {clickDuration}, Hareket mesafesi: {distance}");

        // Týklama, hem süre hem de fare hareketi açýsýndan eþik deðerlerini geçmiyorsa tetiklenir
        if (!isDragging && clickDuration <= clickThreshold && distance <= dragThreshold)
        {
            Debug.Log("Týklama algýlandý, ses çalýnýyor.");
            PlaySound(clickSound, clickVolume);
        }
        else
        {
            Debug.Log("Sürükleme nedeniyle týklama iptal edildi.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > 2f) // Çarpma þiddeti eþiði
        {
            Debug.Log("Çarpma algýlandý, ses çalýnýyor.");
            PlaySound(impactSound, impactVolume);
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && Time.time - lastSoundTime >= soundCooldown)
        {
            audioSource.PlayOneShot(clip, volume);
            lastSoundTime = Time.time; // Son çalma zamanýný güncelle
        }
        else
        {
            Debug.Log("Ses çalma bekleme süresi nedeniyle engellendi.");
        }
    }
}
