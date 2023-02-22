using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    bool goggs = true;

    public GameObject curHitObj;

    public GameObject debug;

    public float radius;
    public float distance;
    public LayerMask layer;

    private Vector3 origin;
    private Vector3 dir;
    private float curHitDis;


    [SerializeField]
    GameObject tony;

    private void FixedUpdate()
    {
        origin = transform.position;
        dir = transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin, radius, dir, out hit, distance, layer, QueryTriggerInteraction.UseGlobal))
        {
            curHitObj = hit.transform.gameObject;
            curHitDis = hit.distance;
        }
        else
        {
            curHitDis = distance;
            curHitObj = null;
        }
        debug.transform.position = new Vector3(transform.position.x, transform.position.y, curHitDis);
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
