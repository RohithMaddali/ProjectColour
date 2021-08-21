using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Player : MonoBehaviour
{
    private uint footsteps;
    RaycastHit hit;
    [SerializeField] LayerMask lm;
    RBMove movement;
    [SerializeField] Transform raycastPos;
    bool doOnce;
    bool dontPlay;

    void Start()
    {
        SetInitialSwitches();
        movement = GetComponentInParent<RBMove>();
    }
    void FixedUpdate()
    {
        MaterialCheck();
    }
    private void Update()
    {
        Landing();
    }
    void SetInitialSwitches()
    {
        AkSoundEngine.SetSwitch("fs_material_switch_group", "Stone", gameObject);
        AkSoundEngine.SetSwitch("fs_movement_switch_group", "Walking", gameObject);
    }
    public void JumpSound()
    {
        dontPlay = true;
        AkSoundEngine.PostEvent("ev_sfx_landing", gameObject);
    }
    void Landing()
    {
        if (dontPlay) //Stops sound playing at the start of the game.
        {
            if (movement.isGrounded && !doOnce)
            {
                AkSoundEngine.PostEvent("ev_sfx_landing", gameObject);
                doOnce = true;
            }
            else if (!movement.isGrounded)
            {
                doOnce = false;
            }
        }
    }
    public void Step()
    {
        if (movement.isGrounded)
        {
            dontPlay = true; 
            footsteps = AkSoundEngine.PostEvent("ev_sfx_plr_foosteps", gameObject);
        }
    }
    public void GrabColour()
    {
        AkSoundEngine.PostEvent("ev_colour_grab_sequence", gameObject); 
    }

    void MaterialCheck()
    {
        Physics.Raycast(raycastPos.position, Vector3.down, out hit, 1f, lm);
        Debug.DrawRay(raycastPos.position, Vector3.down * 1f, Color.red);
        if (hit.collider)
        {
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Dirt")
            {
                //Debug.Log("Dirt");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Dirt", gameObject);
            }
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Stone")
            {
                //Debug.Log("Stone");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Stone", gameObject);
            }
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Metal")
            {
                //Debug.Log("Stone");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Metal", gameObject);
            }
            if (hit.collider.GetComponent<W_TagExtender>() != null && hit.collider.GetComponent<W_TagExtender>().tags[0] == "Water")
            {
                //Debug.Log("Water");
                AkSoundEngine.SetSwitch("fs_material_switch_group", "Water", gameObject);
            }
        }
    }
}
