using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DialogueMovement : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 target;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 startPos;
    private float lerpTime = 0;
    public float movementSpeed = 0.5f;

    public bool isFinished;


    public List<RectTransform> childs = new List<RectTransform>();


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
        startPos = offset;

        foreach (Transform child in transform)
        {
            RectTransform rectTransform = child.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                childs.Add(rectTransform);
            }
        }

    }

    public bool Movement() //parent object
    {
        lerpTime += Time.deltaTime * movementSpeed;
        offset = rectTransform.localPosition;

        rectTransform.localPosition = Vector3.Lerp(offset, target, lerpTime);

     
        if (lerpTime >= 1)
        {
            lerpTime = 0;
            offset = target;
            target = new Vector3(offset.x, offset.y - 100, offset.z);
            leftPos = new Vector3(offset.x - 229f, 0, 0);
            rightPos = new Vector3(offset.x + 229f, 0, 0);
            return true; 
        }
        return false; 
    }
    
    public void FinishMove() //child objects
    {
        for (int i = 0; i < childs.Count; i++)
        {
            if (childs[i].localPosition.x < 0)
            {
                StartCoroutine(FinishMovement(childs[i], childs[i].localPosition + leftPos));
            }  
            else if (childs[i].localPosition.x > 0)
            {
                StartCoroutine(FinishMovement(childs[i], childs[i].localPosition + rightPos));
            }

        }
    }

    public void SetPosition()
    {
        rectTransform.localPosition = startPos;

        for (int i = 0; i < childs.Count;i++)
        {
            if (childs[i].localPosition.x < 0)
            {
                childs[i].localPosition = new Vector3(-271, childs[i].localPosition.y, childs[i].localPosition.z);
            }
            else if (childs[i].localPosition.x > 0)
            {
                childs[i].localPosition = new Vector3(271, childs[i].localPosition.y, childs[i].localPosition.z);
            }
        }
    }


    IEnumerator FinishMovement(RectTransform childsTransform, Vector3 position)
    {
        float t = 0;
        Vector3 startPosition = childsTransform.localPosition;
        while (t < 1)
        {
            print(childsTransform.localPosition);
            t += Time.deltaTime * movementSpeed;
            childsTransform.localPosition = Vector3.Lerp(startPosition, position, t);

            yield return null;
            
            childsTransform.localPosition = position;
            isFinished = true;
        }

        SetPosition();

    }




}
