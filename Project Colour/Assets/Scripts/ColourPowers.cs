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

    public float bounceHeight = 50f;

    public float speedBoost = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
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

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ColourPowers>() != null
            && collision.gameObject.GetComponent<ColourPowers>().isRed
            && fAndBStrength <= fAndBStrengthMax)
        {
            fAndBStrength *= 2;
        }
        if (collision.gameObject.GetComponent<ColourPowers>() != null
            && collision.gameObject.GetComponent<ColourPowers>().isBlue)
        {
            rb.AddForce(Vector3.up * jumpStrength * 2);
        }
    }*/

    //      resets acceleration when leaving platform

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<thirdPersonMovement>() != null)
        {
            collision.gameObject.GetComponent<thirdPersonMovement>().fAndBStrength = 5;
        }
    }
}
