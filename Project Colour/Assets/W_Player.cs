using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Player : MonoBehaviour
{
    public void Step()
    {
        AkSoundEngine.SetSwitch("fs_material_switch_group", "Dirt", gameObject);
        AkSoundEngine.SetSwitch("fs_movement_switch_group", "Walking", gameObject);
        AkSoundEngine.PostEvent("ev_sfx_plr_foosteps", gameObject);
        Debug.Log("step");
    }
}
