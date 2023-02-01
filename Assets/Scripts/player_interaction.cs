using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_interaction : MonoBehaviour
{

    [SerializeField]
    float interactReach = 1.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactReach, 1 << LayerMask.NameToLayer("Interactables"));
            Debug.Log("E pressed");
            
            foreach (Collider collider in colliders)
            {
                collider.gameObject.GetComponent<interactable>().activate();
            }
        }
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, interactReach, 1 << LayerMask.NameToLayer("Interactables"));
        //Debug.Log("every object in OverlpaSphere:");
        foreach (Collider obj in objectsInRange)
        {
            Debug.Log(obj.name);
        }
    }
}
