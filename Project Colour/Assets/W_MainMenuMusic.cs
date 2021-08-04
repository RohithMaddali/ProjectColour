using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_MainMenuMusic : MonoBehaviour
{
    uint mainMenuMusic;
    uint voiceOver;

    private void Start()
    {
        voiceOver = AkSoundEngine.PostEvent("ev_waking_the_weapon", gameObject);
    }
    public void StartMainMenuMusic()
    {
        mainMenuMusic = AkSoundEngine.PostEvent("ev_mx_mainmenu_and_intro_music", gameObject);
        AkSoundEngine.StopPlayingID(voiceOver);
    }
    public void StartGame()
    {
       AkSoundEngine.SetRTPCValue("track_volume", 100);
    }
    public void StopMusic()
    {
        AkSoundEngine.StopPlayingID(mainMenuMusic);
    }
    public IEnumerator FadeMusic()
    {
        yield return new WaitForSeconds(10);
        AkSoundEngine.SetRTPCValue("main_menu_music_volume", 0);
    }
    
}
