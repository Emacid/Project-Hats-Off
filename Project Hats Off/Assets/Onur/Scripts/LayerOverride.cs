using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOverride : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private float timer;
    private bool togglePriority = true;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        
        if (timer >= 1f)
        {
            timer = 0f;

            if (boxCollider2D != null)
            {
                
                if (togglePriority)
                {
                    boxCollider2D.layerOverridePriority = 4;
                }
                else
                {
                    boxCollider2D.layerOverridePriority = 5;
                }
                togglePriority = !togglePriority;
            }
        }
    }
}