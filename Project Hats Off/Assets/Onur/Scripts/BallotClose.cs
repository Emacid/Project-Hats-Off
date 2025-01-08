using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallotClose : MonoBehaviour
{
    public GameObject ballotHover;
    public GameObject ballotObject;
    public Animator ballotAnimator;
    private Stamper stamper;

    // Start is called before the first frame update
    void Start()
    {
        stamper = GameObject.Find("Stamper").GetComponent<Stamper>();
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
        stamper.SetLayersBack();
        StartCoroutine(BallotFix());
        Destroy(ballotObject, 5.0f);
    }

    private IEnumerator BallotFix()
    {
        yield return new WaitForSeconds(1.0f);
        stamper.isBallotUp = false;

    }

}
