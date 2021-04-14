using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ZachFrench
{


    public class ColourManRb : MonoBehaviour
    {
        public Rigidbody rb;
        public CinemachineFreeLook CinemachineFreeLook;
        public Quaternion cameraAngle;
        public Vector3 moveCamera;
        public Vector3 fAndBMovementForce;
        public Vector3 lAndRMovementForce;
        public Vector3 jumpForce;
        public float fAndBStrength;
        public float lAndRStrength;
        public float jumpStrength;
        public bool isGrounded;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            fAndBStrength = 10;
            lAndRStrength = 40;
            jumpStrength = 100;
        }

        // Update is called once per frame
        void Update()
        {
            fAndBMovementForce = new Vector3(0, 0, fAndBStrength);
            lAndRMovementForce = new Vector3(lAndRStrength, 0, 0);
            
            //attempt at movement based around camera
            cameraAngle.eulerAngles = CinemachineFreeLook.transform.position;
            moveCamera = new Vector3(cameraAngle.x, 0, -cameraAngle.z);
            
            jumpForce = new Vector3(0, jumpStrength, 0);
            
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(moveCamera.normalized * fAndBStrength);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddRelativeForce(-fAndBMovementForce);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddRelativeForce(-lAndRMovementForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddRelativeForce(lAndRMovementForce);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddRelativeForce(jumpForce);
                isGrounded = false;
            }
        }

        private void OnCollisionStay(Collision other)
        {
            isGrounded = true;
        }
    }
}
