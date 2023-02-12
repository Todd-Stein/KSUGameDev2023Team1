using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSound : MonoBehaviour
{

    private SoundEvent SE;
    // Start is called before the first frame update
    void Start()
    {
        SE = gameObject.GetComponentInChildren<SoundEvent>();
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
            SE.activateNoiseEvent();
        }
    }
}
