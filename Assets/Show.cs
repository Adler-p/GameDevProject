using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    public float changgeSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Image>().color.a> 0)
        {
            Color targetColor = GetComponent<Image>().color;
            targetColor.a -= Time.deltaTime * changgeSpeed;
            GetComponent<Image>().color = targetColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
