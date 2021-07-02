using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Ui : MonoBehaviour
{
    public void VolumeSider(float volume)
    {
        AkSoundEngine.SetRTPCValue("Master_Volume", volume * 100);
    }
}
