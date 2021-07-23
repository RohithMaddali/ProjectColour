using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_VoiceOverManager : MonoBehaviour
{
    private RBMove movement;
    uint idleVoxID;
    private bool toggle;
    [SerializeField] private float cooldown;
    private float waitTime = 8;
    void Start()
    {
        movement = GetComponent<RBMove>();
    }
    private void Update()
    {
        NotMoving();
    }
    void NotMoving() //Checks if player isn't moving or aiming 
    {
        if (movement.moveDir.z == 0 && movement.moveDir.x == 0 && !toggle && movement.moveCamActive)
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
        yield return new WaitForSeconds(25);
        toggle = false;
    }
}
