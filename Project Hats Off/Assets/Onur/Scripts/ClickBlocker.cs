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
        // E�er mouse ekran�n alt yar�s�nda ise, t�klamalar� engelle
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�u
        {
            Vector3 mousePosition = Input.mousePosition;

            // E�er mouse ekran�n alt yar�s�nda ise
            if (mousePosition.y <= Screen.height / 2)
            {
                // PointerEventData olu�turup, t�klaman�n herhangi bir yere gitmesini engelle
                PointerEventData pointerData = new PointerEventData(eventSystem);
                pointerData.position = mousePosition;

                // EventSystem'de hi�bir etkile�imi ge�irme
                eventSystem.RaycastAll(pointerData, new System.Collections.Generic.List<RaycastResult>());

                // Burada hi�bir i�lem yap�lmas�n� engellemi� olduk
                return;
            }

            // Ekran�n �st yar�s�na t�klama yap�lm��sa, normal i�lemler devam eder
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // T�klama i�lemi yap�labilir
                Debug.Log("T�klama �st yar�da yap�ld�.");
            }
        }
    }
}
