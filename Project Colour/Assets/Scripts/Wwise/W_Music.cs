using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Music : MonoBehaviour
{
    private uint musicExploreID;
    void Start()
    {
        AkSoundEngine.SetSwitch("MusicSwitch", "Hub", gameObject);
        musicExploreID = AkSoundEngine.PostEvent("ev_mx_explore", gameObject);
    }

    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(musicExploreID);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GreenTriger"))
        {
            Debug.Log("green trigger");
            AkSoundEngine.SetSwitch("MusicSwitch", "Green", gameObject);
            AkSoundEngine.SetState("Colour", "Green");
        }
        if (other.gameObject.CompareTag("BlueTrigger"))
        {
            Debug.Log("blue trigger");
            AkSoundEngine.SetSwitch("MusicSwitch", "Blue", gameObject);
            AkSoundEngine.SetState("Colour", "Blue");
        }
        if (other.gameObject.CompareTag("RedTrigger"))
        {
            Debug.Log("green trigger");
            AkSoundEngine.SetSwitch("MusicSwitch", "Red", gameObject);
            AkSoundEngine.SetState("Colour", "Red");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GreenTriger") || other.gameObject.CompareTag("BlueTrigger") || other.gameObject.CompareTag("RedTrigger"))
        {
            Debug.Log("exit green");
            AkSoundEngine.SetState("Colour", "None");
            AkSoundEngine.SetSwitch("MusicSwitch", "hub", gameObject);
        }
    }
}
