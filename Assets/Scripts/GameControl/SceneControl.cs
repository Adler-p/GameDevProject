using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    
    [SerializeField]float currentTime;
    float time = 0.5f;

    private void LateUpdate()
    {
        //print(currentTime);
        if (!name.Equals(""))
        {
            //print(currentTime);
            currentTime += 0.01f;
            if (name.Equals("restart") && currentTime > time)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                name = "";
            }
            else if (name.Equals("return") && currentTime > time)
            {
                SceneManager.LoadScene(0);
                name = "";
            }
        }
    }
    public void loadScene(int sceneID)  
    {
        SceneManager.LoadScene(sceneID);
    }
    public void exit()          
    {
        Application.Quit();
    }

    public void ResetGame() 
    {
        Time.timeScale = 1;
        name = "restart";
       
    }

    public void returnToMenu()      
    {
        Time.timeScale = 1;
        name = "return";
    }

    public void single()
    {
        Data.instance.setSinglePlayer();
    }

    public void muti()
    {
        Data.instance.setMultiplayer();
    }
}
