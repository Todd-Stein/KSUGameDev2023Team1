using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentSound : MonoBehaviour
{

    private SoundEvent SE;

    //public AudioClip sound;
    private AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        SE = GetComponentInChildren<SoundEvent>();
        Debug.Log(SE.name);
        au = gameObject.GetComponentInChildren<AudioSource>();
        //au.clip = sound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NoiseMaker" || other.gameObject.layer == 10)
        {
            return;
        }
        if(other.tag == "Player")
        {
            Debug.Log("EnviroSound Activated");
            //au.Play();
            SE.activateNoiseEvent();
        }
        else if(other.tag == "Tony")
        {
            au.Play();
        }
    }
}
