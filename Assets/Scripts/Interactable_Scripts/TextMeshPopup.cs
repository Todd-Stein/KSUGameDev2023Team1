using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshPopup : MonoBehaviour
{
    /// This script controls 3 functions of the Pop-up text above game objects.
    /// 1.) The TextMesh game object auto-rotates to always face the player.
    /// 2.) The MeshRenderer turns on and off dependent on the distance from the player.
    /// 3.) The Text in the TextMeshPro-Text component is automatacally set to the name of the Parent gameobject.

    private Transform thisCanvas;
    private MeshRenderer thisMeshRend;
    private Transform pCam;
    private TextMeshPro textMesh;

    [SerializeField]
    [Tooltip("How close the player should be before the floating text will appear.")]
    private float activationDistance = 10f;

    void Start()
    {
        //get the transforms of this gameobject and the maincamera for use in LateUpdate().
        pCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        thisCanvas = GetComponent<Transform>();

        //get the mesh renderer and disable it at start
        thisMeshRend = GetComponent<MeshRenderer>();
        thisMeshRend.enabled = false;

        //get the textmesh component, get the parent of this,
        //and set the textmesh text to the parent name.
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = gameObject.transform.parent.name;
    }

    public void destroyMe()
    {
        GameObject.Destroy(gameObject);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, pCam.position);   

        if (distance <= activationDistance)
        {
            thisMeshRend.enabled = true;
        }
        else
        {
            thisMeshRend.enabled = false;
        }
    }

    void LateUpdate()
    {     
        thisCanvas.rotation = Quaternion.LookRotation((thisCanvas.position - pCam.position).normalized);
    }
}
