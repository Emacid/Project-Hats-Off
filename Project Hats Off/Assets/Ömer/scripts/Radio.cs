using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] musics;
    public Button radioButton;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.5f;
    public bool isSingleClick;
    
    private void Start()
    {
        if(radioButton != null)
        {
            radioButton.onClick.AddListener(OnButtonClick);
        }
        print(source.volume);
    }
  

    public void OnButtonClick()
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            isSingleClick = false;
            OnDoubleClick();   
        }
        else
        {
            isSingleClick = true;
            Invoke(nameof(PerformSingleClick), doubleClickThreshold);       
        }

        lastClickTime = Time.time;
    }

    void PerformSingleClick()
    {
        if (isSingleClick)
        {
            OnSingleClick();
        }
    }

    void OnDoubleClick()
    {
        if (source.volume > 0)
        {
            source.volume = 0;
        }
        else
        {
            source.volume = 0.5f;
        }
    }

    void OnSingleClick()
    {
        for (int i = 0; i < musics.Length; i++)
        {
            if (source.clip == musics[i])
            {
                source.clip = musics[(i + 1) % musics.Length];
                source.Play();
                return;
            }
        }

        source.clip = musics[0];
        source.Play();
    }



    private void OnDestroy()
    {
        if (radioButton != null)
        {
            radioButton.onClick.RemoveListener(OnButtonClick);
        }
    }


}
