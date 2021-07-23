using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    PlayerControls controls;
    public GameObject player;
    public Canvas interact;
    public bool cooldown = false;

    private void Awake()
    {
        controls = new PlayerControls();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    
    public void OnTriggerStay(Collider other)
    {
        interact.gameObject.SetActive(true);
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && cooldown == false)
            {
                AkSoundEngine.PostEvent("ev_switch_on", gameObject);
                cb.PowerSwitch();
                Invoke("ResetCooldown", 3.0f);
                cooldown = true;
            }
            else if (controls.Gameplay.Switch.triggered)
            {
                cb.PowerSwitch();
            }
        }
    }

    void ResetCooldown()
    {
        cooldown = false;
    }

    public void OnTriggerExit(Collider other)
    {
        interact.gameObject.SetActive(false);
    }
}
