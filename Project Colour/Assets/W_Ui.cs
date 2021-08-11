using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Ui : MonoBehaviour
{
    public void VolumeSider(float volume)
    {
        AkSoundEngine.SetRTPCValue("Master_Volume", volume);
    }
    public void SFXBus(float volume)
    {
        AkSoundEngine.SetRTPCValue("sfx_bus_volume", volume);
    }
    public void MusicBus(float volume)
    {
        AkSoundEngine.SetRTPCValue("music_bus_volume", volume);
    }
    public void VoiceOverBus(float volume)
    {
        AkSoundEngine.SetRTPCValue("voice_over_bus_volume", volume);
    }
    public void OnClick()
    {
        AkSoundEngine.PostEvent("ev_sfx_ui_hover", gameObject);
    }
    public void OnHover()
    {
        AkSoundEngine.PostEvent("ev_sfx_ui_click", gameObject);
    }
}

