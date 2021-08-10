﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AJ;

public class OutlineManager : MonoBehaviour
{
    public GameObject player;
    public GameObject self;
    public ChangeColour changeColour;
    public Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        if (!(player is null)) changeColour = player.GetComponentInChildren<ChangeColour>();
        outline = self.GetComponentInChildren<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(changeColour is null) && changeColour.colourTarget != null)
        {
            if (changeColour.colourTarget == self)
            {
                outline.enabled = true;
                outline.OutlineColor = changeColour.thisRenderer.material.color;
                outline.OutlineWidth = 3;
            }
            else
            {
                outline.OutlineColor = Color.white;
                outline.enabled = false;
                outline.OutlineWidth = 0;
            }
        }
        else
        {
            outline.OutlineColor = Color.white;
            outline.enabled = false;
            outline.OutlineWidth = 0;
        }
    }
}
