using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject controlsMenu;
    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public bool submenu = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && submenu == false)
        {
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Settings()
    {
        submenu = true;
        settingsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        Debug.Log("Loading Settings...");
    }

    public void Controls()
    {
        submenu = true;
        controlsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        Debug.Log("Loading Controls...");
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        controlsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        settingsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        quitMenu.SetActive(false);
        submenu = false;
    }  
    

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        submenu = true;
        quitMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void CloseGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
