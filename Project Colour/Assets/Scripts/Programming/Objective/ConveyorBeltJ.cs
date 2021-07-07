using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AJ;

public class ConveyorBeltJ : MonoBehaviour
{
    public GameObject belt;
    public Transform endpoint;
    public int currentSpeed;
    public int maxSpeed;
    public Camera cam;
    public GameObject powerSwitch;
    public GameObject speedSwitch;
    public GameObject buttonHit;
    bool beltOn;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        buttonHit = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CheckForButton())
            {
                if (buttonHit == powerSwitch)
                {
                    PowerSwitch();
                }
                else if (buttonHit == speedSwitch)
                {
                    ChangeSpeed();
                }
            }
        }
    }

    void ChangeSpeed()
    {
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed++;
        }
    }

    void PowerSwitch()
    {
        if (beltOn)
        {
            beltOn = false;
        }
        else
        {
            beltOn = true;
        }
    }

    bool CheckForButton()
    {
        bool rayHitButton = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.gameObject == powerSwitch)
            {
                rayHitButton = true;
                buttonHit = powerSwitch;
                Debug.Log("power");
            }
            else if (hit.collider.gameObject == speedSwitch)
            {
                rayHitButton = true;
                buttonHit = speedSwitch;
                Debug.Log("speed");
            }
        }
        return rayHitButton;
    }

    void OnTriggerStay(Collider other)
    {
        if (beltOn)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, currentSpeed * Time.deltaTime);
        }
    }

}

