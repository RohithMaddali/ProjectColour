using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AJ
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        public Transform cam;
        public float speed = 10f;
        public float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        private CharacterController controller;
        private float lastSpeed;
        public float maxSpeed = 20f;

        // Start is called before the first frame update

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

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
        }
    }
}
