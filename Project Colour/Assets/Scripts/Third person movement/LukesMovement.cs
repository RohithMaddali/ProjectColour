using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class LukesMovement : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;

    public CinemachineVirtualCamera aimCam;
    public CinemachineFreeLook moveCam;
    public CharacterAim aimScript;
    public bool moveCamActive;

    public CharacterController controller;
    public Transform cam;

    public Material red;
    public Material blue;

    public float speed = 2f;
    public float maxSpeed = 10f;
    public float walkSpeed = 2f;
    public float runSpeed = 10f;
    public float boostSpeed = 20f;
    public float aimSpeed = 3f;
    private float acceleration;
    public float accelBase = 5f;
    public float accelBoost = 100f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    private bool boosted = false;
    public Vector3 moveDir;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 velocity;
    public bool isGrounded;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.CamChange.performed += ctx => SwitchCam();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }

    void Jump()
    {
        if (isGrounded && moveCamActive)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void Update()
    {
        if (moveCamActive)
        {
            //check if grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            //increase speed over time up to max speed 
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else if(speed > maxSpeed)
            {
                //reset down to new max speed if running then walking
                speed = maxSpeed;
            }
            //get direction of movement
            Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;

            if(controls.Gameplay.Move.ReadValue<Vector2>().magnitude < .9f && !boosted)
            {
                maxSpeed = walkSpeed;
            }
            else if(controls.Gameplay.Move.ReadValue<Vector2>().magnitude >= .9f && !boosted)
            {
                maxSpeed = runSpeed;
            }
            //move
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else if (direction.magnitude < .1f)
            {
                //reset speed when not moving
                speed = 2f;
            }
        }
        else if (!moveCamActive)
        {
            //aimCam movement
            Vector3 aimMove = transform.right * move.x + transform.forward * move.y;

            controller.Move(aimMove * aimSpeed * Time.deltaTime);
        }
        //apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial == blue 
            && Mathf.Abs(velocity.y) > 10.1f)
        {
            Debug.Log("velocity.y = " + velocity.y);
            velocity.y = -velocity.y;
        }
        else if (hit.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial == blue 
            && Mathf.Abs(velocity.y) <= 10.1f)
        {
            Debug.Log("bounce");
            velocity.y = 10f;
        }
        else if (isGrounded && velocity.y < 0)
        {
            //reset velocity when on ground so gravity stops increasing
            velocity.y = -2f;
        }

        if (hit.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial == red)
        {
            maxSpeed = boostSpeed;
            acceleration = accelBoost;
            boosted = true;
        }
        if (hit.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial != red && 
            hit.gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterial != blue)
        {
            acceleration = accelBase;
            boosted = false;
        }
    }
}
