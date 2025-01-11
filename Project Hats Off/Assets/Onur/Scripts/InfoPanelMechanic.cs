using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelMechanic : MonoBehaviour
{
    public GameObject infoPanelObject;
    public GameObject ballotBlack;
    public Stamper stamper;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openInfoPanel() 
    {
        infoPanelObject.SetActive(true);
        ballotBlack.SetActive(true);
        Time.timeScale = 0;
        stamper.ChangeLayers();
    }

    public void closeInfoPanel()
    {
        infoPanelObject.SetActive(false);
        ballotBlack.SetActive(false);
        Time.timeScale = 1;
        stamper.SetLayersBack();
    }

}
