using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AJ;

public class ConveyorBeltJ : MonoBehaviour
{
    public GameObject belt;
    public GameObject indicator;
    public float speed;
    public int maxSpeed;
    public Camera cam;
    public GameObject powerSwitch;
    public GameObject speedSwitch;
    public GameObject buttonHit;
    public bool beltOn;
    public Material LightMat;
    public Material greyMat;
    public bool isPowered;
    Rigidbody player;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        buttonHit = null;
    }

    private void Start()
    {
        player = GetComponent<Rigidbody>();
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

    void FixedUpdate()
    {
        if (beltOn)
        {
            //player.transform.position = Vector3.MoveTowards(player.transform.position, endpoint.position, currentSpeed * Time.deltaTime);
            //player.AddForce(pushDir * currentSpeed);
            Vector3 pos = player.position;
            player.position -= transform.forward * speed * Time.deltaTime;
            player.MovePosition(pos);
        }
    }

    void ChangeSpeed()
    {
        if (speed >= maxSpeed)
        {
            speed = 0;
        }
        else
        {
            speed += 5;
        }
    }

    void PowerSwitch()
    {
        if (beltOn)
        {
            powerSwitch.GetComponent<Animator>().SetBool("on", false);
            beltOn = false;
            isPowered = false;
            indicator.GetComponent<Renderer>().material = greyMat;
        }
        else
        {
            powerSwitch.GetComponent<Animator>().SetBool("on", true);
            beltOn = true;
            isPowered = false;
            indicator.GetComponent<Renderer>().material = LightMat;
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
}

