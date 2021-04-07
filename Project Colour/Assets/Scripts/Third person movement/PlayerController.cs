using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //set up character controller
    CharacterController controller;

    //set the walk speed
    public float speed = 4;
    //jump and gravity
    public float jumpSpeed = 3;
    public float gravity = 9.8f;

    //make a private vector3 for movement direction use .zero to avoid null
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //get the character controller
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is on the ground
        if (controller.isGrounded)
        {
            //if on ground input rotation and movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //move character in that direction * by speed
            moveDirection *= speed;

            //jumping
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //bring the player to the ground
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
