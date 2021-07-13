using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AJ;

public class OutlineManager : MonoBehaviour
{
    GameObject player;
    ChangeColour changeColour;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        changeColour = player.GetComponentInChildren<ChangeColour>();
        outline = GetComponentInChildren<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColour.colourTarget != null)
        {
            if (changeColour.colourTarget = this.gameObject)
            {
                outline.OutlineWidth = 3;
            }
            else
            {
                outline.OutlineWidth = 0;
            }
        }
        else
        {
            outline.OutlineWidth = 0;
        }
    }
}
