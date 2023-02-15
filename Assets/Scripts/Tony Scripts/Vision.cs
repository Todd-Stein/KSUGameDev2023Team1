using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    bool goggs = true;

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
        //alerted if no goggles
        //hunting if goggles
        if (other.gameObject.CompareTag("Player") && !goggs)
        {
            tony.GetComponent<Tony>().OnAlert(other, 10);
            Debug.Log("Alerted");
        }

        if(other.gameObject.CompareTag("Player") && goggs)
        {
            tony.GetComponent<Tony>().OnTheHunt(other);
            Debug.Log("Hunting");
        }
    }

    // Update to continuously provide location info of the player.
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Inside Trigger.");
        if (other.gameObject.CompareTag("Player"))
        {
            tony.GetComponent<Tony>().OnAlert(other, 1); // Sets Tony's destination to player and increases aggro by 1
        }
    }
}
