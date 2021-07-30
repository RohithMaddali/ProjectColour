using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBeltJ : MonoBehaviour
{
    public GameObject belt;
    public GameObject indicator;
    public float speed;
    public float visualScalarSpeed;
    public int maxSpeed;
    public Camera cam;
    public GameObject powerSwitch;
    public GameObject speedSwitch;
    public GameObject buttonHit;
    public bool beltOn;
    public Material LightMat;
    public Material greyMat;
    public Renderer beltMat;
    Rigidbody player;
    W_ConveyerBeltAudio conveyAudio;
    bool playOnce;

    public float currentScroll;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        buttonHit = null;
    }

    private void Start()
    {
        player = GetComponent<Rigidbody>();
        conveyAudio = GetComponent<W_ConveyerBeltAudio>();
        beltMat = GetComponent<Renderer>();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (CheckForButton())
    //        {
    //            if (buttonHit == powerSwitch)
    //            {
    //                PowerSwitch();
    //            }
    //            else if (buttonHit == speedSwitch)
    //            {
    //                ChangeSpeed();
    //            }
    //        }
    //    }
    //}

    void FixedUpdate()
    {

        if (beltOn && beltMat.material.color != Color.green)
        {

            //player.transform.position = Vector3.MoveTowards(player.transform.position, endpoint.position, currentSpeed * Time.deltaTime);
            //player.AddForce(pushDir * currentSpeed);
            Vector3 pos = player.position;
            player.position -= transform.forward * speed * Time.deltaTime;
            player.MovePosition(pos);
            currentScroll = currentScroll + visualScalarSpeed * speed * Time.deltaTime;
            GetComponent<Renderer>().material.SetVector("_Offset", new Vector4(currentScroll, 0, 0, 0));
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

    public void PowerSwitch()
    {
        if (beltOn)
        {
            conveyAudio.ConveyerBeltSound();
            powerSwitch.GetComponent<Animator>().SetBool("on", false);
            beltOn = false;
            indicator.GetComponent<Renderer>().material = greyMat;        
        }
        else 
        {
            conveyAudio.ConveyerBeltSound();
            powerSwitch.GetComponent<Animator>().SetBool("on", true);
            beltOn = true;
            indicator.GetComponent<Renderer>().material = LightMat;
        }
    }

    //bool CheckForButton()
    //{
    //    bool rayHitButton = false;

    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, 10))
    //    {
    //        if (hit.collider.gameObject == powerSwitch)
    //        {
    //            rayHitButton = true;
    //            buttonHit = powerSwitch;
    //            Debug.Log("power");
    //        }
    //        else if (hit.collider.gameObject == speedSwitch)
    //        {
    //            rayHitButton = true;
    //            buttonHit = speedSwitch;
    //            Debug.Log("speed");
    //        }
    //    }
    //    return rayHitButton;
    //}
}

