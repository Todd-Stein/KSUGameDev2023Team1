using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Returntomainmenu : MonoBehaviour
{

    private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audiosource.isPlaying)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
