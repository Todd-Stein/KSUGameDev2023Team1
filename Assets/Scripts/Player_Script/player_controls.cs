using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class player_controls : MonoBehaviour
{
    public KeyCode gogglesKey = KeyCode.Mouse1;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode tossKey = KeyCode.Mouse0;

    private Camera playerCam;

    [SerializeField]
    private float interactDistance = 2.5f;

    /////////////////////////
    /// pickup variables
    private GameObject held;
    private Rigidbody heldRB;
    public bool isHolding = false;

    [SerializeField]
    [Tooltip("(Auto-assigned during Awake().) The position which a pickup will be held at in the FOV.")]
    private Transform holdPos;
    /////////////////////////

    [Tooltip("(Auto-assigned during Awake().) The layer that any interactable object is on.")]
    public LayerMask interactableLayer;
    [Tooltip("(Auto-assigned during Awake().) The layer that any pickupable object is on.")]
    public LayerMask pickupableLayer;

    private RaycastHit interactHit;
    private RaycastHit pickupHit;

    [SerializeField]
    [Tooltip("(Auto-assigned during Awake().) The Canvas Image which overlays a yellow tint when gogglesKey is pressed.")]
    private GameObject tonyVision;

    //throwing control

    [Tooltip("The force applied to thrown objects.")]
    public float throwForce = 500f;  //public in case of a future game mechanic that may alter it.

    private void Awake()
    {
        interactableLayer = LayerMask.GetMask("Interactables");
        pickupableLayer = LayerMask.GetMask("Pickup"); 
        if(holdPos == null)
            holdPos = GameObject.Find("HoldingPosition").GetComponent<Transform>();
        if(tonyVision== null)
        pickupableLayer = LayerMask.GetMask("Pickup");
        if(holdPos== null)
            holdPos = GameObject.Find("HoldingPosition").GetComponent<Transform>();
        tonyVision = GameObject.Find("TonyVision");
        tonyVision.SetActive(false);
        //playerCam = transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(gogglesKey))
        {
            tonyVision.SetActive(true);      
        }
        if (Input.GetKeyUp(gogglesKey))
        {
            tonyVision.SetActive(false);
        }
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
        if (Input.GetKeyDown(tossKey))
        {
            toss();
        }
    }

    void Interact()
    {
        Debug.Log("Interact key pressed.");

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * interactDistance, Color.green);

        if (Physics.Raycast(transform.position, playerCam.transform.forward, out interactHit, interactDistance, interactableLayer))
        {
            Debug.Log("Raycast hit object with Interactable Layer");

            //Debug.Log(interactHit.transform.gameObject.name);

            interactHit.collider.gameObject.GetComponent<interactable>().activate();
            return;
        }


        if (!isHolding)
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out pickupHit, interactDistance, pickupableLayer))
            {
                Debug.Log("Raycast hit object with Pickupable Layer");

                if (pickupHit.collider.gameObject.GetComponent<Rigidbody>() != null)
                {
                    held = pickupHit.collider.gameObject;
                    heldRB = pickupHit.collider.gameObject.GetComponent<Rigidbody>();
                    heldRB.isKinematic = true;
                    held.transform.position = holdPos.position;
                    heldRB.transform.parent = holdPos.transform;

                    Debug.Log("Player is now holding object");
                    isHolding = true;
                    held.GetComponentInChildren<TextMeshPopup>().destroyMe();

                    held.GetComponent<DistractItem>().thrown = true;

                    Physics.IgnoreCollision(held.GetComponent<Collider>(), GetComponent<Collider>(), true);
                }              
            }
        }

        else
            Debug.Log("No interactable within range");
    }

    void toss()
    {
        Debug.Log("Toss key pressed. Is player holding something: " + isHolding);

        if (isHolding)
        {
            Debug.Log("Tossing!");

            Physics.IgnoreCollision(held.GetComponent<Collider>(), GetComponent<Collider>(), false);

            heldRB.isKinematic = false;
            held.transform.parent = null;
            heldRB.AddForce(transform.forward * throwForce);
            held = null;
            isHolding = false;
        }
    }
}
