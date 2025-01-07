using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNo : MonoBehaviour
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
        parentObject.SetActive(false);
    }

}