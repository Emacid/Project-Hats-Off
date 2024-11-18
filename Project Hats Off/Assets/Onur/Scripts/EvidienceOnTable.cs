using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidienceOnTable : MonoBehaviour
{

    public ButtonManager buttonManager;

    [SerializeField]
    private GameObject childObject;
    public Asistant asistantMechanic;

    public GameObject asistantAnswer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (asistantMechanic.triggerToClosingTheOutline) 
        {
            TriggerToClosingTheOutline();
        }
    }

    private void OnMouseOver()
    {
        if (buttonManager.canShowOutlineOfEvidiences && asistantMechanic.isClickedOnAsistant)
        {

            {
                asistantAnswer.SetActive(true);
            }
        }
        else if (buttonManager.clickedOnSuspect && buttonManager.canShowOutlineOfEvidiences)
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

        if (!buttonManager.clickedOnSuspect)
        {
            asistantAnswer.SetActive(false);
        }
    }


    private void TriggerToClosingTheOutline() 
    {
        asistantAnswer.SetActive(false);
        asistantMechanic.triggerToClosingTheOutline = false;
    }
}
