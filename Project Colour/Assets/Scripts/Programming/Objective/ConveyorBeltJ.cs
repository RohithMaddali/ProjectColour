using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AJ;

public class ConveyorBeltJ : MonoBehaviour
{
    public GameObject belt;
    public GameObject indicator;
    public Transform endpoint;
    public int currentSpeed;
    public int maxSpeed;
    public Camera cam;
    public GameObject powerSwitch;
    public GameObject speedSwitch;
    public GameObject buttonHit;
    public bool beltOn;
    public Material redMat;
    public Material greyMat;
    public bool isPowered;
    Vector3 pushDir;
    Rigidbody player;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        buttonHit = null;
    }

    private void Start()
    {
        player = FindObjectOfType<Rigidbody>();
        pushDir = (endpoint.transform.position - transform.position).normalized;
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
            indicator.GetComponent<Renderer>().material = redMat;
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

    void OnTriggerStay(Collider col)
    {
        if (beltOn)
        {
            //player.transform.position = Vector3.MoveTowards(player.transform.position, endpoint.position, currentSpeed * Time.deltaTime);
            player.AddForce(pushDir * currentSpeed);
        }
    }

}

