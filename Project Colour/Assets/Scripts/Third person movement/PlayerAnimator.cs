﻿using System.Collections;
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
            velocityZ = rbmove.speed / 8;
            if (rbmove.moveDir == Vector3.zero)
            {
                velocityZ = 0;
            }
            animator.SetFloat("VelocityX", velocityX);
            animator.SetFloat("VelocityZ", velocityZ);
        }
    }
}
