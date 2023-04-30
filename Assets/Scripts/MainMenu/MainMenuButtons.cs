using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] UIsounds;

    private AudioSource audioSource;
    private GameObject creditsCanvas;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        creditsCanvas = GameObject.Find("CreditsCanvas");
        creditsCanvas.SetActive(false);
    }

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
        creditsCanvas.SetActive(true);
    }

    public void HideCreditsOverlay()
    {
        Debug.Log("Exit Credits");
        creditsCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit pressed");
        Application.Quit();
    }

    public void ClickNoise()
    {
        audioSource.PlayOneShot(UIsounds[0]);
    }

    public void BackNoise()
    {
        audioSource.PlayOneShot(UIsounds[1]);
    }

    public void HoverNoise()
    {
        audioSource.PlayOneShot(UIsounds[2]);
    }
}
