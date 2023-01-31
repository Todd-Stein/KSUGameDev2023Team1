using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sound System for making sound events
//Sound objects will need a sphere collider attatched to act as a trigger; Needs to be disabled inistially
public class SoundEvent : MonoBehaviour
{
    public int noise;
    public float noiseradius;
    private SphereCollider soundtrigger;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        soundtrigger = gameObject.GetComponent<SphereCollider>();
        soundtrigger.enabled = false;
        soundtrigger.radius = noiseradius;
    }

    // Update is called once per frame
    void Update()
    {
        //Test execution
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Activating event");
            activateNoiseEvent();
        }
        */
        if (soundtrigger.enabled)
        {
            timer += Time.deltaTime;
            if(timer > .5)
            {
                timer = 0;
                soundtrigger.enabled = false;
            }
        }
    }

    public void activateNoiseEvent()
    {
        soundtrigger.enabled = true;
        
    }
}
