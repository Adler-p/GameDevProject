using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void returnToMenu()      
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
}
