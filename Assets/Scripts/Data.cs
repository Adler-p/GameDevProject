using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int PlayerNum = 1;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);        
    }

    public void setSinglePlayer()           
    {
        PlayerNum = 1;
    }

    public void setMultiplayer()        
    {
        PlayerNum = 2;
    }
}
