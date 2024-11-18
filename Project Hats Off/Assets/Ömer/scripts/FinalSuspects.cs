using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FinalSuspects : MonoBehaviour
{
    public RectTransform suspectsRectTransform;

    public Vector3 offset;
    public Vector3 targetIn;
    public Vector3 targetOut;

    public float moveSpeed;
    
    public bool onPressed;
    public bool isMoving;

    private void Start()
    {
        suspectsRectTransform = GetComponent<RectTransform>();
        offset = suspectsRectTransform.localPosition;
        targetIn = new Vector3(offset.x - 200, offset.y, offset.z);
        targetOut = new Vector3(offset.x, offset.y, offset.z);
    }


    public void Move()
    {
        if (isMoving) return;
        
        if (!onPressed)
        {
            StartCoroutine(Movement(targetIn));
            onPressed = true;
        }
        else if (onPressed)
        {
            StartCoroutine(Movement(targetOut));
            onPressed = false;
        }
    }



    IEnumerator Movement(Vector3 target)
    {
        float lerpTime = 0;
     
        while(lerpTime < 1)
        {
            isMoving = true;
            lerpTime += Time.deltaTime * moveSpeed;

            suspectsRectTransform.localPosition = Vector3.Lerp(offset, target, lerpTime);


            if (lerpTime >= 1)
            {
                StopAllCoroutines();
                offset = target;
                lerpTime = 0;
                isMoving = false;
            }
            yield return null;

        }

    }


}
