using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
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
    public float channelVolumes = 1.0f;
    public Sprite[] radioSprites;
    public SpriteRenderer radioSpriteRenderer;
    public Animator radioAnimator;
    public SpriteRenderer secondRadio;
    public Sprite offSprite;
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
            channels[currentChannel].volume = channelVolumes;
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
        if (isSingleClick && !isMuted)
        {
            OnSingleClick();
            if(currentChannel == 0) 
            {
                radioSpriteRenderer.sprite = radioSprites[0];
            }
            else if (currentChannel == 1)
            {
                radioSpriteRenderer.sprite = radioSprites[1];
            }
            else if (currentChannel == 2)
            {
                radioSpriteRenderer.sprite = radioSprites[2];
            }
        }
    }

    public void OnDoubleClick()
    {
        if (!isMuted)
        {
            secondRadio.enabled = false;
            StartCoroutine(RadioFix2());
            radioAnimator.SetTrigger("TurnOffRadio");
            isMuted = true;
            channels[currentChannel].volume = 0;
        } 
        else
        {
            secondRadio.enabled = false;
            radioAnimator.SetTrigger("TurnOnRadio");
            isMuted = false;
            channels[currentChannel].volume = channelVolumes;
            StartCoroutine(RadioFix());
            secondRadio.sprite = radioSprites[currentChannel];
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

        channels[currentChannel].volume = channelVolumes;
    }

    private void OnDestroy()
    {
        if (radioButton != null)
        {
            radioButton.onClick.RemoveListener(OnButtonClick);
        }
    }

    private IEnumerator RadioFix()
    {
        yield return new WaitForSeconds(0.7f);
        secondRadio.enabled = true;

    }

    private IEnumerator RadioFix2()
    {
        yield return new WaitForSeconds(1.0f);
        secondRadio.sprite = offSprite;
        secondRadio.enabled = true;

    }
}
