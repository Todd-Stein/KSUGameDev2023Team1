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
    private Goggles goggles;

    [SerializeField]
    [Tooltip("The reach of the player's interaction range.")]
    private float interactDistance = 2.5f;

    /////////////////////////
    /// pickup variables
    private GameObject held;
    private Rigidbody heldRB;
    private bool isHolding = false;

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

        //find game manager
        goggles = GameObject.Find("GameManager").GetComponent<Goggles>();

        //playerCam = transform.GetChild(0).GetChild(0).GetComponent<Camera>();
        playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(gogglesKey))
        {
            //tonyVision.SetActive(true);
            goggles.Activate();
        }
        if (Input.GetKeyUp(gogglesKey))
        {
            //tonyVision.SetActive(false);
            goggles.Disable();
        }
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
        if (Input.GetKeyDown(tossKey))
        {
            Toss();
        }
    }

    void Interact()
    {
        Debug.Log("Interact key pressed.");

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * interactDistance, Color.green);

        //This raycast will only hit objects on the interactableLayer. If it hits it will call that gameobject's "activate()" method.
        if (Physics.Raycast(transform.position, playerCam.transform.forward, out interactHit, interactDistance, interactableLayer))
        {
            Debug.Log("Raycast hit object with Interactable Layer");

            interactHit.collider.gameObject.GetComponent<interactable>().Activate();
            return;
        }

        //This raycast will only hit objects on the pickupableLayer. If it hits, it will send that gameobject to the pickup() method.
        if (!isHolding)
        {
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out pickupHit, interactDistance, pickupableLayer))
            {
                Debug.Log("Raycast hit object with Pickupable Layer");

                if (pickupHit.collider.gameObject.GetComponent<Rigidbody>() != null)
                    Pickup(pickupHit.collider.gameObject);                                           
            }
        }

        else
            Debug.Log("No interactable within the set interactDistance.");
    }

    public void Pickup(GameObject obj)
    {
        held = obj;
        heldRB = held.GetComponent<Rigidbody>();
        heldRB.isKinematic = true;
        held.transform.position = holdPos.position;
        heldRB.transform.parent = holdPos.transform;

        Debug.Log("Player is now holding object");

        isHolding = true;
        if (held.GetComponentInChildren<TextMeshPopup>() != null)
            held.GetComponentInChildren<TextMeshPopup>().destroyMe();

        Physics.IgnoreCollision(held.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    void Toss()
    {
        Debug.Log("Toss key pressed. Is player holding something: " + isHolding);

        if (isHolding)
        {
            Debug.Log("Tossing!");

            if (held.GetComponent<DistractItem>() != null)
                held.GetComponent<DistractItem>().thrown = true;

            Physics.IgnoreCollision(held.GetComponent<Collider>(), GetComponent<Collider>(), false);

            heldRB.isKinematic = false;
            held.transform.parent = null;
            heldRB.AddForce(transform.forward * throwForce);
            held = null;
            isHolding = false;
        }
    }
}
