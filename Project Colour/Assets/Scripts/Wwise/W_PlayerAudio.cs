using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("Colour","Blue");    
    }

    // Update is called once per frame
    void Update()
    {
        AkSoundEngine.SetRTPCValue("Master_Volume", 2);
    }
}
