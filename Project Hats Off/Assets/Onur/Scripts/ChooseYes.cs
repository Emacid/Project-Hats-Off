using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseYes : MonoBehaviour
{
    public SpriteRenderer parentSpriteRenderer;
    public Sprite mainSprite;
    public Sprite noHoverSprite;
    public GameObject parentObject;

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