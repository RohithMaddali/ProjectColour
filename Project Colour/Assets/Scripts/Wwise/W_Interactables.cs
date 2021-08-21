using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Interactables : MonoBehaviour
{
    public void Interactions(string obj)
    {
        if (obj == "Plant")
        {
            AkSoundEngine.PostEvent("ev_mushroom", gameObject);
        }
        if (obj == "Bridge")
        {
            AkSoundEngine.PostEvent("ev_bridge_fixed_01", gameObject); 
        }
    }  
}
