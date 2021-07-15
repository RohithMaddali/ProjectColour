using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerAnimator : MonoBehaviour
    {
        public Animator animator;
        RBMove rbmove;
        PlayerControls controls;
        float velocityX;
        public float velocityZ;
        public bool isAiming;
        public bool isFalling;
        public bool isJumping;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            controls = new PlayerControls();
            rbmove = GetComponentInParent<RBMove>();
        }

        void OnEnable()
        {
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }

        public void Update()
        {
            isAiming = !rbmove.moveCamActive;
            velocityZ = rbmove.speed / 8;
            if (rbmove.moveDir == Vector3.zero)
            {
                velocityZ = 0;
            }

            if (!rbmove.isGrounded)
            {
                if (rbmove.fallVelocity.y > 0.5f)
                {
                    isJumping = true;
                }
                else
                {
                    isJumping = false;
                    isFalling = true;
                }
            }
            else
            {
                isJumping = false;
                isFalling = false;
            }

            animator.SetFloat("VelocityX", velocityX);
            animator.SetFloat("VelocityZ", velocityZ);
            animator.SetBool("IsAiming", isAiming);
            animator.SetBool("IsFalling", isFalling);
            animator.SetBool("IsJumping", isJumping);
        }
    }
}
