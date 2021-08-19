using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    PlayerControls controls;
    public GameObject player;
    public Canvas keyinteract;
    public Canvas controllerinteract;
    public float waitForCooldown = 1f;
    public bool isCooldown = false;
    public bool canUseSwitch = false;
    public bool gamepadInUse;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    public void Start()
    {
       
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
        if (Gamepad.current == null)
        {
            //Debug.Log("no gamepad connected");
            gamepadInUse = false;
            //use keyboard and mouse ui
        }
        else
        {
            Debug.Log(Gamepad.current.displayName + "Gamepad connected");
            gamepadInUse = true;
            //use gamepad ui
        }
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if (!gamepadInUse)
        {
            Debug.Log("B");
            keyinteract.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("C");
            controllerinteract.gameObject.SetActive(true);
        }

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

    private void OnTriggerExit(Collider other)
    {
        keyinteract.gameObject.SetActive(false);
        controllerinteract.gameObject.SetActive(false);
        canUseSwitch = false;
    }
}
