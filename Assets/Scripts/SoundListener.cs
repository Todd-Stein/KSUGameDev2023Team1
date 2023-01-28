using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public GameObject soundlocation;
    // Start is called before the first frame update
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
        Debug.Log("Heared sound event");
        if(soundlocation == null || other.gameObject.GetComponent<SoundEvent>().noise > soundlocation.GetComponent<SoundEvent>().noise)
        {
            soundlocation = other.gameObject;
        }
    }
}
