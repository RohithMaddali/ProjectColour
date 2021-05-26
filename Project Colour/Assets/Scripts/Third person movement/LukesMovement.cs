using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LukesMovement : MonoBehaviour
{
    public CinemachineVirtualCamera aimCam;
    public CinemachineFreeLook moveCam;
    public CharacterAim aimScript;
    public bool moveCamActive;

    public CharacterController controller;
    public Transform cam;

    public float speed = 2f;
    public float maxSpeed = 15f;
    public float minSpeed = 2f;
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

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (speed < maxSpeed)
        {
            speed += acceleration *Time.deltaTime;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (!moveCamActive)
        {
            horizontal = 0;
        }
        
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);


        }
        else if(direction.magnitude < .1f)
        {
            speed = 2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Fire3"))
        {
            SwitchCam();
        }
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
