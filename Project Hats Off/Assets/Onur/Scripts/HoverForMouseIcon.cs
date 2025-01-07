using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverForMouseIcon : MonoBehaviour
{
    private MouseIconChanger MouseIconChanger;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        MouseIconChanger = GameObject.Find("Mouse Icon Changer").GetComponent<MouseIconChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        MouseIconChanger.MouseHover();
    }

    private void OnMouseExit()
    {
        MouseIconChanger.MouseNormal();
    }
}
