using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonMovement : MonoBehaviour
{
    //set up character controller
    public CharacterController controller;
    public Transform cam;

    //set the walk speed
    public float speed = 4f;
    //jump and gravity
    public float jumpHeight = 4f;
    public float gravity = -9.8f;
    public float weight = 2f;
    //create smooth turning
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    //this is for GRAVITY
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //check if the player is on the ground
        if (controller.isGrounded)
        {
            velocity.y = -1f;
            //jumping
            if (Input.GetButton("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
            }
        }

        //bring the player to the ground
        velocity.y += gravity * Time.deltaTime;

        controller.Move((velocity * weight) * Time.deltaTime);
    }
}
