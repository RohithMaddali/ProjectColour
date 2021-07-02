using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class W_Ambience : MonoBehaviour
{
    private uint ambienceID;
    void Start()
    {
        ambienceID = AkSoundEngine.PostEvent("ev_all_ambience", gameObject);
    }
    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(ambienceID);
    }
}
