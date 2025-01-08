using UnityEngine;

public class DraggableEvidence : MonoBehaviour
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
    public AudioClip clickSound;
    public AudioClip impactSound;
    public float clickVolume = 1f;
    public float impactVolume = 1f;
    private AudioSource audioSource;

    private Vector2 originalPos;

    private bool isDragging = false;
    private Vector2 initialMousePosition;
    private float dragThreshold = 0.1f; // Fare hareketi e�i�i
    private float clickStartTime;
    private float clickThreshold = 0.2f; // Maksimum t�klama s�resi (saniye)

    private Rigidbody2D rb;

    private float lastSoundTime = -2f; // En son ses �alma zaman�
    private float soundCooldown = 2f; // Ayn� sesi tekrar �alma s�resi (saniye)

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
        if (!isDraggable) return;

        clickStartTime = Time.time;
        initialMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        isDragging = false; // S�r�kleme hen�z ba�lamad�
        difference = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        Debug.Log("MouseDown tetiklendi.");
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return;

        Vector2 currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(initialMousePosition, currentMousePosition);

        // E�er fare hareketi e�ik de�erini ge�erse, s�r�kleme alg�lan�r
        if (distance > dragThreshold)
        {
            isDragging = true;
        }

        Vector2 newPos = currentMousePosition - difference;
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
        Debug.Log("S�r�kleme alg�land�.");
    }

    private void OnMouseUp()
    {
        float clickDuration = Time.time - clickStartTime;
        Vector2 finalMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(initialMousePosition, finalMousePosition);

        Debug.Log("MouseUp tetiklendi.");
        Debug.Log($"T�klama s�resi: {clickDuration}, Hareket mesafesi: {distance}");

        // T�klama, hem s�re hem de fare hareketi a��s�ndan e�ik de�erlerini ge�miyorsa tetiklenir
        if (!isDragging && clickDuration <= clickThreshold && distance <= dragThreshold)
        {
            Debug.Log("T�klama alg�land�, ses �al�n�yor.");
            PlaySound(clickSound, clickVolume);
        }
        else
        {
            Debug.Log("S�r�kleme nedeniyle t�klama iptal edildi.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > 2f) // �arpma �iddeti e�i�i
        {
            Debug.Log("�arpma alg�land�, ses �al�n�yor.");
            PlaySound(impactSound, impactVolume);
        }
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && Time.time - lastSoundTime >= soundCooldown)
        {
            audioSource.PlayOneShot(clip, volume);
            lastSoundTime = Time.time; // Son �alma zaman�n� g�ncelle
        }
        else
        {
            Debug.Log("Ses �alma bekleme s�resi nedeniyle engellendi.");
        }
    }
}
