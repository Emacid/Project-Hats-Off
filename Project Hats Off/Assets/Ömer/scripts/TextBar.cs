using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBar : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    private ContentSizeFitter contentSizeFitter;
    private RectTransform rectTransform;

    public float initialWidth;
    public float initialHeight;


    private void Start()
    {
        contentSizeFitter = GetComponent<ContentSizeFitter>();
        rectTransform = GetComponent<RectTransform>();
        contentSizeFitter.enabled = false;

        rectTransform.sizeDelta = new Vector2(initialWidth, initialHeight);

    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(textComponent.text))
        {
            contentSizeFitter.enabled = true;
        }
    }

}
