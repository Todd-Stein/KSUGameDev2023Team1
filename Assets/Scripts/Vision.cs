using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField]
    GameObject tony;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Player"))
        {
            tony.GetComponent<Tony>().OnAlert(other);
        }
    }

    // Update to continuously provide location info of the player.
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Inside Trigger.");
        if (other.gameObject.CompareTag("Player"))
        {
            tony.GetComponent<Tony>().OnAlert(other);
        }
    }
}
