using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class player_vr_controls : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveInput;
    public SteamVR_Action_Vector2 inventoryButton;
    public SteamVR_Action_Vector2 interactionButton;
    public float speed = 1;

    void Start()
    {

    }



    void Update()
    {
        Vector3 direction = Player.instance.rightHand.transform.TransformDirection(new Vector3(moveInput.axis.x, 0, moveInput.axis.y));
        transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
    }

}
