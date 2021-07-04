using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using AJ;
using ZachFrench;
using UnityEngine;

public class ColourPowers : MonoBehaviour
{
    public bool isBlue;
    public bool isRed;
    public GameObject brokenObject;
    public GameObject repairedObject;


    public Renderer rend;

    public float bounceHeight;
    public RBMove player;

    public int repairTime;
    public float speedBoost = 50f;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        player = FindObjectOfType<RBMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move this to ColourMover script so it happens when when changed instead of constantly checking

        
        if (rend.material.color == Color.red)
        {
            isBlue = false;
            isRed = true;
        }
        if (rend.material.color == Color.blue)
        {
            isBlue = true;
            isRed = false;
        }
        if (rend.material.color == Color.green && repairedObject != null)
        {
                StartCoroutine(repair());
        }
    }

    //      Put this in charactercontroller to check platforms and make them work

    /*private void OnCollisionEnter(Collision collision)
    {
        if (isBlue && collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb.velocity.y <= -9f)
            {
                rb.velocity = new Vector3(0f, -2f * rb.velocity.y, 0f);
                Debug.Log(rb.velocity.y + " fall on");
            }
            else if (rb.velocity.y > -9f && rb.velocity.y < .5f)
            {
                rb.velocity = new Vector3(0f, bounceHeight, 0f);
                //rb.AddForce(0f, bounceHeight, 0f, ForceMode.Impulse);
                Debug.Log(rb.velocity.y + " walk on");
            }
            else if (rb.velocity.y >= .5f)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.AddForce(-100f * rb.velocity.x, bounceHeight, -100f * rb.velocity.z, ForceMode.Impulse);
                Debug.Log(rb.velocity.y + " BBBBOUNCE BACK");
            }


        }

        if(isRed && collision.gameObject.CompareTag("Player"))
        {
            player.maxSpeed = player.boostSpeed;
            player.acceleration = player.accelBoost;
            player.boosted = true;
        }
    }

    //      resets acceleration when leaving platform

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<MainPlayerMovementScript>() != null)
        {
            collision.gameObject.GetComponent<MainPlayerMovementScript>().fAndBStrength = 5;
        }
    }*/
   
    IEnumerator repair()
    {
        brokenObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(repairTime);
        repairedObject.SetActive(true);
    }
}
