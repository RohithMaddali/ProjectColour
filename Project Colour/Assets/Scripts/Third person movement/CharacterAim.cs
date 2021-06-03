using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour
{
    PlayerControls controls;
    Vector2 rotate;

    public bool aimCam = false;
    Camera mainCam;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Camera.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Camera.canceled += ctx => rotate = Vector2.zero;
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        
        if (aimCam)
        {
            float mouseX = rotate.x * mouseSensitivity * Time.deltaTime;
            float mouseY = rotate.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}
