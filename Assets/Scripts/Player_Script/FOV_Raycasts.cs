using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_Raycasts : MonoBehaviour
{
    /// this script uses raycasts to interact with interactables in the world.
    /// if the raycasts hit something on the interactable layer then that object will enable its attached billboard prefab
    /// the billboard prefab is the second part of this system. it needs to be attached to every interactable object
    /// 

    [SerializeField]
    [Tooltip("How close a player is to an interactable before the billboard activates.")]
    private float range = 7f;

    [SerializeField]
    [Tooltip("The degrees outward from the reticle that will be scanned for interactables.")]
    private float coneAngle = 30f;

    private int rayCount = 10;

    LayerMask interactableLayer;
    LayerMask pickupableLayer;

    private Camera playerCam;

    //public LayerMask layerMask;
    private void Awake()
    {
        interactableLayer = LayerMask.GetMask("Interactables");
        pickupableLayer = LayerMask.GetMask("Pickup");

        playerCam = transform.GetChild(0).GetChild(0).GetComponent<Camera>();
    }
    private void Update()
    {
        float halfCone = coneAngle / 2f;
        float angleStep = coneAngle / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = playerCam.transform.eulerAngles.y - halfCone + angleStep * i;
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * playerCam.transform.forward;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, pickupableLayer))
            {
                // Do something with the hit object
                Debug.Log("Cone Raycast Hit object: " + hit.collider.gameObject.name);
            }

            if (Physics.Raycast(ray, out hit, range, interactableLayer))
            {
                // Do something with the hit object
                Debug.Log("Cone Raycast Hit object: " + hit.collider.gameObject.name);
            }
        }
    }
}
