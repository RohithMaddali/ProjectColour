﻿using System.Collections;
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
    public float aimSpeed;
    public float maxSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float boostSpeed;
    public float acceleration;
    public float jumpHeight;
    public float bounceHeight;
    public float accelBoost;
    public float bounceMultiplier;
    bool isBouncing;
    public bool boosted;
    public float gravity;
    public Vector3 fallVelocity;
    public Vector3 moveDir;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Vector3 bounceDir;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Material red;
    public Material blue;

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

        if (moveCamActive)
        {
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else if (speed > maxSpeed)
            {
                //reset down to new max speed if running then walking
                speed = maxSpeed;
            }

            if (controls.Gameplay.Move.ReadValue<Vector2>().magnitude < .9f && !boosted)
            {
                maxSpeed = walkSpeed;
            }
            else if (controls.Gameplay.Move.ReadValue<Vector2>().magnitude >= .9f && !boosted)
            {
                maxSpeed = runSpeed;
            }

            Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);


                Vector3 movement = new Vector3(move.x * 5f * Time.deltaTime, moveDir.y, move.y * 5f * Time.deltaTime);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            else if (direction.magnitude < .1f)
            {
                //reset speed when not moving
                speed = 2f;
            }

            if (controls.Gameplay.Move.ReadValue<Vector2>().magnitude > .1f)
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
        }
        else if (!moveCamActive)
        {
            //aimCam movement
            Vector3 aimMove = transform.right * move.x + transform.forward * move.y;
            rb.MovePosition(transform.position + (aimMove.normalized * aimSpeed * Time.deltaTime));
        }
        fallVelocity = rb.velocity; // get fall velocity because physics update is weird
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
        if (isGrounded && moveCamActive && !isBouncing)
        {
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Color colColor = collision.gameObject.GetComponent<MeshRenderer>().material.color;
        
        float mag = fallVelocity.magnitude * 2;
        if (!isBouncing)
        {
            bounceDir = moveDir;
        }
        

        if (colColor == Color.blue && fallVelocity.y <= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); //reset y velocity to 0 to establish base
            float bounceBack = Mathf.Max(mag * bounceMultiplier, bounceHeight); //take the higher of either base bounce or magnitude of fall
            rb.AddForce(new Vector3(0f, bounceBack, 0f), ForceMode.Impulse); //add y force
            Debug.Log("Boucne " + bounceBack);
            isBouncing = true;
        }
        else if(colColor == Color.blue && fallVelocity.y > 0 && !isBouncing)
        {
            
            rb.AddForce(new Vector3(bounceDir.x * -bounceHeight, bounceHeight, bounceDir.z * -bounceHeight), ForceMode.Impulse);
            isBouncing = true;
            Debug.Log("BOUNCE BACK");
        }
        else if (colColor == Color.blue && fallVelocity.y > 0 && isBouncing)
        {
            rb.AddForce(new Vector3(bounceDir.x * bounceHeight, bounceHeight, bounceDir.z * bounceHeight), ForceMode.Impulse);
            isBouncing = true;
            Debug.Log("BOUNCE BACK 2");
        }

        if (colColor == Color.red)
        {
            maxSpeed = boostSpeed;
            acceleration = accelBoost;
            boosted = true;
        }
        
        if(colColor != Color.blue &&
            colColor != Color.red)
        {
            boosted = false;
            isBouncing = false;
        }
        

    }

}
