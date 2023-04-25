using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void LoadMainMap()
    {
        Debug.Log("Start pressed");
        SceneManager.LoadScene(1);
    }

    public void ShowOptionsOverlay()
    {
        Debug.Log("Options pressed");
        
    }

    public void ShowCreditsOverlay()
    {
        Debug.Log("Credits pressed");

    }

    public void ExitGame()
    {
        Debug.Log("Exit pressed");
        Application.Quit();
    }
}
