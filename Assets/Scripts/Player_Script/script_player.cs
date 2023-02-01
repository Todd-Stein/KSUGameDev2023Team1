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
    [SerializeField]
    private float playerSprintSpeed = 10;

    private Vector2 movementDirection;

    private Vector2 cameraControl;

    private CharacterController playerInput;


    private float xRot, yRot;

    private bool hasJumped;
    private bool hasFired;
    private bool hasSprinted;
    private bool exausted;

    public bool tonyVision;
    public bool toggleDash;

    public GameObject tonyOverlay;

    [SerializeField]
    private float sprintTimeCurrent = 0.0f;
    [SerializeField]
    private float sprintTimeTotal = 3.0f;

    [SerializeField]
    [Range(-200f, 200f)]
    private float maxPitch = 100.0f;
    [SerializeField]
    [Range(-200, 200f)]
    private float minPitch = -100.0f;

    private Camera cam;

    private float bobTimer = 0.0f;
    private float camHeight;

    private void Awake()
    {
        controls = new PlayerInput();
        cam = GetComponentInChildren<Camera>();
        playerInput = GetComponent<CharacterController>();
        tonyOverlay = transform.GetChild(0).GetChild(0).gameObject;
        camHeight = cam.transform.localPosition.y;

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
        controls.Player.Sprint.started += Sprint;
        controls.Player.Sprint.canceled += Sprint;
        xRot = yRot = 0.0f;
    }

    

    // Update is called once per frame
    void Update()
    {

        Debug.Log(bobTimer);
        tonyOverlay.SetActive(tonyVision);
        
        Vector3 velocity = Vector3.zero;
        velocity = (transform.forward*movementDirection.y)+(transform.right * movementDirection.x);
        velocity.Normalize();
        if (velocity.magnitude != 0.0f)
        {
            if (hasSprinted && (sprintTimeCurrent <= sprintTimeTotal) && !exausted)
            {
                velocity *= playerSprintSpeed;
                sprintTimeCurrent += Time.deltaTime;
            }
            else
            {
                velocity *= playerSpeed;
            }
            if(sprintTimeCurrent >= sprintTimeTotal)
            {
                exausted = true;
                sprintTimeCurrent = sprintTimeTotal;
            }
            Vector3 newHeadPos = Vector3.zero;
            newHeadPos.y = (0.08f)*Mathf.Sin(bobTimer*10)+camHeight;
            cam.transform.localPosition = newHeadPos;
            bobTimer += Time.deltaTime;
            playerInput.Move(velocity * Time.deltaTime);

        }
        else
        {
            if (sprintTimeCurrent >= 0.0)
            {
                sprintTimeCurrent -= Time.deltaTime;
            }
            else
            {
                sprintTimeCurrent = 0.0f;
                exausted = false;
            }
            bobTimer = 0.0f;
        }
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
    void Sprint(InputAction.CallbackContext ctx)
    {
        if (toggleDash)
            hasSprinted = !hasSprinted;
        else
            hasSprinted = ctx.ReadValueAsButton();
    }
}
