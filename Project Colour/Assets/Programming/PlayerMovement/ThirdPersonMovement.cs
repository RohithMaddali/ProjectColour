using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AJ
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        
        public Transform cam;
        public float speed = 6f;

        private float lastSpeed;

        public float turnSmoothTime = 0.1f;

        private float turnSmoothVelocity;
        
        private CharacterController controller;

        //public float forwardSpeed;

        public float maxSpeed;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            speed = 10f;
            maxSpeed = 20f;
        }

        // Update is called once per frame
        
        
            
        
        // Update is called once per frame
        void Update()
        {
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
                //Increase speed over time
                if (speed < maxSpeed)
                {
                    speed += 0.9f * Time.deltaTime;
                }
                else if(Input.GetKeyDown(KeyCode.W))
                {
                    //Reset once key is released
                    if (speed != lastSpeed)
                    {
                        speed = 10;
                        lastSpeed = speed;
                    }
                }
            }
        }
    }
}

