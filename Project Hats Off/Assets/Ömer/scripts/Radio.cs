using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public List<AudioSource> channels = new List<AudioSource>();
    public AudioClip[] musics;
    public Button radioButton;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.5f;
    public bool isSingleClick;
    public bool isMuted;
    public int currentChannel;


    private void Start()
    {
        channels.Clear();
        if (radioButton != null)
        {
            radioButton.onClick.AddListener(OnButtonClick);
        }

        foreach (Transform child in transform)
        {
            GameObject childObject = child.gameObject;
            AudioSource channel = child.GetComponent<AudioSource>();
            channels.Add(channel);
        }
        for (int j = 0; j < musics.Length; j++)
        {
            channels[j].clip = musics[j];
            channels[j].Play();
        }
        foreach (var channel in channels)
        {
            channel.volume = 0;
        }

        if (channels.Count > 0)
        {
            channels[currentChannel].volume = 0.5f;
        }
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
        if (!isMuted)
        {
            isMuted = true;
            channels[currentChannel].volume = 0;
        } 
        else
        {
            isMuted = false;
            channels[currentChannel].volume = 0.5f;
        }
        
    }

    void OnSingleClick()
    {
        channels[currentChannel].volume = 0;

        if (currentChannel >= channels.Count - 1)
        {
            currentChannel = 0;
        }
        else
        {
            currentChannel++;
        }

        channels[currentChannel].volume = 0.5f;
    }

    private void OnDestroy()
    {
        if (radioButton != null)
        {
            radioButton.onClick.RemoveListener(OnButtonClick);
        }
    }

    
}
