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
        //if(goggles == true){
        //  currentGoal = player's position;
        //  agent.destination = currentGoal.position;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tony.GetComponent<Tony>().OnAlert(other, 10);
            Debug.Log("Alerted");
        }
    }

    // Update to continuously provide location info of the player.
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Inside Trigger.");
        if (other.gameObject.CompareTag("Player"))
        {
            tony.GetComponent<Tony>().OnAlert(other, 15);
        }
    }
}
