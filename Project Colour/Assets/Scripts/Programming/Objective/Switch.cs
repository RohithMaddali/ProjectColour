using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    PlayerControls controls;
    public GameObject player;
    private void Awake()
    {
        controls = new PlayerControls();
        //controls.Gameplay.Switch.performed += ctx => SwitchOn();
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
        Debug.Log("Check for collider");
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AkSoundEngine.PostEvent("ev_switch_on", gameObject);
                //controls.Gameplay.Switch.triggered
                Debug.Log("Switch on function is working");
                cb.PowerSwitch();
            }
            else if (controls.Gameplay.Switch.triggered)
            {
                Debug.Log("Switch");
                cb.PowerSwitch();
            }
        }
    }
}
