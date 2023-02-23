using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public GameObject soundlocation;
    [SerializeField]
    GameObject tony;

    void Start()
    {
        soundlocation = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("NoiseMaker"))
    //    {
    //        soundlocation = other.gameObject;
    //        tony.GetComponent<Tony>().OnAlert(other, other.GetComponent<SoundEvent>().noise); // Call Tony's alert script, increasing aggression by noise level
    //        Debug.Log("Heared sound event");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NoiseMaker"))
        {
            soundlocation = other.gameObject;
            tony.GetComponent<Tony>().OnAlert(soundlocation, soundlocation.GetComponent<SoundEvent>().noise); // Call Tony's alert script, increasing aggression by noise level
            Debug.Log("Heared sound event");
        }
    }
}
