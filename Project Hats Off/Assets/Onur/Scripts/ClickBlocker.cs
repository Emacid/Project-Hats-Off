using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        // Eðer mouse ekranýn alt yarýsýnda ise, týklamalarý engelle
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþu
        {
            Vector3 mousePosition = Input.mousePosition;

            // Eðer mouse ekranýn alt yarýsýnda ise
            if (mousePosition.y <= Screen.height / 2)
            {
                // PointerEventData oluþturup, týklamanýn herhangi bir yere gitmesini engelle
                PointerEventData pointerData = new PointerEventData(eventSystem);
                pointerData.position = mousePosition;

                // EventSystem'de hiçbir etkileþimi geçirme
                eventSystem.RaycastAll(pointerData, new System.Collections.Generic.List<RaycastResult>());

                // Burada hiçbir iþlem yapýlmasýný engellemiþ olduk
                return;
            }

            // Ekranýn üst yarýsýna týklama yapýlmýþsa, normal iþlemler devam eder
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Týklama iþlemi yapýlabilir
                Debug.Log("Týklama üst yarýda yapýldý.");
            }
        }
    }
}
