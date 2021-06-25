using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class RBMove : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public Rigidbody rb;
    
    public CinemachineFreeLook moveCam;
    public CinemachineVirtualCamera aimCam;
    public CharacterAim aimScript;
    public Transform cam;
    public bool moveCamActive;

    public float speed;
    private float maxSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float boostSpeed;
    public float baseAcceleration;
    public float jumpHeight;
    public float accelBoost;
    public float bounceHeight;
    private bool boosted;
    public float gravity;

    public Vector3 moveDir;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    public bool isGrounded;


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.CamChange.performed += ctx => SwitchCam();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void Update()
    {
        moveDir = new Vector3(move.x, move.y);
        if (!isGrounded)
        {
            rb.AddForce(0f, gravity * 2f, 0f);
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        
        Vector3 movement = new Vector3(move.x * 5f * Time.deltaTime, moveDir.y, move.y * 5f * Time.deltaTime);
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        if(controls.Gameplay.Move.ReadValue<Vector2>().magnitude > .1f)
        {
            rb.MovePosition(transform.position + (moveDir.normalized * speed * Time.deltaTime));
        }
        

        if (controls.Gameplay.Move.ReadValue<Vector2>().magnitude < .9f && !boosted)
        {
            maxSpeed = walkSpeed;
        }
        else if (controls.Gameplay.Move.ReadValue<Vector2>().magnitude >= .9f && !boosted)
        {
            maxSpeed = runSpeed;
        }

        rb.AddForce(0f, gravity, 0f);


    }

    private void SwitchCam()
    {
        if (moveCamActive)
        {
            moveCam.Priority = 0;
            aimCam.Priority = 1;
            aimScript.aimCam = true;
        }
        else
        {
            moveCam.Priority = 1;
            aimCam.Priority = 0;
            aimScript.aimCam = false;
        }
        moveCamActive = !moveCamActive;
    }
    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
            Debug.Log("WEE");
        }
    }
}
