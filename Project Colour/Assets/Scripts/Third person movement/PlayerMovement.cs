using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AJ
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform cam;
        public float speed = 10f;
        public float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        private CharacterController controller;
        private float lastSpeed;
        public float jumpHeight = 3f;
        public float maxSpeed = 20f;
        Vector3 Velocity; 
        bool isGrounded = false;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        
        // Start is called before the first frame update

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            //Mouse cursor locked to screen
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
            
            
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && Velocity.y < 0)
            {
                Velocity.y = -2f;
            }
            
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal,0f, vertical).normalized;

            if (direction.magnitude >= 0.01f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f );
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if(Input.GetKey(KeyCode.W))
            {
                if (speed <= maxSpeed)
                {
                    speed += 0.5f * Time.deltaTime;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    //Reset once key is released 
                    if (speed >= lastSpeed)
                    {
                        speed = 10;
                        lastSpeed = speed;
                    }
                }
            }
            
            if(Input.GetKey(KeyCode.D))
            {
                if (speed <= maxSpeed)
                {
                    speed += 0.5f * Time.deltaTime;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    //Reset once key is released 
                    if (speed >= lastSpeed)
                    {
                        speed = 10;
                        lastSpeed = speed;
                    }
                }
            }
            
            if(Input.GetKey(KeyCode.S))
            {
                if (speed <= maxSpeed)
                {
                    speed += 0.5f * Time.deltaTime;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //Reset once key is released 
                    if (speed >= lastSpeed)
                    {
                        speed = 10;
                        lastSpeed = speed;
                    }
                }
            }
            
            if(Input.GetKey(KeyCode.A))
            {
                if (speed <= maxSpeed)
                {
                    speed += 0.5f * Time.deltaTime;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    //Reset once key is released 
                    if (speed >= lastSpeed)
                    {
                        speed = 10;
                        lastSpeed = speed;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            
        }
    }
}
