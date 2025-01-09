using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallotClose : MonoBehaviour
{
    public GameObject ballotHover;
    public GameObject ballotObject;
    public Animator ballotAnimator;
    private Stamper stamper;
    private GameObject ballotBlack;

    // Start is called before the first frame update
    void Start()
    {
        stamper = GameObject.Find("Stamper").GetComponent<Stamper>();
        ballotBlack = GameObject.Find("BallotBlack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        ballotHover.SetActive(true);
    }

    private void OnMouseExit()
    {
        ballotHover.SetActive(false);
    }

    private void OnMouseDown()
    {
        ballotAnimator.SetTrigger("GoBack");
        //stamper.SetLayersBack();
        
        stamper.DeselectButton();
        StartCoroutine(BallotFix());
        StartCoroutine(BallotFix2());
        Destroy(ballotObject, 5.0f);
        ballotBlack.SetActive(false);
    }

    private IEnumerator BallotFix()
    {
        yield return new WaitForSeconds(1.0f);
        stamper.isBallotUp = false;
        /*
        stamper.button.interactable = true;
        stamper.isClickedOnAsistant = true;
        stamper.ClickedOnAsistant();
        */
    }

    private IEnumerator BallotFix2()
    {
        yield return new WaitForSeconds(0.2f);
        stamper.button.interactable = true;
        stamper.isClickedOnAsistant = true;
        stamper.ClickedOnAsistant();
    }
}
