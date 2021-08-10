﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pyro;

public class Tutorial : MonoBehaviour
{
    MenuScript menu;
    GameManager gm;
    public Saving save;
    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<MenuScript>();
        gm = FindObjectOfType<GameManager>();
        gm.green = GameObject.FindGameObjectWithTag("GreenPrism");
        gm.blue = GameObject.FindGameObjectWithTag("BluePrism");
        gm.red = GameObject.FindGameObjectWithTag("RedPrism");
        if (gm.gPrism == 2)
        {
            gm.green.GetComponent<Renderer>().material = gm.Green;
        }
        if (gm.bPrism == 2)
        {
            gm.blue.GetComponent<Renderer>().material = gm.Blue;
        }
        if (gm.rPrism == 2)
        {
            gm.red.GetComponent<Renderer>().material = gm.Red;
        }
        if (gm.displayTips)
        {
            menu.tips.SetActive(true);
        }

        if (gm.continuing)
        {
            save.Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
