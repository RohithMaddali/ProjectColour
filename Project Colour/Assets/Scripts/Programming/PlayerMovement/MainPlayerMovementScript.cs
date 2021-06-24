using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace ZachFrench
{


    public class MainPlayerMovementScript : MonoBehaviour
    {
        //set up character controller
        //public CharacterController controller;
        public Transform cam;
        
        //Camera Related Vectors
        public float hori;
        public float verti;
        public Vector3 directionCam;
        public Vector3 camDir;
        
        //Rigidbody Additions
        public Rigidbody rb;
        public Vector3 fAndBMovementForce;
        public Vector3 lAndRMovementForce;
        public Vector3 moveDir;
        public Vector3 jumpForce;
        public float fAndBStrength = 10;
        public float lAndRStrength = 10;
        public float jumpStrength;
        public bool isGrounded;
        

        //set the walk speed
        public float speed = 4f;
        private float lastSpeed;
        public float maxSpeed = 20f;

        //jump and gravity
        public float jumpHeight = 4f;
        public float gravity = -9.8f;

        public float weight = 2f;

        //create smooth turning
        public float turnSmoothTime = 0.1f;

        float turnSmoothVelocity;

        //this is for GRAVITY
        public Vector3 velocity = Vector3.zero;

        //For BlueBounce
        public GameObject feet;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            //Cursor Lock to screen
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.lockState = CursorLockMode.Locked;
            
            
            
            hori = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            verti = Input.GetAxisRaw("Vertical") * Time.deltaTime;
            directionCam = new Vector3(hori, 0f, verti).normalized;

            if (directionCam.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(directionCam.x, directionCam.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //camDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward;
                //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            
            
            
            fAndBMovementForce = new Vector3(0, 0, fAndBStrength);
            lAndRMovementForce = new Vector3(lAndRStrength, 0, 0);
           
            jumpForce = new Vector3(0, jumpStrength, 0);
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(moveDir.normalized * fAndBStrength);
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

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-moveDir.normalized * -fAndBStrength);
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

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-lAndRMovementForce);
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

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(lAndRMovementForce);
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
            
            jumpForce = new Vector3(0, jumpStrength, 0);
            //check if the player is on the ground
            if (isGrounded == true && Input.GetKey(KeyCode.Space))
            {
                //jumping
                if (Input.GetButton("Jump"))
                {
                    rb.AddRelativeForce(jumpForce);
                    isGrounded = false;
                }
            }
        }

        private void OnCollisionStay(Collision other)
        {
            isGrounded = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //check if coliding with blue object
            if (collision.gameObject.GetComponent<Renderer>().material.color == Color.blue)
            {
                Debug.Log("Boing");
                rb.AddExplosionForce(200f, feet.transform.position, 1f);
            }
            //check if coliding with red object
            if (collision.gameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                Debug.Log("gotta go fast");
                speed *= 2f;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                Debug.Log("Reset speed");
                speed /= 2f;
            }
        }
    }
}
