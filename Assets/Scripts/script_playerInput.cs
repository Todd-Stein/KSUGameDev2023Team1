using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class script_playerInput : MonoBehaviour
{
    private PlayerInput controls;

    private Vector2 movementDirection;

    private bool hasJumped;
    private bool hasFired;

    private void Awake()
    {
        controls = new PlayerInput();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls.Player.Movement.performed += Move;
        controls.Player.Jump.started += Jump;
        controls.Player.Jump.performed += Jump;
        controls.Player.Fire.started += Fire;
        controls.Player.Fire.performed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Jump(InputAction.CallbackContext ctx)
    {
        hasJumped = ctx.ReadValueAsButton();
    }
    void Fire(InputAction.CallbackContext ctx)
    {
        hasFired = ctx.ReadValueAsButton();
    }
    void Move(InputAction.CallbackContext ctx)
    {
        movementDirection = ctx.ReadValue<Vector2>();
    }

}
