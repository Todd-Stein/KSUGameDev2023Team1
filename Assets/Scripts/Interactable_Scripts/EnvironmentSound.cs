using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentSound : MonoBehaviour
{

    private SoundEvent SE;

    public AudioClip sound;
    private AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        SE = GetComponentInChildren<SoundEvent>();
        Debug.Log(SE.name);
        au = gameObject.GetComponent<AudioSource>();
        au.clip = sound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("EnviroSound Activated");
            //au.Play();
            SE.activateNoiseEvent();
        }
        else
        {
            if (other.gameObject.layer == 8)
            {
                Debug.Log("Tony Activated Envirosound");
                //Tony Activated sound
                au.Play();
            }
        }
    }
}
