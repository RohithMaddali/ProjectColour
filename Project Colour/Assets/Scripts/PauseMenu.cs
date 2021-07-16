﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;

    public bool GameIsPaused = false;
    public GameObject controlsMenu;
    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public GameObject MainMenuPrompt;
    public bool submenu = false;
    public GameObject player;

    public SettingsMenu sm;

    public GameObject pauseMenu;
    public GameObject mainMenuFirst;
    public GameObject settingsFirst;
    public GameObject controlsFirst;
    public GameObject quitFirst;
    // Update is called once per frame

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Pause.performed += ctx => PauseActive();
    }

    void Start()
    {
        sm = settingsMenu.GetComponent<SettingsMenu>();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    
    void PauseActive()
    {
        if (submenu == false)
        {
            player = GameObject.Find("ThirdPersonPlayer");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
                
        } 
        
    }

    public void Resume()
    {
        submenu = false;
        controlsMenu.SetActive(false);
        quitMenu.SetActive(false);
        MainMenuPrompt.SetActive(false);
        settingsMenu.SetActive(false);
        player.GetComponent<RBMove>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        player.GetComponent<RBMove>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenu);
        Time.timeScale = 0f;
        GameIsPaused = true;
 
    }

    public void Settings()
    {
        submenu = true;
        settingsMenu.SetActive(true);
        sm.lastMenu = sm.pauseMenu;
        pauseMenuUI.SetActive(false);
        Debug.Log("Loading Settings...");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirst);
    }

    public void Controls()
    {
        submenu = true;
        controlsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        Debug.Log("Loading Controls...");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirst);
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        controlsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        settingsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        quitMenu.SetActive(false);
        MainMenuPrompt.SetActive(false);
        submenu = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenu);
    }  
    

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Main Menu...");
        pauseMenuUI.SetActive(false);
        MainMenuPrompt.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        submenu = true;
        quitMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitFirst);
    }

    public void CloseGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void mainMenuPrompt()
    {
        submenu = true;
        MainMenuPrompt.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);

    }
}
