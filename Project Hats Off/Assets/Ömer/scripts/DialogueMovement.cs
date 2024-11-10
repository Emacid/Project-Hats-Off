using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 offset;
    private Vector3 target;
    private Vector3 finishTarget;
    private float lerpTime = 0;
    public float movementSpeed = 0.5f; 

    public GameObject[] leftPos;
    public GameObject[] rightPos;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not found on this GameObject. Ensure it is a UI element.");
            return;
        }

        offset = rectTransform.localPosition;
        target = new Vector3(offset.x, offset.y - 100, offset.z);
        finishTarget = new Vector3(offset.x + 300, offset.y, offset.z);
    }

    public bool Movement()
    {
        lerpTime += Time.deltaTime * movementSpeed;
        rectTransform.localPosition = Vector3.Lerp(offset, target, lerpTime);

     
        if (lerpTime >= 1)
        {
            lerpTime = 0;
            offset = target;
            target = new Vector3(offset.x, offset.y - 100, offset.z);
            return true; 
        }
        return false; 
    }
    
}
