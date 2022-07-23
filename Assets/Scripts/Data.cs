using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data instance;
    public int PlayerNum = 1;

    public int starNumInLev1;
    public int starNumInLev2;
    public int starNumInLev3;
    public int starNumInLev4;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
    }


    public void setSinglePlayer()           
    {
        PlayerNum = 1;
    }

    public void setMultiplayer()        
    {
        PlayerNum = 2;
    }


    public void setLev(int starNum,int lev)
    {
        switch (lev)
        {
            case 1:
                starNumInLev1 = starNum;
                break;
            case 2:
                starNumInLev2 = starNum;
                break;
            case 3:
                starNumInLev3 = starNum;
                break;
            case 4:
                starNumInLev4 = starNum;
                break;
        }
        
    }
}
