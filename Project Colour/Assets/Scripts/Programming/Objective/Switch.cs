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
    public bool canUseSwitch = false;

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

    public void Update()
    {
        if (canUseSwitch)
        {
            if (controls.Gameplay.Switch.triggered && !isCooldown)
            {
                AkSoundEngine.PostEvent("ev_switch_on", gameObject);
                cb.PowerSwitch();
                StartCoroutine(WaitSeconds());
                isCooldown = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        interact.gameObject.SetActive(true);
        if (other.tag == "Player")
        {
            canUseSwitch = true;
        }
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(waitForCooldown);
        isCooldown = false;
    }

    public void OnTriggerExit(Collider other)
    {
        interact.gameObject.SetActive(false);
        canUseSwitch = false;
    }
}
