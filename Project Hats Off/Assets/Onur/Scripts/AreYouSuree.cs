using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreYouSuree : MonoBehaviour
{
    public BoxCollider2D[] parentBoxColliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        // GameObject aktifleþtiðinde tüm parentBoxColliders objelerini kapat
        foreach (BoxCollider2D suspectImage in parentBoxColliders)
        {
            if (suspectImage != null)
            {
                suspectImage.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        foreach (BoxCollider2D suspectImage in parentBoxColliders)
        {
            if (suspectImage != null)
            {
                suspectImage.gameObject.SetActive(true);
            }
        }
    }
}
