using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspect2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float speed;
    [SerializeField]
    private bool isMiddle;
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private bool isRight;
    public Transform middle;
    public Transform right;
    public Transform left;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPosition();
        CheckMiddle();
    }

    void CheckPosition()
    {
        if (transform.position.x == middle.position.x)
        {
            transform.position = middle.position;
            SuspectManager.instance.isMoving2 = false;
            isMiddle = true;
            isLeft = false;
            isRight = false;
        }
        else if (transform.position.x == left.position.x)
        {
            transform.position = left.position;
            isLeft = true;
            isRight = false;
            isMiddle = false;
        }
        else if (transform.position.x == right.position.x)
        {
            transform.position = right.position;
            isRight = true;
            isMiddle = false;
            isLeft = false;
        }
    }

    void CheckMiddle()
    {
        if (isMiddle)
        {
            anim.SetBool("inLeft", false);
            anim.SetBool("inRight", false);

            if (SuspectManager.instance.isMoving1 || SuspectManager.instance.isMoving3)
            {

                anim.SetBool("outRight", true);
            }
            /* else if ()
             {
                 anim.SetBool("outLeft", true);
             }
            */
            isLeft = false;
            isRight = false;
        }
    }


    public void MovementSuspect()
    {
        anim.SetBool("inLeft", false);
        anim.SetBool("inRight", false);
        anim.SetBool("outRight", false);
        anim.SetBool("outLeft", false);

        if (isLeft)
        {
            anim.SetBool("inLeft", true);
            SuspectManager.instance.isMoving2 = true;
        }
        else if (isRight)
        {
            anim.SetBool("inRight", true);
            SuspectManager.instance.isMoving2 = true;

        }
    }


}



