using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ColourPowers : MonoBehaviour
{
    public bool isBlue;
    public bool isRed;
    public GameObject pairedObject;
    public GameObject repairedObject;
    [SerializeField] private Animator animator;

    public Renderer rend;
    public Renderer pairedRend;

    public float bounceHeight;
    public RBMove player;

    public float speedBoost = 50f;
    public bool repaired = false;
    bool hasGrown;
    bool isFixed;
    public Material grey;
    W_Interactables interactSound;

    // Start is called before the first frame update
    void Start()
    {
        interactSound = GetComponent<W_Interactables>();
        rend = GetComponent<Renderer>();
        pairedRend = pairedObject.GetComponent<Renderer>();
        player = FindObjectOfType<RBMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move this to ColourMover script so it happens when when changed instead of constantly checking

        if (animator != null && rend.material.color != Color.green && repairedObject == null)
        {
            animator.SetBool("Grow", false);
            if (hasGrown)
            {
                interactSound.Interactions("Plant");
                hasGrown = false;
            }
        }
        
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
        if (rend.material.color == Color.green && !repaired)
        {
            if (animator != null && pairedObject == null)
            {
                Debug.Log("Growwwww");
                if (!hasGrown)
                {
                    interactSound.Interactions("Plant");
                    hasGrown = true;
                }
                animator.SetBool("Grow", true);
            }
            else
            {
                SwitchActive();
                if (!isFixed)
                {
                    interactSound.Interactions("Bridge");
                    isFixed = true;
                }
                rend.material.color = Color.grey;
            }
        }
        if(rend.material.color != Color.green && repaired)
        {
            Debug.Log("LETS GOOOO");
            SwitchActive();
            rend.material.color = Color.green;
        }
    }

    void SwitchActive()
    {
        pairedRend.material.color = rend.material.color;
        pairedObject.SetActive(true);
        //animation can go here?
        gameObject.SetActive(false);
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
   
}
