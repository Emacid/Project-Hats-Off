using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNo : MonoBehaviour
{
    public SpriteRenderer parentSpriteRenderer;
    public Sprite mainSprite;
    public Sprite noHoverSprite;
    public GameObject parentObject;
    private AudioSource clickSound;
    void Start()
    {
        clickSound = GameObject.Find("AsistantClickAudio").GetComponent<AudioSource>();
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
        clickSound.Play();
    }

}