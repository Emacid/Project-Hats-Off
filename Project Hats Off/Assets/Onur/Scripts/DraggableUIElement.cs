using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUIElement : MonoBehaviour, IDragHandler
{
    public Canvas canvas;
    public ClickableObject clickableObjectScript;

    private RectTransform rectTransform;
    private Animator animator;

    public Collider2D colliderOfReferanceInTable;
    public Rigidbody2D rigidbody2DOfReferanceInTable;

    private float positionX;

    public GameObject referanceInTable;  // Oyun sahnesindeki obje
    public Vector2 offset = new Vector2(0, -20); // UI eleman�n� nereye yerle�tirece�imizin offset'i

    private Vector2 currentPosObject;

    private bool hasUpdatedPosition = false;  // Pozisyonun sadece bir kez ayarlanmas�n� kontrol etmek i�in

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(referanceInTable.transform.position);
        }

        if (gameObject.activeInHierarchy == true && !hasUpdatedPosition)
        {
            // UI objesinin pozisyonunu bir kez ayarla
            UpdatePosition();
            hasUpdatedPosition = true;  // Bir kez pozisyon ayarland��� i�in tekrar yap�lmas�n� engeller
            StartCoroutine(DisableAnimatorAfterDelay(1.0f));
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    private void UpdatePosition()
    {
        // referanceInTable'�n SADECE pozisyonunu al
        Vector3 worldPosition = referanceInTable.transform.position;

        // D�nya konumunu Canvas i�indeki UI pozisyonuna �evir
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);

        // Canvas'daki ekran pozisyonunu local pozisyona �evir (UI'n�n kendi koordinatlar�)
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPoint, canvas.worldCamera, out Vector2 localPoint);

        // Yeni pozisyonu belirle (offset ekleyerek)
        rectTransform.anchoredPosition = localPoint + offset;
    }

    private IEnumerator DisableAnimatorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = false;
    }

    public void closeWindow()
    {
        animator.enabled = true;
        clickableObjectScript.canOpenTheFolder = false;
        this.gameObject.SetActive(false);
        hasUpdatedPosition = false;  // UI tekrar aktif oldu�unda pozisyonu tekrar ayarlamak i�in
        //currentPosObject = clickableObjectScript.currentPos;
        clickableObjectScript.canGoUp = true;

    }

}