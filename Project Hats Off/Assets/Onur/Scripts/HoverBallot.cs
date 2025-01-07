using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBallot : MonoBehaviour
{

    private SpriteRenderer mySprite;
    public GameObject areYouSureObject;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        mySprite.enabled = true;
    }

    private void OnMouseExit()
    {
        mySprite.enabled = false;
    }

    private void OnMouseDown()
    {
        areYouSureObject.SetActive(true);
    }

}
