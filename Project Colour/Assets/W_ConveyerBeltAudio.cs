using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_ConveyerBeltAudio : MonoBehaviour
{
    uint conveyerBeltAudio;
    bool toggle;

    public void ConveyerBeltSound()
    {
        if (!toggle)
        {
            conveyerBeltAudio = AkSoundEngine.PostEvent("ev_conveyer_belt", gameObject);
            toggle = true;
        }
        else
        {
            AkSoundEngine.StopPlayingID(conveyerBeltAudio);
            toggle = false;
        }
    }
}
