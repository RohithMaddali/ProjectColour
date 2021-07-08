using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_VoiceOverManager : MonoBehaviour
{
    private Rigidbody rb;
    private RBMove movement;
    uint idleVoxID;
    private bool toggle;
    [SerializeField] private float cooldown;
    [SerializeField] private float waitTime;
    void Start()
    {
        movement = GetComponent<RBMove>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        NotMoving();
    }
    void NotMoving() //Checks if player isn't moving
    {
        if (movement.moveDir.z == 0 && movement.moveDir.x == 0 && !toggle)
        {
            cooldown += Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }
        if (cooldown >= waitTime)
        {
            if (!toggle)
            {
                StartCoroutine(PlayIdleVox());
                cooldown = 0;
                toggle = true;
            }

        }
    }
    IEnumerator PlayIdleVox()
    {
        idleVoxID = AkSoundEngine.PostEvent("ev_IdleVox",gameObject);
        yield return new WaitForSeconds(10);
        toggle = false;
    }
    void BlueAreaVox()
    {
        //play blue area vox 
    }
}
