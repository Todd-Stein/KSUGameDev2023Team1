using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    SoundEvent Sound;
    AudioSource au;

    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponentInParent<SoundEvent>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Sound.activateNoiseEvent();
        if(au.isPlaying == false)
        {
            GameObject.Destroy(gameObject);
        }

    }
}
