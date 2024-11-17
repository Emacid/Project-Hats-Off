using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidienceOnTable : MonoBehaviour
{

    public ButtonManager buttonManager;

    [SerializeField]
    private GameObject childObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (buttonManager.clickedOnSuspect && buttonManager.canShowOutlineOfEvidiences)
        {
            
            {
                childObject.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (buttonManager.clickedOnSuspect)
        {
            childObject.SetActive(false);
        }
    }

}
