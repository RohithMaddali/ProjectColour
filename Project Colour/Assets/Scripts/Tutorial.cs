using System.Collections;
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
        if(gm.displayTips)
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
