using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ConveyorBeltJ cb;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cb.PowerSwitch();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cb.PowerSwitch();
            }
        }
    }
}
