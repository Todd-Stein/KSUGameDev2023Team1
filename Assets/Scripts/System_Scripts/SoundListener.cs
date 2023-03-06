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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoundEvent>() && !other.GetComponent<Door>() && other.GetComponent<SphereCollider>().enabled == true)
        {
            soundlocation = other.gameObject;
            tony.GetComponent<Tony>().OnAlert(soundlocation, soundlocation.GetComponent<SoundEvent>().noise); // Call Tony's alert script, increasing aggression by noise level
            Debug.Log("Heared sound event");
        }
    }
}
