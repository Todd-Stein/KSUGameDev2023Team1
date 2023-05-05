using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    public GameObject soundlocation;
    public AudioClip[] growls;
    public AudioSource shout;
    int num;
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
        if (other.gameObject.layer == 6 && other.isTrigger || other.gameObject.layer == 7 && other.isTrigger || other.tag == "NoiseMaker" && other.isTrigger)
        {
            soundlocation = other.gameObject;
            try { tony.GetComponent<Tony>().OnAlert(other.gameObject, other.GetComponent<SoundEvent>().noise); } // Call Tony's alert script, increasing aggression by noise level
            catch { }
            Debug.Log("Heared sound event" + other.name + " other.enebled = " + other.enabled);

            num = Random.Range(0, 6);
            shout.PlayOneShot(growls[num]);

            Debug.Log("Played growl number: " + num);
        }
    }
}
