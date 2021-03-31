using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZachFrench
{


    public class ColourManRb : MonoBehaviour
    {
        public Rigidbody rb;
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
            fAndBStrength = 50;
            lAndRStrength = 40;
            jumpStrength = 100;
            fAndBMovementForce = new Vector3(0, 0, fAndBStrength);
            lAndRMovementForce = new Vector3(lAndRStrength, 0, 0);
            jumpForce = new Vector3(0, jumpStrength, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddRelativeForce(fAndBMovementForce);
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
