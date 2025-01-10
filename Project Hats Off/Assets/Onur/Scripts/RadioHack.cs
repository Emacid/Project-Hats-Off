using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioHack : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color hoverColor;

    // Start is called before the first frame update
    void Start()
    {
        // RGB değerlerini 255'e bölerek normalleştiriyoruz
        hoverColor = new Color(217 / 255f, 214 / 255f, 214 / 255f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    private void OnMouseOver()
    {
        // Normalleştirilmiş RGB değerlerini kullanıyoruz
        spriteRenderer.color = new Color(217 / 255f, 214 / 255f, 214 / 255f, 1f);
        print("radyoda");
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("radyoda");
        spriteRenderer.color = new Color(217 / 255f, 214 / 255f, 214 / 255f, 1f);
        
        if (collision.CompareTag("FareTakip")) 
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.color = Color.white;
        if (collision.CompareTag("FareTakip"))
        {
            
        }
    }

}
