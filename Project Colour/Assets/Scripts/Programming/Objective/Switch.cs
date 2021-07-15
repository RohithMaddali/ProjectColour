using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    PlayerControls controls;
    public GameObject g;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Switch.performed += ctx => SwitchOn();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void SwitchOn()
    {
        Debug.Log("Switch on function is working");
        cb.PowerSwitch();
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            g = other.gameObject;
            SwitchOn();
        }
        
    }
}
