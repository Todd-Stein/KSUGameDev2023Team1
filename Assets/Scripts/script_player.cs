using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class script_player : MonoBehaviour
{
    private PlayerInput controls;

    [SerializeField]
    private float playerSpeed = 5;

    private Vector2 movementDirection;

    private Vector2 cameraControl;

    private CharacterController playerInput;

    private float xRot, yRot;

    private bool hasJumped;
    private bool hasFired;

    public bool tonyVision;

    public GameObject tonyOverlay;

    [SerializeField]
    [Range(-200f, 200f)]
    private float maxPitch = 100.0f;
    [SerializeField]
    [Range(-200, 200f)]
    private float minPitch = -100.0f;

    private Camera cam;

    private void Awake()
    {
        controls = new PlayerInput();
        cam = GetComponentInChildren<Camera>();
        playerInput = GetComponent<CharacterController>();
        tonyOverlay = transform.GetChild(0).GetChild(0).gameObject;

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
        controls.Player.Camera.performed += CameraMovement;
        xRot = yRot = 0.0f;
    }

    

    // Update is called once per frame
    void Update()
    {
        tonyOverlay.SetActive(tonyVision);
        
        Vector3 velocity = Vector3.zero;
        velocity = (transform.forward*movementDirection.y)+(transform.right * movementDirection.x);
        velocity.Normalize();
        velocity *= playerSpeed;
        playerInput.Move(velocity*Time.deltaTime);
        //Vector3 nextRot = transform.eulerAngles;
        //nextRot.y += cameraControl.x;
        yRot += cameraControl.x;
        transform.rotation = Quaternion.Euler(0.0f, yRot, 0.0f);
        xRot += -cameraControl.y;
        xRot = Mathf.Clamp(xRot, minPitch, maxPitch);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0.0f);
        //tonyOverlay.transform.localPosition = cam.transform.forward * (cam.nearClipPlane + 1.0f);
        //UnityEngine.Debug.Log(cam.transform.forward);
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
    void CameraMovement(InputAction.CallbackContext ctx)
    {
        cameraControl = ctx.ReadValue<Vector2>();
    }
}
