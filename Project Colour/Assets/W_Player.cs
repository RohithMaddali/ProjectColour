﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Player : MonoBehaviour
{
    private uint footsteps;
    RaycastHit hit;
    [SerializeField] LayerMask lm;
    RBMove movement;
    [SerializeField] Transform raycastPos;

    void Start()
    {
        SetInitialSwitches();
        movement = GetComponentInParent<RBMove>();
    }
    void Update()
    {
        MaterialCheck();
    }
    void SetInitialSwitches()
    {
        AkSoundEngine.SetSwitch("fs_material_switch_group", "Dirt", gameObject);
        AkSoundEngine.SetSwitch("fs_movement_switch_group", "Walking", gameObject);
    }
    public void Step()
    {
        if (movement.isGrounded)
        {
            footsteps = AkSoundEngine.PostEvent("ev_sfx_plr_foosteps", gameObject);
            Debug.Log("step");
        }
    }
    public void GrabColour()
    {
        AkSoundEngine.PostEvent("ev_colour_grab_sequence", gameObject); 
    }

    void MaterialCheck()
    {
        Physics.Raycast(raycastPos.position, Vector3.down, out hit, 1.5f, lm);
        Debug.DrawRay(raycastPos.position, Vector3.down * 1.5f, Color.red);
        if (hit.collider)
        {
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Dirt")
            {
                Debug.Log("Dirt");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Dirt", gameObject);
            }
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Stone")
            {
                Debug.Log("Stone");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Stone", gameObject);
            }
        }
    }
}
