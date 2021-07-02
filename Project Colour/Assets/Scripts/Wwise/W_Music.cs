using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Music : MonoBehaviour
{
    private uint musicExploreID;
    void Start()
    {
        musicExploreID = AkSoundEngine.PostEvent("ev_mx_explore", gameObject);
    }

    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(musicExploreID);
    }
}
