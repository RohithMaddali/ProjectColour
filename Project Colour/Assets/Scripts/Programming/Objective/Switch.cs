using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    PlayerControls controls;
    public GameObject player;
    public Canvas interact;
    public float waitForCooldown = 1f;
    public bool isCooldown = false;

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
            if (!isCooldown)
            {
                if (controls.Gameplay.Switch.triggered)
                {
                    AkSoundEngine.PostEvent("ev_switch_on", gameObject);
                    cb.PowerSwitch();
                    StartCoroutine(WaitSeconds());
                }
            }
        }
    }

    IEnumerator WaitSeconds()
    {
        isCooldown = true;
        yield return new WaitForSeconds(waitForCooldown);
        isCooldown = false;
    }

    public void OnTriggerExit(Collider other)
    {
        interact.gameObject.SetActive(false);
    }
}
