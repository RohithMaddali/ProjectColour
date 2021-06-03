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

    public float speed = 2f;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;
    public float aimSpeed = 3f;
    public float acceleration = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 velocity;
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
            //reset velocity when on ground so gravity stops increasing
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
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

            if(controls.Gameplay.Move.ReadValue<Vector2>().magnitude < .9f)
            {
                maxSpeed = 5f;
            }
            else if(controls.Gameplay.Move.ReadValue<Vector2>().magnitude >= .9f)
            {
                maxSpeed = 10f;
            }
            //move
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
}
