using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controls : MonoBehaviour
{
    public KeyCode enableGoggles = KeyCode.Mouse1;
    public KeyCode interact = KeyCode.E;
    public KeyCode toss = KeyCode.Mouse0;

    private Camera playerCam;

    public GameObject held;


    [SerializeField]
    private float interactDistance = 1.5f;

    public LayerMask interactableLayer;
    public LayerMask pickupableLayer;

    private RaycastHit interactHit;
    private RaycastHit pickupHit;

    [SerializeField]
    private GameObject tonyVision;

    //throwing control

    private void Awake()
    {
        interactableLayer = LayerMask.GetMask("Interactables");
        pickupableLayer = LayerMask.GetMask("Pickup");
        playerCam = transform.GetChild(0).GetChild(0).GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(enableGoggles))
        {
            tonyVision.SetActive(true);
           
        }
        if (Input.GetKeyUp(enableGoggles))
        {
            tonyVision.SetActive(false);
        }
        if (Input.GetKeyDown(interact))
        {
            Interact();
        }
    }
    void Interact()
    {
        if(Physics.Raycast(transform.position, transform.forward, out interactHit, interactDistance, interactableLayer))
        {
            interactHit.collider.gameObject.GetComponent<interactable>().activate();
            return;
        }
        if(Physics.Raycast(transform.position, transform.forward, out pickupHit, interactDistance, pickupableLayer))
        {
            if(pickupHit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                pickupHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            pickupHit.collider.gameObject.transform.position = held.transform.position;
            pickupHit.collider.gameObject.transform.parent = playerCam.gameObject.transform;
        }
    }
}
