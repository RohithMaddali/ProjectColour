using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public GameObject lastMenu;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public PauseMenu pm;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        lastMenu.SetActive(true);
        gameObject.SetActive(false);
        pm.submenu = false;
    }
}
