using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprites : MonoBehaviour
{
    public Sprite shadowSprite;
    public Sprite noShadowSprite;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShadowColliderObject")) 
        {
            spriteRenderer.sprite = noShadowSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("ShadowColliderObject"))
        {
            spriteRenderer.sprite = shadowSprite;
        }
    }

}
