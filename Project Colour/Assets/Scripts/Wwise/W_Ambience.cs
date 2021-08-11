using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class W_Ambience : MonoBehaviour
{
    private uint ambienceID;
    uint waterId;
    [SerializeField] bool isMainMenu;
    void Start()
    {
        if (isMainMenu)
        {
            AkSoundEngine.SetSwitch("amb_area", "green_area", gameObject);
            ambienceID = AkSoundEngine.PostEvent("ev_amb_switcher", gameObject);
            waterId = AkSoundEngine.PostEvent("ev_sfx_running_water", gameObject);
        }
        else
        {
            AkSoundEngine.SetSwitch("amb_area", "hub_area", gameObject);
            ambienceID = AkSoundEngine.PostEvent("ev_amb_switcher", gameObject);
            waterId = AkSoundEngine.PostEvent("ev_sfx_running_water", gameObject);
        }
    }
    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(ambienceID);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GreenTriger"))
        {
            Debug.Log("green trigger");
            AkSoundEngine.SetSwitch("amb_area", "green_area", gameObject);
        }
        if (other.gameObject.CompareTag("RedTrigger"))
        {
            Debug.Log("red trigger");
            AkSoundEngine.SetSwitch("amb_area", "red_area", gameObject);
        }
        if (other.gameObject.CompareTag("BlueTrigger"))
        {
            Debug.Log("blue trigger");
            AkSoundEngine.SetSwitch("amb_area", "blue_area", gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GreenTriger") || other.gameObject.CompareTag("BlueTrigger"))
        {
            Debug.Log("exit green");
            AkSoundEngine.SetSwitch("amb_area", "hub_area", gameObject);
        }
    }
}
