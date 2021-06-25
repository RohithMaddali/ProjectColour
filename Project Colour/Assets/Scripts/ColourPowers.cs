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
    public GameObject repairedObject;


    public Renderer rend;

    public float bounceHeight;
    public RBMove player;

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
            Vector3 pos = gameObject.transform.position;
            Quaternion rot = gameObject.transform.rotation;
            Instantiate(repairedObject,pos, rot);
            Destroy(gameObject);
        }
    }

    //      Put this in charactercontroller to check platforms and make them work

    private void OnCollisionEnter(Collision collision)
    {
        if (isBlue && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0f, bounceHeight, 0f,ForceMode.Impulse);
            Debug.Log(collision.gameObject);
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
    }
}
