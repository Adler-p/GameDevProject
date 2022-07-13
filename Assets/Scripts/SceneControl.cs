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

    public void ResertGame(int sceneID) 
    {
        SceneManager.LoadScene(sceneID);
    }

    public void returnToMenu()      
    {
        SceneManager.LoadScene(0);
    }
}
