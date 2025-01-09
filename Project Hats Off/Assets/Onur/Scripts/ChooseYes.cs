using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseYes : MonoBehaviour
{
    public SpriteRenderer parentSpriteRenderer;
    public Sprite mainSprite;
    public Sprite noHoverSprite;
    public GameObject parentObject;
    public GameObject ballotStamp;
    void Start()
    {

    }


    void Update()
    {

    }

    private void OnMouseOver()
    {
        parentSpriteRenderer.sprite = noHoverSprite;
    }

    private void OnMouseExit()
    {
        parentSpriteRenderer.sprite = mainSprite;
    }

    private void OnMouseDown()
    {
        ballotStamp.SetActive(true);
        if(gameObject.name == "TrueYes") 
        {
            print("DOÐRU SUSPECT!");
        }
        else 
        {
            print("YANLIS SUSPECT!");
        }
    }

}