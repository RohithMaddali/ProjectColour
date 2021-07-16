using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Pusher_Audio : MonoBehaviour
{
    public void PusherSound()
    {
        AkSoundEngine.PostEvent("ev_machinery_move", gameObject);
    }
}
