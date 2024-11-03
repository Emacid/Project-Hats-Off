using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspect3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]
    private bool isMiddle;
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private bool isRight;
    public Transform middle;
    public Transform right;
    public Transform left;
    public Evidence currentEvidence;
    public enum Evidence
    {
        obj1,
        obj2,
        obj3,
        obj4
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        CheckMiddle();
        CheckPosition();
        ChooseEvidence();
        CheckQuestion();
    }

    private void OnMouseDown()
    {
        TextWriter.instance.StartDialogue();
    }

    public void SetEvidence(int objectNumber)
    {
        currentEvidence = (Evidence)objectNumber;
    }


    void ChooseEvidence()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SetEvidence(0);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            SetEvidence(1);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SetEvidence(2);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SetEvidence(3);
        }
    }

    void CheckQuestion()
    {
        switch (currentEvidence)
        {
            case Evidence.obj1:
                DialogueManager.instance.SetQuestion(0);
                break;
            case Evidence.obj2:
                DialogueManager.instance.SetQuestion(1);
                break;
            case Evidence.obj3:
                DialogueManager.instance.SetQuestion(2);
                break;
            case Evidence.obj4:
                DialogueManager.instance.SetQuestion(3);
                break;
        }
    }

    public void CheckPosition()
    {
        if (transform.position.x == middle.position.x)
        {
            transform.position = middle.position;
            SuspectManager.instance.isMoving3 = false;
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

    public void CheckMiddle()
    {
        if (isMiddle)
        {
            anim.SetBool("inLeft", false);
            anim.SetBool("inRight", false);

            if (SuspectManager.instance.isMoving1 || SuspectManager.instance.isMoving2)
            {
                anim.SetBool("outRight", true);
            }

            DialogueManager.instance.SetSuspect(2);
            SuspectManager.instance.isMoving3 = false;

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
            SuspectManager.instance.isMoving3 = true;
        }
        else if (isRight)
        {
            anim.SetBool("inRight", true);
            SuspectManager.instance.isMoving3 = true;

        }
    }


}