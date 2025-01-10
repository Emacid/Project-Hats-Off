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
    private ChooseTheGuilty finishGameObject;
    void Start()
    {
        finishGameObject = GameObject.Find("2").GetComponent<ChooseTheGuilty>();
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
            finishGameObject.RightKillerr();
        }
        else 
        {
            print("YANLIS SUSPECT!");
            finishGameObject.WrongKillerr();
        }
    }

}