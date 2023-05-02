using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonySpawner : MonoBehaviour
{
    public GameObject Tony;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Tony = GameObject.Find("Tony");
        Tony.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tony.SetActive(true);
        }
    }
}
